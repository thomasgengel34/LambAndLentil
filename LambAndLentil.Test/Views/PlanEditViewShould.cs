using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.Views
{
    [Ignore]
    [TestClass]
    public class PlanEditViewShould
    {
        [TestMethod]
        public void GiveMessageToChooseIngredientWhenAddIngredientButtonIsClickedWithoutChoosingIngredientPresents()
        {
            //  if you try to add an ingredient but you have not chosen one and you press the add ingredient button, you get a message saying please choose an ingredient, not an error or otherwise graceful handling
            Assert.Fail();
        }

        [TestMethod]
        public void AttachIngredients()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DetachIngredients()
        {
            Assert.Fail();
        }


        [TestMethod]
        public void AttachRecipe()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DetachRecipe()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CorrectlyListIngredientsAfterRecipeIsAttached()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CorrectlyListIngredientsAfterRecipeIsDetached()
        {
            Assert.Fail();
        }


        [TestMethod]
        public void AttachMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DetachMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CorrectlyListIngredientsAfterMenuIsAttached()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CorrectlyListIngredientsAfterMenuIsDetached()
        {
            Assert.Fail();
        }
    }
}
