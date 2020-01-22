using System;

namespace BLL.Dto
{
    public class MenuMeal
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public int MealId { get; set; }

        public string MealName { get; set; }

        public int InstitutionId { get; set; }

        public string InstitutionName { get; set; }

        public string InstitutionCity { get; set; }

        public string InstitutionAddress { get; set; }

        public TimeSpan ExpectedDeliveryTime { get; set; }

        public decimal Price { get; set; }
    }
}
