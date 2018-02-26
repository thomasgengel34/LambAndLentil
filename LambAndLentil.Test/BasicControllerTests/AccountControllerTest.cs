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
    internal class AccountControllerTest
    {

        [TestMethod]
        private static void AccountCtr_InheritsFromBaseController()
        {
            AccountController testController = new AccountController();

            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testController);

            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        private static void AccountCtr_IsPublic()
        {
            AccountController testController = new AccountController();

            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            Assert.AreEqual(isPublic, true);
        }
        /// <summary>
        /// this really needs other paths explored
        /// </summary>
        [TestMethod]
        private static void AccountCtr_LoginInvalidLoginAttempt()
        {
            AccountController testController = new AccountController();

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "foo";

            Task<ViewResult> result = GetActionResult(testController, model, returnUrl);

            Assert.IsNotNull(result);
        }

        // TODO: additional tests

        private async static Task<ViewResult> GetActionResult(AccountController Controller, LoginViewModel model, string returnUrl)
        {
            Task<ActionResult> task = Controller.Login(model, returnUrl);
            ViewResult result = await task as ViewResult;
            return result;
        }
    }
}
