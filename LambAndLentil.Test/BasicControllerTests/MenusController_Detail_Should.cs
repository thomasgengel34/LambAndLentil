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
        public void BeSuccessfulWithValidMenuID()
        {  
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
            Assert.AreEqual("Here it is!", adr.Message);
        }

        
           
        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()=> 
          
            Assert.Fail(); 

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID() => 
            
            Assert.Fail(); 

         


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsWorksWithValidRecipeID()
        { 
            ActionResult ar = Controller.Details(ListEntity.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
        }
         
         
         
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void MenusCtr_DetailsMenuIDIsNotANumber() { }

        //[TestMethod]
        //public void MenusCtr_DetailsMenuIDIsNotAInteger() { } 

    }
}
