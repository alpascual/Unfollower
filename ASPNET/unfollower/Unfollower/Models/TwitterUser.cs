using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Unfollower.Models
{
    public class TwitterUser
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public DateTime LastPull { get; set; }
        public bool DoAlert { get; set; }
        public bool DirectMessage { get; set; }
        public string DeviceToken { get; set; }

        public virtual ICollection<Follower> Followers { get; set; }
        public virtual ICollection<Unfollowers> Unfollowers { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }

    public class Follower
    {
        public virtual TwitterUser TwitterUser { get; set; }
        public string ID { get; set; }
        public string Username { get; set; }        
    }

    public class Unfollowers
    {
        public virtual TwitterUser TwitterUser { get; set; }
        public string ID { get; set; }
        public string Username { get; set; }
        public DateTime FoundTime { get; set; }       
    }

    public class Alert
    {
        public virtual TwitterUser TwitterUser { get; set; }
        public string ID { get; set; }
        public string Username { get; set; }
        public DateTime FoundTime { get; set; }
    }

    public class TwitterUserContext : DbContext
    {
        public DbSet<TwitterUser> Tweeps { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Unfollowers> Unfollowers { get; set; }
        public DbSet<Alert> Alerts { get; set; }
    }
}