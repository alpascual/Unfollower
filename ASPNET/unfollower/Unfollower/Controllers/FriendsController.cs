using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Unfollower.Controllers
{
    public class FriendsController : Controller
    {
        public ActionResult Index()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.FriendRingContext dbContext = new Models.FriendRingContext();

            return View(dbContext.Rings.ToList());
        }

        public ActionResult List()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.FriendRingContext dbContext = new Models.FriendRingContext();

            string sTemp = "";
            foreach (Unfollower.Models.FriendRing ring in dbContext.Rings.ToList())
            {
                sTemp += ring.TwitterUsername + ",";
            }

            sTemp = sTemp.Substring(0, sTemp.Length - 1);

            ViewBag.ListOfUsers = sTemp;
            return View();
        }

        public ActionResult Count()
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.FriendRingContext dbContext = new Models.FriendRingContext();

            ViewBag.Count = "";
            ViewBag.Count = dbContext.Rings.Count();

            return View();
        }

        public ActionResult Ping(string TwitterUsername)
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.FriendRingContext dbContext = new Models.FriendRingContext();

            Models.FriendRing myUser = dbContext.Rings.Single(x => x.TwitterUsername == TwitterUsername);

            if (myUser != null)
            {
                myUser.LastVisit = DateTime.Now;
                dbContext.Entry(myUser).State = System.Data.EntityState.Modified;
                dbContext.SaveChanges();
            }

            return View();
        }

        public ActionResult AddUser(string TwitterUsername, string TwitterPassword)
        {
            Database.DefaultConnectionFactory =
                 new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            Unfollower.Models.FriendRingContext dbContext = new Models.FriendRingContext();

            //var data = dbContext.Rings.Where(c => c.TwitterUsername == TwitterUsername);
            Models.FriendRing myUser = null;
            foreach (Unfollower.Models.FriendRing ring in dbContext.Rings.ToList())
            {
                if (ring.TwitterUsername == TwitterUsername)
                {
                    myUser = ring;
                    break;
                }
            }

            if (myUser == null)
            {
                myUser = new Models.FriendRing()
                {
                    Joined = DateTime.Now,
                    LastVisit = DateTime.Now,
                    ID = Guid.NewGuid().ToString(),
                    TwitterUsername = TwitterUsername,
                    TwitterPassword = TwitterPassword
                };

                dbContext.Rings.Add(myUser);
                dbContext.SaveChanges();

                Utils.TwitterUtils.SendTweet("New User " + TwitterUsername + " for the ring app /cc @alpascual", 0);
            }

            return View();
        }
    }
}
