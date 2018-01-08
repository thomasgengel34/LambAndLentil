using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Linq;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{
   
    [TestClass]
    public class ShoppingListsController_Attach_Should:ShoppingListsController_Test_Should
    {
         

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() => 
            BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller); 

        
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild()  => BaseReturnsDetailWithWarningIfAttachingNullChild(ShoppingList, Repo, Controller);
       
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange
            ShoppingList menu = new ShoppingList
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<ShoppingList> mRepo = new TestRepository<ShoppingList>();
            mRepo.Add(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.Attach(Repo,int.MaxValue, ingredient );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            //Assert
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        } 
       
        [TestMethod]
        public void SuccessfullyAttachChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

            // Act
            Controller.Attach(Repo,ShoppingList.ID, child );
            ReturnedShoppingList = Repo.GetById(ShoppingList.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedShoppingList.Ingredients.Last().Name);
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            
        }

        [TestMethod]
        public void SuccessfullyAttachRecipeChild()
        {
            BaseSuccessfullyAttachRecipeChild(ShoppingList, Controller); 
        }

       
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        {
            IGenericController<ShoppingList> DetachController = new ShoppingListsController(Repo);
            BaseSuccessfullyDetachRecipeChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists);
        }
         
         [Ignore]
        [TestMethod]
        public void SuccessfullyAttachPlanChild()
        {
          
        }

      

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachPlanChild()
        {

        }

        [TestMethod] 
        public void DetachASetOfIngredientChildren()
        {
            // Arrange 
            ShoppingList.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            ShoppingList.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            ShoppingList.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            ShoppingList.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save(ShoppingList);
            int initialIngredientCount = ShoppingList.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = ShoppingList.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf<Ingredient>( ShoppingList.ID, selected);
            ShoppingList returnedShoppingList = Repo.GetById(ShoppingList.ID);

            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedShoppingList.Ingredients.Count());
        }

        [TestMethod]
        public void DetachTheLastIngredientChild() =>
            BaseDetachTheLastIngredientChild(Repo, Controller, ShoppingList); 

        [TestMethod] 
        public void DetachAllIngredientChildren() =>  BaseDetachAllIngredientChildren(Repo, Controller ); 

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(ShoppingList, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller, ShoppingList.ID);

    }
}
