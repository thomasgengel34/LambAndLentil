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
        public void BeSuccessfulWithValidIngredientID()
        {  
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
