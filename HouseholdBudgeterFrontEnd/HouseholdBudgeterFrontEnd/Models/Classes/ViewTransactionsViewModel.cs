using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class ViewTransactionsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Account { get; set; }
        public string CategoryName { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsVoided { get; set; }

    }
}