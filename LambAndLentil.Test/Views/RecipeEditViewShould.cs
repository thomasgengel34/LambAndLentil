using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.Views
{
    [Ignore]
    [TestClass]
    public class RecipeEditViewShould
    {
        [TestMethod]
        public void NotReturnAServerErrorPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GiveMessageToChooseIngredientWhenAddIngredientButtonIsClickedWithoutChoosingIngredientPresents()
        {
            //  if you try to add an ingredient but you have not chosen one and you press the add ingredient button, you get a message saying please choose an ingredient, not an error or otherwise graceful handling

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void AttachIngredient()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DetachIngredient()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ChangeIngredientUnitAndCorrectlyRecalculateTotal()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void HaveCorrectFormatting()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void IndicateInPageTitleCorrectlyIfItIsFromCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void IndicateInPageTitleCorrectlyIfItIsFromEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
