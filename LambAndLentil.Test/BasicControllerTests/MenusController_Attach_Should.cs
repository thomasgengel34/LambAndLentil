using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class MenusController_Attach_Should:MenusController_Test_Should
    { 
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {  
            Menu menu = new Menu
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Menu> mRepo = new TestRepository<Menu>();
            mRepo.Save(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.Attach(int.MaxValue, ingredient );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
      
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString()); 
        }

          
        

       
        [TestMethod]
        public void SuccessfullyAttachChild()
        { 
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);
 
            Controller.Attach( Menu.ID, child );
              
            ReturnedMenu = Repo.GetById(Menu.ID);
             
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedMenu.Ingredients.Last().Name);
        }

       
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        { 
            Menu.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Menu.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Menu.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Menu.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Menu)Menu);
            int initialIngredientCount = Menu.Ingredients.Count();
             
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = Menu.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf(Menu.ID, selected);
            Menu returnedMenu = Repo.GetById(Menu.ID);
             
            Assert.AreEqual(initialIngredientCount - 2, returnedMenu.Ingredients.Count());
        }
 
    }
}
