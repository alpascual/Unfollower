using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Unfollower.Models
{
    public class FriendRing
    {
        public string ID { get; set; }
        public string TwitterUsername { get; set; }
        public string TwitterPassword { get; set; }
        public DateTime Joined { get; set; }
        public DateTime LastVisit { get; set; }        
    }

    public class FriendRingContext : DbContext
    {
        public DbSet<FriendRing> Rings { get; set; }       
    }
}