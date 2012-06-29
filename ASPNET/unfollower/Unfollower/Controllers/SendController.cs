using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Unfollower.Controllers
{
    public class SendController : Controller
    {
        //
        // GET: /Send/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Message(string sUserName, string sMessage)
        {
            Utils.TwitterUtils.SendTweet("SA: @" + sUserName + " " + sMessage, 
                "cmU4OoYsh1QODUePBSWsug",
                "kD7wqsZgZOHRW65x9biqLuWPqEKavAQbvSwqCNgT8",
                "361290246-OYNf6mvJ3qNE2CU2W3Jm1s9wDuIE4XNCFWbs4M1E",
                "PvCIYD6bkHTbW0q0Ybf2pYBfZHvk0mBYoXyeoTENHBk");

            return View();
        }

    }
}
