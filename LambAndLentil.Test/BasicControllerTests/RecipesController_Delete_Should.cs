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
    public class RecipesController_Delete_Should: RecipesController_Test_Should
    {
        
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        { 
            Assert.Fail();
        }
         
          

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange

            // Act
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "Recipes", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteValidRecipe()
        {
            // Arrange - create an recipe
            Recipe recipeVM = new Recipe { ID = 2, Name = "Test2" };
            Repo.Save(recipeVM);
            int repoCount = Repo.Count(); 

            // Act 
            ActionResult result = Controller.DeleteConfirmed(recipeVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;
            int newRepoCount = Repo.Count();
            // Assert
            Assert.AreEqual(repoCount - 1, newRepoCount);
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }
    }
}
