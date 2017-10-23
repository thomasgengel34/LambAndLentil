using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Create")]
    [TestClass]
    public class PlansController_Create_Should:PlansController_Test_Should
    {
        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange

            ViewResult view = Controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }
    }
}
