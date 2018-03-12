using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace  LambAndLentil.Test.BasicTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
   public class IngredientsControllerShould : BaseControllerTest<Ingredient>
    { 
        private static void  InheritIngredientsControllerAsync()
        {
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true).GetInterface("IIngredientsControllerAsync");

            Assert.IsNotNull(type);
        } 
    }
}
