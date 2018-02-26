﻿using AutoMapper;
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

namespace LambAndLentil.Test.BaseControllerTests
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
            repo = new TestRepository<ShoppingList>();
            controller = new ShoppingListsController(repo);
            Controller1 = new ShoppingListsController(repo);
            Controller2 = new ShoppingListsController(repo);
            Controller3 = new ShoppingListsController(repo);
            Controller4 = new ShoppingListsController(repo);
            ShoppingList = new ShoppingList
            {
                ID = 400,
                Name = "ShoppingListsControllerShould"
            };
            repo.Save((ShoppingList)ShoppingList);
        }

         

        [Ignore]
        [TestMethod]
        public void SaveAValidShoppingList()
        { 
            ShoppingList.Name = "test";
        
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit((ShoppingList)ShoppingList);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
              
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.ShoppingLists.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(1).ToString());
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

            ActionResult ar2 = Controller3.PostEdit(ShoppingList);
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
         
         
        internal ShoppingList GetShoppingList(IRepository<ShoppingList> repo, string description)
        {

            ShoppingList.ID = int.MaxValue;
            ShoppingList.Description = description;
            controller.PostEdit((ShoppingList)ShoppingList);

            ShoppingList result = (from m in repo.GetAll()
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
            repo.Save(shoppingList);
            Ingredient ingredient = new Ingredient { ID = 500 };
            repoIngredient.Save(ingredient);

            controller.Attach(shoppingList, ingredient);
            ShoppingList returnedShoppingList = repo.GetById(shoppingList.ID);


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
        public void CreateAPrintableShoppingList()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void EditAPrintableShoppingList()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayAPrintableShoppingList()
        { 
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void DeleteAPrintableShoppingList()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DownloadAPrintableShoppingList()
        { 
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        public void CreateACheckableShoppingList()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void EditACheckableShoppingList()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayACheckableShoppingList()
        { 
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void DeleteACheckableShoppingList()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DownloadACheckableShoppingList()
        { 
            Assert.Fail();
        }
    }
}
