using System;
using System.Collections.Generic;
using System.Data.Entity;
using DAL.Domain;

namespace DAL.Orm
{
    internal class DeliveryInitializer : CreateDatabaseIfNotExists<DeliveryContext>
    {
        protected override void Seed(DeliveryContext context)
        {
            context.Institutions.AddRange(new List<Institution>{
                new Institution
                {
                    MenuId = 1,
                    Name = "Гараж",
                    Address = "Мандарин",
                    CreationDate = new DateTime(2015, 5, 12),
                    City = "Гомель",
                    ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
                },
                new Institution
                {
                    MenuId = 2,
                    Name = "Бургер кинг",
                    Address = "Вокзал",
                    CreationDate = new DateTime(1999, 11, 12),
                    City = "Гомель",
                    ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
                },
                new Institution
                {
                    MenuId = 3,
                    Name = "МакДак",
                    Address = "Хатаевича",
                    CreationDate = new DateTime(1997, 9, 6),
                    City = "Гомель",
                    ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
                },
            });

            context.Menus.AddRange(new List<Menu>
            {
                new Menu
                {
                    InstitutionId = 1,
                    CreationDate = new DateTime(2017, 2, 10),
                },
                new Menu
                {
                    InstitutionId = 2,
                    CreationDate = new DateTime(2012, 5, 15),
                },
                new Menu
                {
                    InstitutionId = 3,
                    CreationDate = new DateTime(2013, 8, 2),
                },
            });

            context.Ingredients.AddRange(new List<Ingredient>
            {
                new Ingredient
                {
                    Name = "Мясо говядины", // 1
                },
                new Ingredient
                {
                    Name = "Мясо свинины", // 2
                },
                new Ingredient
                {
                    Name = "Огурец", // 3
                },
                new Ingredient
                {
                    Name = "Помидор", // 4
                },
                new Ingredient
                {
                    Name = "Тесто", // 5
                },
                new Ingredient
                {
                    Name = "Булка", // 6
                },
                new Ingredient
                {
                    Name = "Кетчуп", // 7
                },
                new Ingredient
                {
                    Name = "Майонез", // 8
                },
                new Ingredient
                {
                    Name = "Перец", // 9
                },
                new Ingredient
                {
                    Name = "Копченая колбаса", // 10
                },
                new Ingredient
                {
                    Name = "Сыр", // 11
                },
            });

            context.Meals.AddRange(new List<Meal>
            {
                new Meal
                {
                    Name = "Пицца \"Стандартная\"",
                },
                new Meal
                {
                    Name = "Бургер \"Чизбургер\"",
                },
                new Meal
                {
                    Name = "Напиток \"Fanta\"",
                },
            });

            context.MealIngredient.AddRange(new List<MealIngredient>
            {
                new MealIngredient
                {
                    MealId = 1,
                    IngredientId = 1,
                },
                new MealIngredient
                {
                    MealId = 1,
                    IngredientId = 3,
                },
                new MealIngredient
                {
                    MealId = 1,
                    IngredientId = 4,
                },
                new MealIngredient
                {
                    MealId = 1,
                    IngredientId = 5,
                },
                new MealIngredient
                {
                    MealId = 1,
                    IngredientId = 7,
                },
                new MealIngredient
                {
                    MealId = 1,
                    IngredientId = 11,
                },
                new MealIngredient
                {
                    MealId = 2,
                    IngredientId = 2,
                },
                new MealIngredient
                {
                    MealId = 2,
                    IngredientId = 3,
                },
                new MealIngredient
                {
                    MealId = 2,
                    IngredientId = 4,
                },
                new MealIngredient
                {
                    MealId = 2,
                    IngredientId = 6,
                },
                new MealIngredient
                {
                    MealId = 2,
                    IngredientId = 7,
                },
                new MealIngredient
                {
                    MealId = 2,
                    IngredientId = 11,
                },
            });

            context.MenuMeal.AddRange(new List<MenuMeal>
            {
                new MenuMeal
                {
                    MealId = 1,
                    MenuId = 1,
                    Price = 12m,
                },
                new MenuMeal
                {
                    MealId = 2,
                    MenuId = 2,
                    Price = 2m,
                },
                new MenuMeal
                {
                    MealId = 2,
                    MenuId = 3,
                    Price = 2.1m,
                },
                new MenuMeal
                {
                    MealId = 3,
                    MenuId = 1,
                    Price = 2m,
                },
                new MenuMeal
                {
                    MealId = 3,
                    MenuId = 2,
                    Price = 1.5m,
                },
                new MenuMeal
                {
                    MealId = 3,
                    MenuId = 3,
                    Price = 1.5m,
                },
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
