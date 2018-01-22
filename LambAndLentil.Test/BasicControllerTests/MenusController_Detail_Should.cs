using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("MenusController")]
    [TestCategory("Details")]
    public class MenusController_Detail_Should:MenusController_Test_Should
    {
          

        public MenusController_Detail_Should()
        { 
        }
         
        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithNullResult()
        { // "Menu was not found"

            // Arrange

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Delete(400);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

        }

        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithEmptyResult() => BaseReturnDeleteWithActionMethodDeleteWithEmptyResult(Controller);


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        { // index, success,  "Item has been deleted"
          // Arrange
            int count = Repo.Count();
            //Act
            Controller.DeleteConfirmed(int.MaxValue );
            Menu menu = Repo.GetById(int.MaxValue);
            //Assert
            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(menu);
            //   Assert.Fail();  // make sure the correct item was deleted before removing this line 
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithIDNotFound()
        {  
            // Arrange

            // Act
            ActionResult ar=Controller.DeleteConfirmed(4000 );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar; 
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        }

        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithBadID()
        {
            ActionResult ar = Controller.DeleteConfirmed(-1 );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        } 

        [TestMethod]
        public void BeSuccessfulWithValidMenuID()
        { // "Here it is!"
            // Arrange


            // Act
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
            Assert.AreEqual("Here it is!", adr.Message);
        }

       


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDTooHighViewNotNull()
        {  // not sure what the desired behavior is yet
           // Arrange 
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDTooHighMessageRight() =>
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDTooHighAlertClassCorrect()
        {   // not sure what the desired behavior is yet
            // Arrange 
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDTooHighCorrectModel() => 
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true); 


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDTooHighCorrectController() => 
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true); 


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDTooHighCorrectRouteValue() => 
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true); 


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDPastIntLimit()
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
        public void DetailsMenuIDIsZeroViewIsNotNull()
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
        public void ReturnErrorIfResultIsNotFound_MenuIDIsZeroMessageIsCorrect()
        {
            // Arrange


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert 
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDIsZeroAlertClassIsCorrect()
        {
            // Arrange 
            Menu.ID = 0 ;
            Repo.Save((Menu)Menu);


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsMenuIDIsZeroReturnModelIsCorrect()
        {
            // Arrange 
            Menu.ID = 0  ;
            Repo.Save((Menu)Menu);


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert    
            Assert.IsNull(adr.Model);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void Details_MenuIDIsNegative_ResultNotNull()
        {
            // Arrange

            Menu.ID = -1;
            Repo.Save((Menu)Menu);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void NotBeSuccessfulWithInvalidMenuID_MenuIDIsNegative_MessageCorrect()
        {
            // Arrange

            Menu.ID = -500;
            Repo.Save((Menu)Menu);

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
        public void NotBeSuccessfulWithInvalidMenuID_MenuIDIsNegative_AlertClassCorrect()
        {
            // Arrange 
            Menu.ID = -1;
            Menu.Name = "Details_MenuIDIsNegative_AlertClassCorrect"; 
            Repo.Save((Menu)Menu);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()=> 
            // Arrange

            // Act

            // Assert
            Assert.Fail(); 

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID() => 
            // Arrange

            // Act

            // Assert
            Assert.Fail(); 


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsNegative()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsWorksWithValidRecipeID()
        {
            // Arrange 

            // Act 
            ActionResult ar = Controller.Details(ListEntity.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDTooHigh()
        {
            // Arrange

            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDPastIntLimit()
        {
            // Arrange

            // Act
            ViewResult result = Controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsZero()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void MenusCtr_DetailsMenuIDIsNotANumber() { }

        //[TestMethod]
        //public void MenusCtr_DetailsMenuIDIsNotAInteger() { } 

    }
}
