namespace DAL.Domain
{
    public class MealIngredient : BaseDomain
    {
        public int IngredientId { get; set; }

        public int MealId { get; set; }
    }
}
