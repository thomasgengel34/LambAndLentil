using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]
    [TestCategory("Details")]
    public class ShoppingListsController_Detail_Should:ShoppingListsController_Test_Should
    {  
        [TestMethod]
        public void BeSuccessfulWithValidIngredientID()
        {  
            ActionResult ar = controller.Details(int.MaxValue);
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
            ActionResult ar = controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert 
            Assert.AreEqual("No Shopping List was found with that id.", adr.Message);
        } 
    }
}
