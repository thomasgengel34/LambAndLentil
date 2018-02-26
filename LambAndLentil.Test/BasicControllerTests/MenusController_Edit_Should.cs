
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
   internal class MenusController_Edit_Should:MenusController_Test_Should
    { 
        [Ignore]
        [TestMethod]
        private static void  CorrectMenusAreBoundInEdit() => Assert.Fail();

        [TestMethod]
        [TestCategory("Edit")]
        private static void CanEditMenu()
        { 
            Menu menu = new Menu
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            repo.Save(menu);
             
            menu.Name = "Name has been changed";
            repo.Save(menu);
           AlertDecoratorResult adr =  (AlertDecoratorResult)controller.Edit(1);
            ViewResult vr = (ViewResult)adr.InnerResult;
            Menu returnedMenu = (Menu)vr.Model;
             
            Assert.IsNotNull(returnedMenu);
            Assert.AreEqual("Name has been changed", returnedMenu.Name); 
        }

        [TestMethod]
        [TestCategory("Edit")]
        private static void  SaveEditedMenu()
        { 
            IGenericController<Menu> indexController = new MenusController(repo);
            IGenericController<Menu> Controller2 = new MenusController(repo);
            IGenericController<Menu> Controller3 = new MenusController(repo);


            Menu vm = new Menu
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test MenusControllerShould.SaveEditedMenu"
            };
             
            ActionResult ar1 = controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit(vm);
            ViewResult view2 = (ViewResult)Controller3.Index();
            ListEntity<Menu> ListEntity2 = (ListEntity<Menu>)view2.Model;
            Menu vm3 = (from m in ListEntity2.ListT
                        where m.Name == "0000 test Edited"
                        select m).AsQueryable().FirstOrDefault();
             
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID); 
        }
         
        
    }
}
