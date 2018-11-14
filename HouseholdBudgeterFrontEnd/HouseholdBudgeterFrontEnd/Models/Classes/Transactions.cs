using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class Transactions
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int? CategoryId { get; set; }
        public virtual Categories Category { get; set; }

        public string EnteredById { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal ReconAmount { get; set; }
        public int Income { get; set; }
        public bool IsVoided { get; set; }
    }
}