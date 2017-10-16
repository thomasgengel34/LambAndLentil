using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestClass]
    public class IngredientsController_Attach_Should : IngredientsController_Test_Should
    {

        // Ingredient cannot attach an ingredient.
        // this is to change
        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachIngredientChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachIngredientChild()
        {

        }


    }
}
