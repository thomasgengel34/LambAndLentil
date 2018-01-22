using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("IngredientsController")]
    [TestCategory("Details")]
    public class IngredientsController_Detail_Should:IngredientsController_Test_Should
    { 
        public IngredientsController_Detail_Should()
        {
             
        }
         
        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithNullResult()
        {  
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Delete(400);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
 
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        }

        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithEmptyResult()=>
            BaseReturnDeleteWithActionMethodDeleteWithEmptyResult(Controller); 


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {  
            int count = Repo.Count();
             
            Controller.DeleteConfirmed(int.MaxValue);
            Ingredient item = Repo.GetById(int.MaxValue);
         
            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(item);
            //   Assert.Fail();  // TODO: make sure the correct item was deleted before removing this line 
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithIDNotFound()
        {  
            
            ActionResult ar=Controller.DeleteConfirmed(4000 );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar; 
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Ingredient was not found", adr.Message);
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
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        } 

        [TestMethod]
        public void BeSuccessfulWithValidIngredientID()
        {
            // sut = system.under.test 
            Ingredient sut = new Ingredient { ID = 60000 };
         
            Repo.Save(sut); 
            ActionResult ar = Controller.Details(sut.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            ViewResult viewResult = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(),  viewResult.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass); 
        }
     
         

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroViewIsNotNull()
        { 
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
             
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
            Assert.AreEqual("No Ingredient was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroAlertClassIsCorrect()
        {
            // Arrange
           
            Ingredient item = new Ingredient { ID = 0 };
            Repo.Save(item);


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
            Ingredient item = new Ingredient { ID = 0 };
            Repo.Save(item);


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
            Ingredient item = new Ingredient { ID = -1 };
            Repo.Save(item);

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

            item.ID = -500;
            Repo.Save(item);

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
            Ingredient item = new Ingredient
            {
                ID = -1,
                Name = "Details_IngredientIDIsNegative_AlertClassCorrect"
            };
            Repo.Save(item);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

       
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()
        { // return result as ViewResult;

            // Arrange

            // Act
            ActionResult ar = Controller.Edit(int.MaxValue );
            ViewResult vr = (ViewResult)ar;
            int returnedID = ((Ingredient)(vr.Model)).ID;

            // Assert
            // Assert.Fail();
            Assert.AreEqual("Details", vr.ViewName);
            Assert.AreEqual(int.MaxValue, returnedID);
           
        }

       
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID()
        {  
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Edit(8000);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(1, rtrr.RouteValues.Count, 1);
            Assert.AreEqual("Index", rtrr.RouteValues.Values.ElementAt(0).ToString());
        }


        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void IngredientsCtr_DetailsIngredientIDIsNotANumber() { }

        //[TestMethod]
        //public void IngredientsCtr_DetailsIngredientIDIsNotAInteger() { } 

    }
}
