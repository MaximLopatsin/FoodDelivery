namespace DAL.Domain
{
    public class MenuMeal : BaseDomain
    {
        public int MenuId { get; set; }

        public int MealId { get; set; }

        public decimal Price { get; set; }
    }
}
