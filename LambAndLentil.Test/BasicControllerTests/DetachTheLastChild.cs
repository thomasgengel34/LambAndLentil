using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    internal class DetachTheLastChild<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            DetachTheLastIngredientChild();
            DetachTheLastMenuChild();
            DetachTheLastPlanChild();
            DetachTheLastRecipeChild();
            DetachTheLastShoppingListChild();
        }



        private static void DetachTheLastIngredientChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Ingredients = new List<Ingredient>();
            item.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            item.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            item.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            item.Ingredients.Add(new Ingredient { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save(item);
            int initialIngredientCount = item.Ingredients.Count();

            Ingredient LastIngredient = item.Ingredients.LastOrDefault();
            item.Ingredients.RemoveAt(initialIngredientCount - 1);
            bool IsLastIngredientStillThere = item.Ingredients.Contains(LastIngredient);

            Assert.AreEqual(initialIngredientCount - 1, item.Ingredients.Count());
            Assert.IsFalse(IsLastIngredientStillThere);
        }

        private static void DetachTheLastPlanChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Plans = new List<Plan>();
            item.Plans.Add(new Plan { ID = 4100, Name = "Laura" });
            item.Plans.Add(new Plan { ID = 4101, Name = "Ingalls" });
            item.Plans.Add(new Plan { ID = 4102, Name = "Wilder" });
            item.Plans.Add(new Plan { ID = 4103, Name = "Rose" });


            repo.Save(item);
            int initialPlanCount = item.Plans.Count();

            Plan LastPlan = item.Plans.LastOrDefault();
            item.Plans.RemoveAt(initialPlanCount - 1);
            bool IsLastPlanStillThere = item.Plans.Contains(LastPlan);

            Assert.AreEqual(initialPlanCount - 1, item.Plans.Count());
            Assert.IsFalse(IsLastPlanStillThere); 
        }

        private static void DetachTheLastMenuChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Menus = new List<Menu>();
            item.Menus.Add(new Menu { ID = 4205, Name = "Butter" });
            item.Menus.Add(new Menu { ID = 4206, Name = "Cayenne Pepper" });
            item.Menus.Add(new Menu { ID = 4207, Name = "Cheese" });
            item.Menus.Add(new Menu { ID = 4208, Name = "Chopped Green Pepper" });
            repo.Save(item);
            int initialMenuCount = item.Menus.Count();

            Menu LastMenu = item.Menus.LastOrDefault();
            item.Menus.RemoveAt(initialMenuCount - 1);
            bool IsLastMenuStillThere = item.Menus.Contains(LastMenu);

            Assert.AreEqual(initialMenuCount - 1, item.Menus.Count());
            Assert.IsFalse(IsLastMenuStillThere);
        }

        private static void DetachTheLastRecipeChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Recipes = new List<Recipe>();
            item.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            item.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            item.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            item.Recipes.Add(new Recipe { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save(item);
            int initialRecipeCount = item.Recipes.Count();

            Recipe LastRecipe = item.Recipes.LastOrDefault();
            item.Recipes.RemoveAt(initialRecipeCount - 1);
            bool IsLastRecipeStillThere = item.Recipes.Contains(LastRecipe);

            Assert.AreEqual(initialRecipeCount - 1, item.Recipes.Count());
            Assert.IsFalse(IsLastRecipeStillThere);
        }

        private static void DetachTheLastShoppingListChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.ShoppingLists = new List<ShoppingList>();
            item.ShoppingLists.Add(new ShoppingList { ID = 4005, Name = "Butter" });
            item.ShoppingLists.Add(new ShoppingList { ID = 4006, Name = "Cayenne Pepper" });
            item.ShoppingLists.Add(new ShoppingList { ID = 4007, Name = "Cheese" });
            item.ShoppingLists.Add(new ShoppingList { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save((T)item);
            int initialShoppingListCount = item.ShoppingLists.Count();

            ShoppingList LastShoppingList = item.ShoppingLists.LastOrDefault();
            item.ShoppingLists.RemoveAt(initialShoppingListCount - 1);
            bool IsLastShoppingListStillThere = item.ShoppingLists.Contains(LastShoppingList);

            Assert.AreEqual(initialShoppingListCount - 1, item.ShoppingLists.Count());
            Assert.IsFalse(IsLastShoppingListStillThere);
        }

    }
}
