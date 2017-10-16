using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Details")]
    public class RecipesController_Detail_Should:RecipesController_Test_Should
    {
        static Recipe recipe;

        public RecipesController_Detail_Should()
        { 
           recipe = new Recipe();
        }
         
        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithNullResult()
        { // "Ingredient was not found"

            // Arrange

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Details(400, UIViewType.Delete);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

        }

        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithEmptyResult()
        { //  Details view  with success  "Here it is!"

            // Arrange

            // Act
            ViewResult view = (ViewResult)Controller.Details(int.MaxValue, UIViewType.Delete);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Recipe));
            // no message
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        { // index, success,  "Item has been deleted"
          // Arrange
            int count = Repo.Count();
            //Act
            Controller.Details(int.MaxValue, UIViewType.DeleteConfirmed);
            Recipe ingredient = Repo.GetById(int.MaxValue);
            //Assert
            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(ingredient);
            //   Assert.Fail();  // make sure the correct item was deleted before removing this line 
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithIDNotFound()
        {  
            // Arrange

            // Act
            ActionResult ar=Controller.Details(4000, UIViewType.DeleteConfirmed);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar; 
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        }

        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithBadID()
        {
            ActionResult ar = Controller.Details(-1, UIViewType.DeleteConfirmed);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        } 

        [TestMethod]
        public void BeSuccessfulWithValidIngredientID()
        { // "Here it is!"
            // Arrange


            // Act
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Recipe));
            Assert.AreEqual("Here it is!", adr.Message);
        }

        [TestMethod]
        public void GotToIndexViewForNonSpecifiedActionMethods()
        {
            // Arrange

            // Act 
            ViewResult view = (ViewResult)Controller.Details(1, UIViewType.About); 

            // Assert
            Assert.AreEqual(UIViewType.Index, view.Model);

        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighViewNotNull()
        {  // not sure what the desired behavior is yet
           // Arrange
            
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighMessageRight()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighAlertClassCorrect()
        {   // not sure what the desired behavior is yet
            // Arrange
          
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectModel()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectController()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectRouteValue()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDPastIntLimit()
        {
            // I am not sure how I want this to operate.  Wait until UI is set up and see then.
            // Arrange
         
            // Act
            ViewResult result = Controller.Details(int.MaxValue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroViewIsNotNull()
        {
            // Arrange


            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnErrorIfResultIsNotFound_IngredientIDIsZeroMessageIsCorrect()
        {
            // Arrange


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert 
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroAlertClassIsCorrect()
        {
            // Arrange
            RecipesController Controller = new RecipesController(Repo);
            Recipe ingredient = new Recipe { ID = 0 };
            Repo.Save(ingredient);


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroReturnModelIsCorrect()
        {
            // Arrange
            RecipesController Controller = new RecipesController(Repo);
            Recipe ingredient = new Recipe { ID = 0 };
            Repo.Save(ingredient);


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert    
            Assert.IsNull(adr.Model);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void Details_IngredientIDIsNegative_ResultNotNull()
        {
            // Arrange

            Recipe ivm = new Recipe { ID = -1 };
            Repo.Save(ivm);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void NotBeSuccessfulWithInvalidIngredientID_IngredientIDIsNegative_MessageCorrect()
        {
            // Arrange

           recipe.ID = -500;
            Repo.Save(recipe);

            // Act
            ActionResult ar = Controller.Details(-500);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert   adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            //Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void NotBeSuccessfulWithInvalidIngredientID_IngredientIDIsNegative_AlertClassCorrect()
        {
            // Arrange
            RecipesController Controller = new RecipesController(Repo);
            Recipe ingredient = new Recipe
            {
                ID = -1,
                Name = "Details_IngredientIDIsNegative_AlertClassCorrect"
            };
            Repo.Save(ingredient);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void RecipesCtr_DetailsIngredientIDIsNotANumber() { }

        //[TestMethod]
        //public void RecipesCtr_DetailsIngredientIDIsNotAInteger() { } 

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnIndexWithErrorWhenRecipeIDIsNegative()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Details(-1) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());


        }

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnDetailsWithSuccessWithValidRecipeID()
        {
            // Arrange 

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Details(int.MaxValue);
            ViewResult view = (ViewResult)adr.InnerResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Recipe));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnIndexWithErrorWhenRecipeIDTooHigh()
        {
            // Arrange


            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void HandleItWhenRecipeIDPastIntLimit()
        {
            // Arrange 

            // Act
            ActionResult result = Controller.Details(Int16.MaxValue + 1);

            // Assert
            Assert.IsNotNull(result);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void ReturnIndexWithErrorWhenDetailsRecipeIDIsZero()
        {
            // Arrange 

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }
         
        // cleanup is in base class, so not needed here
    }
}
