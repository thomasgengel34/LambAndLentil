using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Entities;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsController_IngredientsGenericController_Should : IngredientsController_Test_Should
    { 
         

        [TestMethod]
        public void InheritIIngredientsControllerAsync()
        { 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true).GetInterface("IIngredientsControllerAsync");
            
            Assert.IsNotNull(type);
        }

        [TestMethod]
        public void CallRepositoryInIngredient()
        { 
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
           
            Assert.AreEqual("Ingredient", name);
        }
    }
}
