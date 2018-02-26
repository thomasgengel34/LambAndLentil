using System;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    internal class MenusController_Test_Misc: MenusController_Test_Should
    {
       
        public MenusController_Test_Misc()
        {
            Menu = new Menu { ID = 1000, Name = "Original Name" };
            repo.Save((Menu)Menu);
        }
 
        
         

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectMenuPropertiesAreBoundInEdit()
        {
            Assert.Fail();
        }
         
      

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            Menu.Name = "Name is changed";
            controller.PostEdit((Menu)Menu);
            ReturnedMenu = repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedMenu.Name);
        }
    }
}
