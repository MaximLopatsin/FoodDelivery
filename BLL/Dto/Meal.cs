using System.Collections.Generic;

namespace BLL.Dto
{
    public class Meal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int MenuId { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
