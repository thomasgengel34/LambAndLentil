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
         


        
         
        internal ShoppingList GetShoppingList(IRepository<ShoppingList> repo, string description)
        {

            ShoppingList.ID = int.MaxValue;
            ShoppingList.Description = description;
            controller.PostEdit(ShoppingList);

            ShoppingList result = (from m in repo.GetAll()
                                   where m.Description == ShoppingList.Description
                                   select m).AsQueryable().FirstOrDefault();
            return result;
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
