using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_Test_Should : BaseControllerTest<Plan>
    {
        internal static PlansController Controller { get; set; }
        internal static Plan Plan { get; set; }
        internal static Plan ReturnedPlan { get; set; }

        public PlansController_Test_Should()
        {
            Controller = new PlansController(Repo)
            {
                PageSize = 3
            };
            Plan = new Plan()
            {
                ID = 1234567,
                Description = "PlansController_Test_Should Ctor",
                Ingredients = new List<Ingredient>(),
                Recipes = new List<Recipe>(),
                Menus = new List<Menu>()
            };
            Repo.Save(Plan);
        }
    }
}
