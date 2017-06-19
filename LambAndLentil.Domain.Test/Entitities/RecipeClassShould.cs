using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq; 

namespace LambAndLentil.Domain.Test.Entities

{
    [TestClass]
    [TestCategory("Recipe")]
    public class RecipetClassShould { 

        [TestMethod]
        public void  AddNewLines() {

            // Arrange - create some test products
            Ingredient p1 = new Ingredient { ID = 1, Name = "P1" };
            Ingredient p2 = new Ingredient { ID = 2, Name = "P2" };

            // Arrange - create a new cart
            Recipe target = new Recipe();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Ingredient, p1);
            Assert.AreEqual(results[1].Ingredient, p2);
        }

        [TestMethod]
        public void  AddQuantityForExisting_Lines() {

            // Arrange - create some test products
            Ingredient p1 = new Ingredient { ID = 1, Name = "P1" };
            Ingredient p2 = new Ingredient { ID = 2, Name = "P2" };

            // Arrange - create a new cart
            Recipe target = new Recipe();

            // Act
            target.AddItem(p1, 0);
            target.AddItem(p2, 0);
            target.AddItem(p1, 0);
            CartLine[] results = target.Lines.OrderBy(c => c.Ingredient.ID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 0);
            Assert.AreEqual(results[1].Quantity, 0);
        }

        [TestMethod]
        public void  RemoveLine() {

            // Arrange - create some test products
            Ingredient p1 = new Ingredient { ID = 1, Name = "P1" };
            Ingredient p2 = new Ingredient { ID = 2, Name = "P2" };
            Ingredient p3 = new Ingredient { ID = 3, Name = "P3" };

            // Arrange - create a new cart
            Recipe target = new Recipe();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Ingredient == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

       

        [TestMethod]
        public void  ClearContents() {

            // Arrange - create some test products
            Ingredient p1 = new Ingredient { ID = 1, Name = "P1",  };
            Ingredient p2 = new Ingredient { ID = 2, Name = "P2"  };

            // Arrange - create a new cart
            Recipe target = new Recipe();

            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act - reset the cart
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        } 
    }
}
