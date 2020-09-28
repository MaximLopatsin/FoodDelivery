using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class MenuMeal
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public int MealId { get; set; }

        [Display(Name = "Блюдо")]
        public string MealName { get; set; }

        public int InstitutionId { get; set; }

        [Display(Name = "Заведение")]
        public string InstitutionName { get; set; }

        [Display(Name = "Город")]
        public string InstitutionCity { get; set; }

        [Display(Name = "Адрес")]
        public string InstitutionAddress { get; set; }

        [Display(Name = "Ожидаемое время доставки")]
        public TimeSpan ExpectedDeliveryTime { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
