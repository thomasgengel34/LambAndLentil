
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class MenusController_Edit_Should:MenusController_Test_Should
    {


        [Ignore]
        [TestMethod]
        public void CorrectMenusAreBoundInEdit() => Assert.Fail();

        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditMenu()
        { 
            Menu menu = new Menu
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            Repo.Save(menu);
             
            menu.Name = "Name has been changed";
            Repo.Save(menu);
           AlertDecoratorResult adr =  (AlertDecoratorResult)Controller.Edit(1);
            ViewResult vr = (ViewResult)adr.InnerResult;
            Menu returnedMenu = (Menu)vr.Model;
             
            Assert.IsNotNull(returnedMenu);
            Assert.AreEqual("Name has been changed", returnedMenu.Name); 
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenu()
        {
            // Arrange
            IGenericController<Menu> indexController = new MenusController(Repo);
            IGenericController<Menu> Controller2 = new MenusController(Repo);
            IGenericController<Menu> Controller3 = new MenusController(Repo);


            Menu vm = new Menu
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test MenusControllerShould.SaveEditedMenu"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit(vm);
            ViewResult view2 = (ViewResult)Controller3.Index();
            ListEntity<Menu> ListEntity2 = (ListEntity<Menu>)view2.Model;
            Menu vm3 = (from m in ListEntity2.ListT
                        where m.Name == "0000 test Edited"
                        select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID);

        }

        //[Ignore]  look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditMenu()
        {
            // Arrange
            Menu menu = new Menu
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            Repo.Save(menu);

            // Act 
            menu.Name = "Name has been changed";
            Repo.Save(menu);

            ViewResult view1 = (ViewResult)Controller.Edit(1);

            Menu returnedMenuListEntity = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedMenuListEntity.Name);
            Assert.AreEqual(menu.Description, returnedMenuListEntity.Description);
            Assert.AreEqual(menu.CreationDate, returnedMenuListEntity.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentMenu()
        {
            // Arrange

            // Act
            Menu result = (Menu)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateReturnsNonNull()
        {
            // Arrange 

            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
