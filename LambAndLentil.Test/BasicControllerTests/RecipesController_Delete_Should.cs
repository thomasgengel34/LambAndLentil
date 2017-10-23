using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Delete")]
    public class RecipesController_Delete_Should: RecipesController_Test_Should
    {
        public RecipesController_Delete_Should()
        {

        }
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIDetailsWhenIDIstFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }






        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundRecipe()
        {
            // Arrange


            // Act 
            var view = Controller.Delete(int.MaxValue - 1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);


        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidRecipe()
        {
            // Arrange 
            ClassCleanup();
            // Act 
            ActionResult ar = Controller.Delete(int.MaxValue - 6) as ViewResult;   // does not exist
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert

            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

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
            Repo.Add(recipeVM);
            int repoCount = Repo.Count();

            // Arrange - create the Controller
            RecipesController Controller = new RecipesController(Repo);

            // Act - delete the recipe
            ActionResult result = Controller.DeleteConfirmed(recipeVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;
            int newRepoCount = Repo.Count();
            // Assert
            Assert.AreEqual(repoCount - 1, newRepoCount);
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }
    }
}
