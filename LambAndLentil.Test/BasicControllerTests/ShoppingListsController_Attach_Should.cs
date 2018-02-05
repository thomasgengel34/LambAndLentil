using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class ShoppingListsController_Attach_Should:ShoppingListsController_Test_Should
    { 
        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent();

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() => 
            BaseReturnsIndexWithWarningWithUnknownParentID(); 

        
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild()  => BaseReturnsDetailWithWarningIfAttachingNullChild();
       
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        { 
            ShoppingList  shoppingList= new ShoppingList
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<ShoppingList> mRepo = new TestRepository<ShoppingList>();
            mRepo.Save(shoppingList);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
             
            ActionResult ar = Controller.Attach(shoppingList, ingredient );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
             
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
            Assert.Fail();
        } 
       
        [TestMethod]
        public void SuccessfullyAttachChild()
        { 
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);
             
            Controller.Attach(ShoppingList, child );
            ReturnedShoppingList = Repo.GetById(ShoppingList.ID);
          
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
            BaseSuccessfullyDetachRecipeChild(Controller, DetachController);
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
            //ShoppingList.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            //ShoppingList.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            //ShoppingList.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            //ShoppingList.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            //Repo.Save(ShoppingList);
            //int initialIngredientCount = ShoppingList.Ingredients.Count();
             
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = ShoppingList.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            //Controller.DetachASetOf( ShoppingList, selected);
            //ShoppingList returnedShoppingList = Repo.GetById(ShoppingList.ID);
             
            //Assert.AreEqual(initialIngredientCount - 2, returnedShoppingList.Ingredients.Count());
        }

        [TestMethod]
        public void DetachTheLastIngredientChild() =>
            BaseDetachTheLastIngredientChild(Controller, ShoppingList);
         
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(ShoppingList, Controller);

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild();

    }
}
