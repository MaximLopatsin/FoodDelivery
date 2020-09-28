using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class InstitutionDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Продукты")]
        public Dictionary<string, decimal> Meals { get; set; } = new Dictionary<string, decimal>();

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Время доставки")]
        public TimeSpan ExpectedDeliveryTime { get; set; }
    }
}