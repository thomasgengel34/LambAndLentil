using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusController_Test_Should : BaseControllerTest<Menu>
    { 
        internal static MenusController Controller { get; set; }
        internal static Menu Menu { get; set; }

        public MenusController_Test_Should()
        {
            Controller = new MenusController(Repo)
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
        }
    }
}
