using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class Household
    {
        public Household()
        {
            Members = new HashSet<ApplicationUser>();
            Categories = new HashSet<Categories>();
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}