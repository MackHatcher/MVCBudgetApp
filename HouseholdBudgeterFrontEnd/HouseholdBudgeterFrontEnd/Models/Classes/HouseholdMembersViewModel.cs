using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class HouseholdMembersViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsCreator { get; set; }
    }

    public class HouseHoldViewModel
    {
        public HouseHoldViewModel()
        {
            Members = new List<HouseholdMembersViewModel>();
        }

        public string Name { get; set; }

        public List<HouseholdMembersViewModel> Members { get; set; }
    }
}