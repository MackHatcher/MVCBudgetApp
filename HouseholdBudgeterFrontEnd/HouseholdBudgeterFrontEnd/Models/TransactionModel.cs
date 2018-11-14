using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models
{
    public class TransactionModel
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}