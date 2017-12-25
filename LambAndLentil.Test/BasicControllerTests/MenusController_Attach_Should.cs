using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Linq;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using System.Collections.Generic;
using LambAndLentil.UI.Models;

namespace LambAndLentil.Test.BasicControllerTests
{
    
    [TestClass]
    public class MenusController_Attach_Should:MenusController_Test_Should
    {
         
        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Menu, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild(Menu, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        { 
            // Arrange
            Menu menu = new Menu
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Menu> mRepo = new TestRepository<Menu>();
            mRepo.Add(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.Attach<Ingredient>(Repo, int.MaxValue, ingredient,UI.Models.AttachOrDetach.Attach);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
     
            //Assert
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString()); 
        }

          
        

       
        [TestMethod]
        public void SuccessfullyAttachChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

            // Act
            Controller.Attach(Repo, Menu.ID, child, AttachOrDetach.Attach);
              
            ReturnedMenu = Repo.GetById(Menu.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedMenu.Ingredients.Last().Name);
        }

       [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            //IGenericController<Menu> DetachController = new MenusController(Repo);
            //BaseSuccessfullyDetachChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists);
        }

        
        [TestMethod]
        public void SuccessfullyAttachRecipeChild() => 
             BaseSuccessfullyAttachRecipeChild(Menu,Controller); 
      



        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        {
            // Arrange 
            Menu.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Menu.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Menu.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Menu.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Menu)Menu);
            int initialIngredientCount = Menu.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = Menu.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf(Menu.ID, selected);
            Menu returnedMenu = Repo.GetById(Menu.ID);

            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedMenu.Ingredients.Count());
        }

        [TestMethod]
        public void DetachTheLastIngredientChild() => BaseDetachTheLastIngredientChild(Repo, Controller, Menu);

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachAllIngredientChildren() => BaseDetachAllIngredientChildren(Repo, Controller, Menu);

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() =>
            BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller); 

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller, Menu.ID);
    }
}
