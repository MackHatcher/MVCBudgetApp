﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Models.Classes
{
    public class CreateHouseholdBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}