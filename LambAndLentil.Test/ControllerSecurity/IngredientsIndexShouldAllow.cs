using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.ControllerSecurity
{
    [Ignore]
    [TestClass]
    [TestCategory("Security")]
    [TestCategory("IngredientssController")]
    [TestCategory("Index")]
    public class IngredientsIndexShouldAllow
    {
        [TestMethod]
        public void AnonymousUserToSee()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void LoggedInUserToSee()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AdminToSee()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AnonymousUserNotToSeeEditButton()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AnonymousUserNotToSeeCreateButton()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AnonymousUserNotToSeeDeleteButton()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AnonymousUserNotToEdit()
        {  // aimed at not being able to circumvent the button to get to CUD
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AnonymousUserNotToCreate()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void AnonymousUserNotToDelete()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
