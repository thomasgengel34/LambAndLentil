using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Concrete;

namespace IngredientControllerTests
{
    [TestClass]
    [TestCategory("Integration")]
    public class CreateMethodShould
    {
        [TestMethod]
        public void CreateAnIngredient()
        {
            // Arrange
             EFRepository repo = new EFRepository(); ;
             IngredientsController controller = new IngredientsController(repo); 
            // Act
           controller.Create(LambAndLentil.UI.UIViewType.Create);
            // Assert

        }
    }
}
