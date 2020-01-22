using System.Data.Entity;
using DAL.Domain;

namespace DAL.Orm
{
    internal class DeliveryContext : DbContext
    {
        public DeliveryContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new DeliveryInitializer());
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<MealIngredient> MealIngredient { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuMeal> MenuMeal { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderMeal> OrderMeal { get; set; }
    }
}
