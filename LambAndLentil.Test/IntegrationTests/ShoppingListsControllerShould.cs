using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerShould : BaseControllerTest<ShoppingList>
    {
        private static IGenericController<ShoppingList> Controller1, Controller2, Controller3, Controller4;
        private static  ShoppingList ShoppingList;


        public ShoppingListsControllerShould()
        {
            Repo = new TestRepository<ShoppingList>();
            Controller = new ShoppingListsController(Repo);
            Controller1 = new ShoppingListsController(Repo);
            Controller2 = new ShoppingListsController(Repo);
            Controller3 = new ShoppingListsController(Repo);
            Controller4 = new ShoppingListsController(Repo);
            ShoppingList = new ShoppingList
            {
                ID = 400,
                Name = "ShoppingListsControllerShould"
            };
            Repo.Save((ShoppingList)ShoppingList);
        }

         

        [Ignore]
        [TestMethod]
        public void SaveAValidShoppingList()
        { 
            ShoppingList.Name = "test";
        
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit((ShoppingList)ShoppingList);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
              
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.ShoppingLists.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("ShoppingLists", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithNameChange()
        {
            // Arrange 

            ShoppingList.Name = "0000 test";

            // Act 
            ActionResult ar1 = Controller1.PostEdit((ShoppingList)ShoppingList);
            ViewResult view1 = (ViewResult)Controller2.Index();
            IEnumerable<ShoppingList> ListEntity = (IEnumerable<ShoppingList>)view1.Model;
            var result = (from m in ListEntity
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            ShoppingList item = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", item.Name);


            // now edit it
            ShoppingList.Name = "0000 test Edited";
            ShoppingList.ID = item.ID;
            ActionResult ar2 = Controller3.PostEdit((ShoppingList)ShoppingList);
            ViewResult view2 = (ViewResult)Controller4.Index();
            List<ShoppingList> ListEntity2 = (List<ShoppingList>)view2.Model;
            ShoppingList result2 = (from m in ListEntity2
                                    where m.Name == "0000 test Edited"
                                    select m).AsQueryable().FirstOrDefault();



            // Assert
            Assert.AreEqual("0000 test Edited", item.Name);

        }



        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithDescriptionChange()
        {
            // Arrange 
            ShoppingList.Name = "0000 test";
            ShoppingList.Description = "SaveEditedShoppingListWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = Controller1.PostEdit((ShoppingList)ShoppingList);
            ViewResult view1 = (ViewResult)Controller2.Index();
            List<ShoppingList> ListEntity = (List<ShoppingList>)view1.Model;
            ShoppingList shoppingListVM = (from m in ListEntity
                                           where m.Name == "0000 test"
                                           select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Pre-test", shoppingListVM.Description);


            // now edit it
            ShoppingList.ID = shoppingListVM.ID;
            ShoppingList.Name = "0000 test Edited";
            ShoppingList.Description = "SaveEditedShoppingListWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit((ShoppingList)ShoppingList);
            ViewResult view2 = (ViewResult)Controller4.Index();
            List<ShoppingList> ListEntity2 = (List<ShoppingList>)view2.Model;
            var result2 = (from m in ListEntity2
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            shoppingListVM = result2.FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", shoppingListVM.Name);
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Post-test", shoppingListVM.Description);
        }

        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAShoppingListFromTheDatabase()
        {
            // Arrange  
            ShoppingList item = new ShoppingList { ID = 1, Description = "test ActuallyDeleteAShoppingListFromTheDatabase" };

            Repo.Save(item);

            //Act
            Controller.DeleteConfirmed(item.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.ID == item.ID
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            ShoppingList shoppingListVM = new ShoppingList(CreationDate)
            {
                Name = "001 Test "
            };

            JSONRepository<ShoppingList> Repo = new JSONRepository<ShoppingList>(); ;
            //ShoppingListsController ControllerEdit = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerView = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerDelete = new ShoppingListsController(Repo);

            // Act
            //ControllerEdit.PostEdit(shoppingListVM);
            //ViewResult view = ControllerView.Index();
            //ShoppingList ListEntity= (ShoppingList)view.Model;
            // var result = (from m in ListEntity.ShoppingLists
            //              where m.Name == "001 Test "
            //               select m).AsQueryable();

            // ShoppingList shoppingList = result.FirstOrDefault();

            // DateTime shouldBeSameDate = shoppingList.CreationDate;

            // Assert
            //    Assert.AreEqual(CreationDate, shouldBeSameDate);

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            ShoppingList shoppingList = new ShoppingList()
            {
                ID = 6000,
                Name = "Test UpdateTheModificationDateBetweenPostedEdits"
            }; 
            Repo.Save(shoppingList);
            BaseUpdateTheModificationDateBetweenPostedEdits(shoppingList);
        }

        internal ShoppingList GetShoppingList(IRepository<ShoppingList> Repo, string description)
        {

            ShoppingList.ID = int.MaxValue;
            ShoppingList.Description = description;
            Controller.PostEdit((ShoppingList)ShoppingList);

            ShoppingList result = (from m in Repo.GetAll()
                                   where m.Description == ShoppingList.Description
                                   select m).AsQueryable().FirstOrDefault();
            return result;
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingShoppingList()
        {
            IRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            ShoppingList shoppingList = new ShoppingList() { ID = 76, Ingredients = new List<Ingredient>() };
            Repo.Save(shoppingList);
            Ingredient ingredient = new Ingredient { ID = 500 };
            repoIngredient.Save(ingredient);

            Controller.Attach(shoppingList, ingredient);
            ShoppingList returnedShoppingList = Repo.GetById(shoppingList.ID);


            Assert.AreEqual(1, returnedShoppingList.Ingredients.Count());
            // Verify the correct ingredient was added 
            Assert.AreEqual(ingredient.ID, returnedShoppingList.Ingredients.First().ID);

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromShoppingList() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingShoppingList() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingShoppingList() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingShoppingList() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingShoppingList() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingShoppingList() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CreateAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void EditAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayAPrintableShoppingList()
        {

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void DeleteAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DownloadAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        public void CreateACheckableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void EditACheckableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayACheckableShoppingList()
        {

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void DeleteACheckableShoppingList()
        {

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DownloadACheckableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
