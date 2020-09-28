using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class Meal
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public int MenuId { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
