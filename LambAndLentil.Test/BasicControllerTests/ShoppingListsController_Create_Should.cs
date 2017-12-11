using LambAndLentil.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class ShoppingListsController_Create_Should:ShoppingListsController_Test_Should
    {
        [TestMethod]
        [TestCategory("ShooppingListsController")] 
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange 
            ViewResult view = (ViewResult)Controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }
    }
}
