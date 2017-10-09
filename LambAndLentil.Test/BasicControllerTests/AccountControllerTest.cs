using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using System.Threading.Tasks; 

namespace LambAndLentil.Tests.Controllers
{ 
    [TestClass]
    [TestCategory("AccountController")]
    public class AccountControllerTest
    {

        [TestMethod]
        public void AccountCtr_InheritsFromBaseController()
        {
            // Arrange
            AccountController testController = new AccountController();

            // Act 
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testController);

            // Assert 
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        public void AccountCtr_IsPublic()
        {
            // Arrange
            AccountController testController = new AccountController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }
/// <summary>
/// this really needs other paths explored
/// </summary>
        [TestMethod]
        public void AccountCtr_LoginInvalidLoginAttempt()
        {
            // Arrange
           AccountController testController = new AccountController();

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "foo";

             //Act

            Task<ViewResult> result = GetActionResult(testController, model, returnUrl);

            // Assert
            Assert.IsNotNull(result);
        }
         
        // TODO: additional tests

        #region PrivateMethods  

        private async Task<ViewResult> GetActionResult(AccountController Controller, LoginViewModel model, string returnUrl )
        {
            Task<ActionResult> task = Controller.Login(model, returnUrl);
             ViewResult result = await task as ViewResult;
            return result; 
        }
        #endregion

    }
}
