using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("IngredientsController")]
    [TestCategory("Create")]
    [TestClass]
    public class IngredientsController_Create_Should : IngredientsController_Test_Should
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
