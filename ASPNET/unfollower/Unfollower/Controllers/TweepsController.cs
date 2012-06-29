using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TweetSharp;
using System.Web.Mvc.Async;
using System.Threading;


namespace Unfollower.Controllers
{
    public class TweepsController : AsyncController 
    {

        public class TweetThreadParams
        {
            public string sUserName { get; set; }
            public Models.TwitterUser myUser { get; set; }
            public Models.TwitterUserContext dbContext { get; set; }
        }

        public ActionResult CleanUsers()
        {
            Database.DefaultConnectionFactory =
                new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
            
            for (int i = 0; i < userlist.Count; i++)
            {
                if (i + 1 < userlist.Count)
                {
                    Unfollower.Models.TwitterUser myUser = userlist[i];
                    Unfollower.Models.TwitterUser myUser2 = userlist[i + 1];

                    if (myUser.Username == myUser2.Username)
                    {
                        //dbContext.Entry(myUser.Unfollowers).State = System.Data.EntityState.Deleted;
                        //dbContext.Entry(myUser.Followers).State = System.Data.EntityState.Deleted;
                        //dbContext.Entry(myUser.Alerts).State = System.Data.EntityState.Deleted;

                        while (myUser.Unfollowers.Count > 0)
                        {
                            Models.Unfollowers foll = myUser.Unfollowers.ElementAt(0);
                            dbContext.Entry(foll).State = System.Data.EntityState.Deleted;
                        }
                        while (myUser.Followers.Count > 0)
                        {
                            Models.Follower unf = myUser.Followers.ElementAt(0);
                            dbContext.Entry(unf).State = System.Data.EntityState.Deleted;
                        }
                        while (myUser.Alerts.Count > 0)
                        {
                            Models.Alert ale = myUser.Alerts.ElementAt(0);
                            dbContext.Entry(ale).State = System.Data.EntityState.Deleted;
                        }

                        dbContext.Entry(myUser).State = System.Data.EntityState.Deleted;
                    }
                }
            }

            dbContext.SaveChanges();

            return View();
        }

        public ActionResult CleanUnfollowers()
        {
            Database.DefaultConnectionFactory =
                new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();

            for (int i = 0; i < userlist.Count; i++)
            {
                Unfollower.Models.TwitterUser myUser = userlist[i];
                                
                for (int u = 0; u < myUser.Unfollowers.Count; u++)
                {
                    ViewBag.Results += "#" + myUser.Username + " cleaning: ";

                    if (u + 1 < myUser.Unfollowers.Count)
                    {
                        Models.Unfollowers unfollow = myUser.Unfollowers.ElementAt(u);
                        Models.Unfollowers unfollow2 = myUser.Unfollowers.ElementAt(u + 1);

                        if (unfollow2.Username == unfollow.Username)
                        {
                            ViewBag.Results += unfollow.Username + ",";

                            dbContext.Entry(unfollow).State = System.Data.EntityState.Deleted;
                        }
                    }
                }

                // Save at the end
                dbContext.SaveChanges();
            }

            return View();
        }
        //
        // GET: /Tweeps/

        public ActionResult Index()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            ViewBag.UserCollection = "<p>";
            try
            {
                if (dbContext.Tweeps.Count() > 0)
                {
                    List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                    foreach (Unfollower.Models.TwitterUser us in userlist)
                    {
                        ViewBag.UserCollection += us.Username.Replace("@","") + ",";
                    }
                }

                ViewBag.UserCollection += "<BR/> Total: " + dbContext.Tweeps.Count().ToString() + "</p>";
            }
            catch { }


            return View();
        }

        public ActionResult Count()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();
            ViewBag.UserCollection += "<BR/> Total Users: " + dbContext.Tweeps.Count().ToString() + "</p>";

            return View();
        }

        public ActionResult CheckAll()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();
            
            ViewBag.Results = "Processing ";

            if (dbContext.Tweeps.Count() > 0)
            {
                List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                foreach (Unfollower.Models.TwitterUser us in userlist)
                {
                    try
                    {
                        if (us.Username.IndexOf('@') > -1)
                            us.Username = us.Username.Replace("@", "");
                    }
                    catch { }

                    TweetThreadParams tweetParams = new TweetThreadParams()
                    {
                        myUser = us,
                        dbContext = dbContext,
                        sUserName = us.Username
                    };

                    //DoWork(tweetParams);
                    Thread th1 = new Thread(new ParameterizedThreadStart(DoWork));

                    th1.Start(tweetParams);

                    ViewBag.Results += ", " + us.Username;
                    Thread.Sleep(1);
                }
                                
            }

            //return Create();
            return View();
        }


        public ActionResult Unfollowers(string sUserName)
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();
            Unfollower.Models.TwitterUser myUser = null;
            try
            {
                if (dbContext.Tweeps.Count() > 0)
                {
                    List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                    foreach (Unfollower.Models.TwitterUser us in userlist)
                    {
                        if (us.Username == sUserName)
                        {
                            myUser = us;
                            break;
                        }
                    }
                }
            }
            catch { }

            if (myUser != null)
            {
                string sReturn = "";
                foreach (Models.Unfollowers unf in myUser.Unfollowers)
                {
                    sReturn += unf.FoundTime + "|" + unf.Username + ",";
                }

                if (sReturn.Length > 0)
                    sReturn = sReturn.Substring(0, sReturn.Length - 1);
                else
                    sReturn = "none";

                ViewBag.UserCollection = sReturn;
            }

            return View();
        }

        //
        // GET: /Tweeps/Details/5

        public ActionResult Details(string sUserName)
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            Unfollower.Models.TwitterUser myUser = null;
            try
            {
                if (dbContext.Tweeps.Count() > 0)
                {
                    List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                    foreach (Unfollower.Models.TwitterUser us in userlist)
                    {
                        if (us.Username == sUserName)
                        {
                            myUser = us;
                            break;
                        }
                    }
                }
            }
            catch { }
            // create user
            if (myUser == null)
            {
                myUser = new Models.TwitterUser()
                {
                    Username = sUserName,
                    ID = Guid.NewGuid().ToString(),
                    LastPull = DateTime.Now.AddDays(-2),
                    DirectMessage = false,
                    DoAlert = false
                };

                dbContext.Tweeps.Add(myUser);
                dbContext.SaveChanges();

                try
                {
                    //Alert me
                    Utils.TwitterUtils.SendTweet("Thanks " + sUserName + " for downloading Unfollowers, now wait for me, to see who unfollowers you./cc @alpascual", 0);
                }
                catch
                {
                }
            }

            // This could take a long time
            if (myUser.LastPull < DateTime.Now.AddHours(-5))
            {
                //DoWork(sUserName, myUser, dbContext);
                AsyncManager.OutstandingOperations.Increment();

                TweetThreadParams tParams = new TweetThreadParams()
                {
                    dbContext = dbContext,
                    sUserName = sUserName,
                    myUser = myUser
                };
               
                Thread th1 = new Thread(new ParameterizedThreadStart(DoWork));

                th1.Start(tParams);

                AsyncManager.OutstandingOperations.Decrement();

                ViewBag.UserCollection = "running";
                return View();
            }

            string sReturn = "";
            foreach (Models.Unfollowers unf in myUser.Unfollowers)
            {
                sReturn += unf.FoundTime + "|" + unf.Username + ",";
            }

            if (sReturn.Length > 0)
                sReturn = sReturn.Substring(0, sReturn.Length - 1);
            else
                sReturn = "none";

            ViewBag.UserCollection = sReturn;

            return View();
        }

        public ActionResult ProcessOne(string sUserName)
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            ViewBag.Results = "Processing only " + sUserName;

            if (dbContext.Tweeps.Count() > 0)
            {
                List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                foreach (Unfollower.Models.TwitterUser us in userlist)
                {
                    try
                    {
                        if (us.Username.IndexOf('@') > -1)
                            us.Username = us.Username.Replace("@", "");
                    }
                    catch { }

                    if (us.Username == sUserName)
                    {
                        TweetThreadParams tweetParams = new TweetThreadParams()
                        {
                            myUser = us,
                            dbContext = dbContext,
                            sUserName = us.Username
                        };

                        DoWork(tweetParams);                      

                        ViewBag.Results += ", " + us.Username;

                        Create();
                    }
                }

            }
            return View();
        }

        public static void DoWork(object oParams)
        {
            try
            {
                TweetThreadParams twParams = oParams as TweetThreadParams;
                string sUserName = twParams.sUserName;
                Models.TwitterUserContext dbContext = twParams.dbContext;
                Models.TwitterUser myUser = twParams.myUser;

                TwitterUser[] mylist = null;

                try { mylist = Utils.TwitterUtils.GetFollowersFor(sUserName); }
                catch { return; }

                var dbFollowers = myUser.Followers;
                // First time for start tracking
                if (dbFollowers == null)
                {
                    foreach (TwitterUser user in mylist)
                    {
                        Models.Follower follow = new Models.Follower()
                        {
                            TwitterUser = myUser,
                            ID = Guid.NewGuid().ToString(),
                            Username = user.ScreenName
                        };
                        dbContext.Followers.Add(follow);
                    }
                    dbContext.SaveChanges();

                    myUser.LastPull = DateTime.Now;
                    dbContext.Entry(myUser).State = System.Data.EntityState.Modified;
                    dbContext.SaveChanges();

                    //ViewBag.UserCollection = "empty";

                    //return View();
                }
                else
                {
                    // Process
                    myUser.LastPull = DateTime.Now;
                    dbContext.Entry(myUser).State = System.Data.EntityState.Modified;

                    // Old dbFollowers
                    // new myList
                    List<string> newestFollowers = new List<string>();
                    foreach (TwitterUser user in mylist)
                    {
                        newestFollowers.Add(user.ScreenName);
                    }

                    // Get the list of unfollowers                    
                    foreach (Models.Follower follower in dbFollowers)
                    {
                        if (newestFollowers.Contains(follower.Username) == false)
                        {
                            Models.Unfollowers unfollow = new Models.Unfollowers()
                            {
                                TwitterUser = myUser,
                                Username = follower.Username,
                                ID = Guid.NewGuid().ToString(),
                                FoundTime = DateTime.Now
                            };
                                                        
                            dbContext.Unfollowers.Add(unfollow);

                            // If sign up for alerts
                            if (myUser.DoAlert == true)
                            {
                                Models.Alert alert = new Models.Alert()
                                {
                                    TwitterUser = myUser,
                                    Username = follower.Username,
                                    ID = Guid.NewGuid().ToString(),
                                    FoundTime = DateTime.Now
                                };
                                dbContext.Alerts.Add(alert);
                            }
                        }

                        try { dbContext.SaveChanges(); }
                        catch { }
                    }

                    while (myUser.Followers.Count > 0)
                    {
                        Models.Follower foll = myUser.Followers.ElementAt(0);
                        dbContext.Entry(foll).State = System.Data.EntityState.Deleted;
                    }

                    dbContext.SaveChanges();

                    // Reset the followers
                    foreach (TwitterUser user in mylist)
                    {
                        Models.Follower follow = new Models.Follower()
                        {
                            TwitterUser = myUser,
                            ID = Guid.NewGuid().ToString(),
                            Username = user.ScreenName
                        };
                        dbContext.Followers.Add(follow);
                    }

                    dbContext.SaveChanges();
                }

            }
            catch { }
            
        }

        //
        // GET: /Tweeps/Create

        public ActionResult Create()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            Unfollower.Models.TwitterUser myUser = null;
            
            if (dbContext.Tweeps.Count() > 0)
            {
                List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                foreach (Unfollower.Models.TwitterUser us in userlist)
                {
                    // Has alerts
                    if (us.DoAlert == true)
                    {
                        if (us.Alerts.Count() > 0)
                        {
                            // Send a tweet with all alerts
                            string sMessage = "@" + us.Username + " list of users that unfollowed you: ";
                            string slast = "";
                            foreach (Models.Alert unf in us.Alerts)
                            {
                                if (unf.Username.ToLower() != slast.ToLower())
                                {
                                    if ( !sMessage.Contains(unf.Username) )
                                        sMessage += " @" + unf.Username;
                                }

                                slast = unf.Username;
                            }

                            if (sMessage.Length > 140)
                            {
                                sMessage = sMessage.Substring(0, 135);
                                sMessage += "...";
                            }

                            try
                            {
                                // Send Notification                            
                                if (us.DeviceToken != null)
                                {
                                    if (us.DeviceToken.Length > 4)
                                        Utils.AppleNotification.SendNotification(us.DeviceToken, sMessage);
                                }
                            }
                            catch { }

                            if (us.DirectMessage == true)
                            {
                                ViewBag.Result += "DM " + sMessage + "</BR>";
                                Utils.TwitterUtils.SendTweet(sMessage, Int32.Parse(us.ID));
                            }
                            else if (us.DoAlert == true)
                            {
                                ViewBag.Result += "Tweet " + sMessage + "</BR>";
                                Utils.TwitterUtils.SendTweet(sMessage, 0);
                            }

                            // Delete all alerts
                            while (us.Alerts.Count > 0)
                            {
                                Models.Alert alert = us.Alerts.ElementAt(0);
                                dbContext.Entry(alert).State = System.Data.EntityState.Deleted;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        if (us.Alerts.Count() > 0)
                        {
                            while (us.Alerts.Count > 0)
                            {
                                Models.Alert alert = us.Alerts.ElementAt(0);
                                dbContext.Entry(alert).State = System.Data.EntityState.Deleted;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            

            return View();
        }


        public ActionResult AddToken(string sUserName, string sToken)
        {
            try
            {
                // TODO: Add update logic here
                Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

                Unfollower.Models.TwitterUser myUser = null;
                try
                {
                    if (dbContext.Tweeps.Count() > 0)
                    {
                        List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                        foreach (Unfollower.Models.TwitterUser us in userlist)
                        {
                            if (us.Username == sUserName)
                            {
                                myUser = us;
                                break;
                            }
                        }
                    }
                }
                catch { }
                // create user
                if (myUser != null)
                {
                    myUser.DeviceToken = sToken;

                    dbContext.Entry(myUser).State = System.Data.EntityState.Modified;
                    dbContext.SaveChanges();

                    ViewBag.Result = "Good";
                }

                return View();
            }
            catch
            {
                ViewBag.Result = "Exception";
                return View();
            }
        }
 
        
             
        //Enable Edits to the users
        public ActionResult Edit(string sUserName, bool bAlert, bool bDM)
        {
            try
            {
                // TODO: Add update logic here
                Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

                Unfollower.Models.TwitterUser myUser = null;
                try
                {
                    if (dbContext.Tweeps.Count() > 0)
                    {
                        List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                        foreach (Unfollower.Models.TwitterUser us in userlist)
                        {
                            if (us.Username == sUserName)
                            {
                                myUser = us;
                                break;
                            }
                        }
                    }
                }
                catch { }
                // create user
                if (myUser != null)
                {
                    myUser.DoAlert = bAlert;
                    myUser.DirectMessage = bDM;

                    if (myUser.Username.IndexOf("@") > -1)
                        myUser.Username = myUser.Username.Replace("@", "");

                    dbContext.Entry(myUser).State = System.Data.EntityState.Modified;
                    dbContext.SaveChanges();

                    ViewBag.Result = "Good";
                }

                return View();
            }
            catch
            {
                ViewBag.Result = "Exception";
                return View();
            }
        }

        public ActionResult DeleteUser(string sUserName)
        {

            Database.DefaultConnectionFactory =
                new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            Unfollower.Models.TwitterUser myUser = null;

            try
            {
                if (dbContext.Tweeps.Count() > 0)
                {
                    List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                    foreach (Unfollower.Models.TwitterUser us in userlist)
                    {
                        if (us.Username == sUserName)
                        {
                            myUser = us;
                            break;
                        }
                    }
                }


            }
            catch { }

            if (myUser != null)
            {
                while (myUser.Unfollowers.Count > 0)
                {
                    Models.Unfollowers foll = myUser.Unfollowers.ElementAt(0);
                    dbContext.Entry(foll).State = System.Data.EntityState.Deleted;
                }
                while (myUser.Followers.Count > 0)
                {
                    Models.Follower unf = myUser.Followers.ElementAt(0);
                    dbContext.Entry(unf).State = System.Data.EntityState.Deleted;
                }
                while (myUser.Alerts.Count > 0)
                {
                    Models.Alert ale = myUser.Alerts.ElementAt(0);
                    dbContext.Entry(ale).State = System.Data.EntityState.Deleted;
                }
                
                dbContext.Entry(myUser).State = System.Data.EntityState.Deleted;
                dbContext.SaveChanges();
            }

            return View();
        }

        //
        // GET: /Tweeps/Delete/5
 
        public ActionResult Delete(string sUserName)
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.TwitterUserContext dbContext = new Models.TwitterUserContext();

            Unfollower.Models.TwitterUser myUser = null;

            try
            {
                if (dbContext.Tweeps.Count() > 0)
                {
                    List<Unfollower.Models.TwitterUser> userlist = dbContext.Tweeps.ToList();
                    foreach (Unfollower.Models.TwitterUser us in userlist)
                    {
                        if (us.Username == sUserName)
                        {
                            myUser = us;
                            break;
                        }
                    }
                }

                
            }
            catch { }

            if (myUser != null)
            {
                while (myUser.Unfollowers.Count > 0)
                {
                    Models.Unfollowers foll = myUser.Unfollowers.ElementAt(0);
                    dbContext.Entry(foll).State = System.Data.EntityState.Deleted;
                }

                dbContext.SaveChanges();
            }

            return View();
        }

        //
        // POST: /Tweeps/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
