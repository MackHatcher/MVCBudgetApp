using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class JoinHouseholdBindingModel
    {
        [Required]
        public int InviteId { get; set; }
    }
}