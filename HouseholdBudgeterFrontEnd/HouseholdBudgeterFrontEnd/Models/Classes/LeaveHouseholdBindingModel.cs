using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class LeaveHouseholdBindingModel
    {
        [Required]
        public int HouseHoldId { get; set; }
    }
}