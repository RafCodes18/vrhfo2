using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRhfo.BL.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Auth0UserId { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime NextRenewalDueDate { get; set; }
        public string Subscription_Tier { get; set; }
        public int GoonScore { get; set; }
    }
}
