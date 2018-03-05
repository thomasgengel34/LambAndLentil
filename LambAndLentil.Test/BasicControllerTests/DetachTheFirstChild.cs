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

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class DetachTheFirstChild<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            DetachTheFirstIngredientChild();
            DetachTheFirstMenuChild();
            DetachTheFirstPlanChild();
            DetachTheFirstRecipeChild();
            DetachTheFirstShoppingListChild(); 
        }



        private static void DetachTheFirstIngredientChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Ingredients = new List<Ingredient>();
            item.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            item.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            item.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            item.Ingredients.Add(new Ingredient { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save(item);
            int initialIngredientCount = item.Ingredients.Count();

            Ingredient FirstIngredient = item.Ingredients.FirstOrDefault();
            item.Ingredients.RemoveAt(0);
            bool IsFirstIngredientStillThere = item.Ingredients.Contains(FirstIngredient);

            Assert.AreEqual(initialIngredientCount - 1, item.Ingredients.Count());
            Assert.IsFalse(IsFirstIngredientStillThere);
            // TODO: flesh out tests
        }
 

            private static void DetachTheFirstPlanChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Plans = new List<Plan>();
            item.Plans.Add(new Plan { ID = 4100, Name = "Laura" });
            item.Plans.Add(new Plan { ID = 4101, Name = "Ingalls" });
            item.Plans.Add(new Plan { ID = 4102, Name = "Wilder" });
            item.Plans.Add(new Plan { ID = 4103, Name = "Rose" });


            repo.Save(item);
            int initialPlanCount = item.Plans.Count();

            Plan FirstPlan = item.Plans.FirstOrDefault();
            item.Plans.RemoveAt(0);
            bool IsFirstPlanStillThere = item.Plans.Contains(FirstPlan);

            Assert.AreEqual(initialPlanCount - 1, item.Plans.Count());
            Assert.IsFalse(IsFirstPlanStillThere); 
        }

        private static void DetachTheFirstMenuChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Menus = new List<Menu>();
            item.Menus.Add(new Menu { ID = 4205, Name = "Butter" });
            item.Menus.Add(new Menu { ID = 4206, Name = "Cayenne Pepper" });
            item.Menus.Add(new Menu { ID = 4207, Name = "Cheese" });
            item.Menus.Add(new Menu { ID = 4208, Name = "Chopped Green Pepper" });
            repo.Save(item);
            int initialMenuCount = item.Menus.Count();

            Menu FirstMenu = item.Menus.FirstOrDefault();
            item.Menus.RemoveAt(0);
            bool IsFirstMenuStillThere = item.Menus.Contains(FirstMenu);

            Assert.AreEqual(initialMenuCount - 1, item.Menus.Count());
            Assert.IsFalse(IsFirstMenuStillThere);
        }

        private static void DetachTheFirstRecipeChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Recipes = new List<Recipe>();
            item.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            item.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            item.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            item.Recipes.Add(new Recipe { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save(item);
            int initialRecipeCount = item.Recipes.Count();

            Recipe FirstRecipe = item.Recipes.FirstOrDefault();
            item.Recipes.RemoveAt(0);
            bool IsFirstRecipeStillThere = item.Recipes.Contains(FirstRecipe);

            Assert.AreEqual(initialRecipeCount - 1, item.Recipes.Count());
            Assert.IsFalse(IsFirstRecipeStillThere);
        }

        private static void DetachTheFirstShoppingListChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.ShoppingLists = new List<ShoppingList>();
            item.ShoppingLists.Add(new ShoppingList { ID = 4005, Name = "Butter" });
            item.ShoppingLists.Add(new ShoppingList { ID = 4006, Name = "Cayenne Pepper" });
            item.ShoppingLists.Add(new ShoppingList { ID = 4007, Name = "Cheese" });
            item.ShoppingLists.Add(new ShoppingList { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save((T)item);
            int initialShoppingListCount = item.ShoppingLists.Count();

            ShoppingList FirstShoppingList = item.ShoppingLists.FirstOrDefault();
            item.ShoppingLists.RemoveAt(0);
            bool IsFirstShoppingListStillThere = item.ShoppingLists.Contains(FirstShoppingList);

            Assert.AreEqual(initialShoppingListCount - 1, item.ShoppingLists.Count());
            Assert.IsFalse(IsFirstShoppingListStillThere);
        }

    }
}
