using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class HouseholdInvite
    {
        public int Id { get; set; }

        public int HouseHoldId { get; set; }
        public virtual Household HouseHold { get; set; }

        public string InvitedUserId { get; set; }
        public virtual ApplicationUser InvitedUser { get; set; }

        public DateTime InvitedDate { get; set; }
    }
}