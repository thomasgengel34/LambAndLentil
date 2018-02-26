using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    internal class MenusController_Test_Should : BaseControllerTest<Menu>
    { 
        
        internal static IMenu Menu { get; set; }
        public IMenu ReturnedMenu { get; set; }

        public MenusController_Test_Should()
        {
            controller = new MenusController(repo)
            { 
                PageSize = 3 
            };

            Menu = new Menu()
            {
                ID = 1234567,
                Description = "MenusController_Test_Should Ctor",
            MealType=MealType.Lunch,
            DayOfWeek=System.DayOfWeek.Friday,
            Diners=4,
            Ingredients=new List<Ingredient>(),
            Recipes= new List<Recipe>()};
            repo.Save((Menu)Menu);
        }
    }
}
