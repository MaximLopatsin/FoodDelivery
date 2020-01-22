using System;
using System.Collections.Generic;

namespace FoodDelivery.Models
{
    public class InstitutionDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, decimal> Meals { get; set; } = new Dictionary<string, decimal>();

        public string Address { get; set; }

        public TimeSpan ExpectedDeliveryTime { get; set; }
    }
}