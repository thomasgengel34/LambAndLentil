using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]
    [TestCategory("Details")]
    public class ShoppingListsController_Detail_Should:ShoppingListsController_Test_Should
    { 
       
        

        public ShoppingListsController_Detail_Should()
        { 
             
        }
         
        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithNullResult()
        { // "Ingredient was not found"

            // Arrange

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Delete(400);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Shopping List was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

        }

        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithEmptyResult() =>
            BaseReturnDeleteWithActionMethodDeleteWithEmptyResult(Controller); 


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        { 
            int count = Repo.Count();
           
            Controller.DeleteConfirmed(int.MaxValue);
            ShoppingList shoppingList = Repo.GetById(int.MaxValue);
            
            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(shoppingList);
            //   Assert.Fail();  // make sure the correct item was deleted before removing this line 
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithIDNotFound()
        {  
            // Arrange

            // Act
            ActionResult ar=Controller.DeleteConfirmed(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar; 
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Shopping List was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        }

        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithBadID()
        {
            ActionResult ar = Controller.DeleteConfirmed(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Shopping List was not found", adr.Message);
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
            Assert.IsInstanceOfType(view.Model, typeof(ShoppingList));
            Assert.AreEqual("Here it is!", adr.Message);
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
            Assert.AreEqual("No Shopping List was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroAlertClassIsCorrect()
        {
            // Arrange 
            ShoppingList shoppingList = new ShoppingList { ID = 0 };
            Repo.Save(shoppingList);


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
            ShoppingList shoppingList = new ShoppingList { ID = 0 };
            Repo.Save(shoppingList);


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

            ShoppingList ivm = new ShoppingList { ID = -1 };
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

           ShoppingList.ID = -500;
            Repo.Save(ShoppingList);

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

            ShoppingList.ID = -1;
            ShoppingList.Name = "Details_IngredientIDIsNegative_AlertClassCorrect"; 
            Repo.Save(ShoppingList);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID() => 
            // Arrange

            // Act

            // Assert
            Assert.Fail(); 

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteAFoundIngredient()
        {
            BaseDeleteAFoundEntity(Controller);
        }

        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void ShoppingListsCtr_DetailsIngredientIDIsNotANumber() { }

        //[TestMethod]
        //public void ShoppingListsCtr_DetailsIngredientIDIsNotAInteger() { } 

    }
}
