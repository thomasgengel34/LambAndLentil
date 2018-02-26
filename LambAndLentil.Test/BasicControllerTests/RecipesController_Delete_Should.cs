using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Delete")]
   internal class RecipesController_Delete_Should: RecipesController_Test_Should
    {
         
        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        { 
            ActionResult result = controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "Recipes", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteValidRecipe()
        { 
            Recipe recipeVM = new Recipe { ID = 2, Name = "Test2" };
            repo.Save(recipeVM);
            int repoCount = repo.Count(); 
             
            ActionResult result = controller.DeleteConfirmed(recipeVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;
            int newrepoCount = repo.Count();
 
            Assert.AreEqual(repoCount - 1, newrepoCount);
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }
    }
}
