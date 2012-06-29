using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using System.Diagnostics;
using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Unfollower.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            //Utils.TwitterUtils.SendTweet("test two", 0);

            return View();

            //Database.DefaultConnectionFactory =
            //     new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            //Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            //Unfollower.Models.TwitterUser myUser = null;
            //try
            //{
            //    if (dbContext.Tweeps.Count() > 0)
            //    {
            //        List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
            //        foreach (Unfollower.Models.TwitterUser us in userlist)
            //        {
            //            if (us.Username == "AlPascual")
            //            {
            //                myUser = us;
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch { }
            //// create user
            //if (myUser == null)
            //{
            //    myUser = new Models.TwitterUser()
            //    {
            //        Username = "AlPascual",
            //        ID = Guid.NewGuid().ToString(),
            //        LastPull = DateTime.Now.AddDays(-2)
            //    };
                
            //    dbContext.Tweeps.Add(myUser);
            //    dbContext.SaveChanges();

            //}

            //if (myUser.LastPull < DateTime.Now.AddMinutes(-5))
            //{
            //    TwitterUser[] mylist = Utils.TwitterUtils.GetFollowersFor("AlPascual");

            //    var dbFollowers = myUser.Followers;
            //    // First time for start tracking
            //    if (dbFollowers == null)
            //    {                    
            //        foreach(TwitterUser user in mylist)
            //        {
            //            Models.Follower follow = new Models.Follower()
            //            {
            //                TwitterUser = myUser,
            //                ID = Guid.NewGuid().ToString(),
            //                Username = user.ScreenName                            
            //            };
            //            dbContext.Followers.Add(follow);                        
            //        }
            //        dbContext.SaveChanges();

            //        myUser.LastPull = DateTime.Now;
            //        dbContext.Entry(myUser).State = System.Data.EntityState.Modified;
            //        dbContext.SaveChanges();
                    
            //        return View("Started Tracking");
            //    }
            //    else
            //    {
            //        // Process
            //        myUser.LastPull = DateTime.Now;
            //        dbContext.Entry(myUser).State = System.Data.EntityState.Modified;

            //        // Old dbFollowers
            //        // new myList
            //        List<string> newestFollowers = new List<string>();
            //        foreach(TwitterUser user in mylist)
            //        {
            //            newestFollowers.Add(user.ScreenName);                        
            //        }

            //        // Get the list of unfollowers                    
            //        foreach (Models.Follower follower in dbFollowers)
            //        {                        
            //            if (newestFollowers.Contains(follower.Username) == false)
            //            {
            //                dbContext.Unfollowers.Add(new Models.Unfollowers()
            //                {
            //                    TwitterUser = myUser,
            //                    Username = follower.Username,
            //                    ID = Guid.NewGuid().ToString(),
            //                    FoundTime = DateTime.Now
            //                }); 
            //            }
            //        }
                                        
            //        for (int i = 0; i < myUser.Followers.Count; i++)
            //        {
            //            Models.Follower foll = myUser.Followers.ElementAt(i);
            //            dbContext.Entry(foll).State = System.Data.EntityState.Deleted;
            //            i = 0;
            //        }
                                        
            //        //dbContext.Entry(myUser).State = System.Data.EntityState.Modified;
            //        dbContext.SaveChanges();
                    
            //        // Reset the followers
            //        foreach (TwitterUser user in mylist)
            //        {
            //            Models.Follower follow = new Models.Follower()
            //            {
            //                TwitterUser = myUser,
            //                ID = Guid.NewGuid().ToString(),
            //                Username = user.ScreenName
            //            };
            //            dbContext.Followers.Add(follow);
            //        }

            //        dbContext.SaveChanges();
            //    }
            //}            

            //return View(myUser.Unfollowers);
        }

        public ActionResult About()
        {
            return View();
        }


      
    }
}
