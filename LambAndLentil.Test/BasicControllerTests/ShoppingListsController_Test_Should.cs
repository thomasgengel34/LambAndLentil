using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_Test_Should : BaseControllerTest<ShoppingList>
    {
        internal static ShoppingListsController Controller { get; set; }
        internal static ShoppingList ShoppingList { get; set; }
        internal static ShoppingList ReturnedShoppingList { get; set; }

        public ShoppingListsController_Test_Should()
        {
            Controller = new ShoppingListsController(Repo)
            {
                PageSize = 3
            };
            ShoppingList = new ShoppingList()
            {
                ID = 1234567,
                Description = "ShoppingListsController_Test_Should Ctor",
                Date = new  DateTime(2017, 10, 12),
                Author="John Doe",
                Ingredients = new List<Ingredient>(),
                Recipes = new List<Recipe>(),
                Menus = new List<Menu>(),
                Plans = new List<Plan>(),
            };
            Repo.Save(ShoppingList);
        } 
    }
}
