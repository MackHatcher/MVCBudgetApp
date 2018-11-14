using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int Id { get; set; }

        public int HouseHoldId { get; set; }
        public virtual Household HouseHold { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
        public decimal ReconBalance { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}