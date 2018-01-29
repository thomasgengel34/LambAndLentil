using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    public class BasicPostEdit<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new() 
    {
        public BasicPostEdit()
        {
           T item = new T()
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            Repo.Save(item);
        }
 
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {
            T invalidItem = new T()
            {
                ID = -2
            };

            ActionResult ar = Controller.PostEdit(invalidItem);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
        }

        
        public void CanPostEditShoppingList()
        {  
            item.Name = "Name has been changed";
            Repo.Save(item);
            ViewResult view1 = (ViewResult)Controller.Edit(item.ID);

            T  returnedItem = Repo.GetById(item.ID);
             
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedItem.Name);
            Assert.AreEqual(item.Description, returnedItem.Description);
            Assert.AreEqual(item.CreationDate, returnedItem.CreationDate);
        }

        public void NotChangeIDInPostEditActionMethod()
        {
            int originalID = item.ID;
            item.ID = 7000;
            int initialCount = Repo.Count();

            Controller.PostEdit(item);
            T returnedItem = Repo.GetById(7000);
            T originalItem = Repo.GetById(originalID);
            int newCount = Repo.Count();

            Assert.AreEqual(originalID, originalItem.ID);
            Assert.AreEqual(item.ID, returnedItem.ID);
            Assert.AreEqual(initialCount + 1, newCount);
        }
    }
}
