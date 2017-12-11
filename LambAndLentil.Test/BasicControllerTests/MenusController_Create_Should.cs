using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{
 
        [TestCategory("MenusController")]
        [TestCategory("Create")]
        [TestClass]
        public class MenusController_Create_Should : MenusController_Test_Should
        {
            [TestMethod]
            public void ReturnNonNull()
            {
                // Arrange

                // Act
                ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

                // Assert
                Assert.IsNotNull(result);
            }

           

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange

            // Act
            ViewResult view = (ViewResult)Controller.Create(UIViewType.Create);

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

    }
}
