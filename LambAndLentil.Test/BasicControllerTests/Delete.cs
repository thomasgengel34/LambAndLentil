using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    internal class Delete<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        { 
            DeleteAFoundShoppingList();
            DeleteAFoundEntity();
            ReturnDeleteWithActionMethodDeleteWithEmptyResult();
        }
         

        private static void DeleteAFoundShoppingList()
        {   // does not actually detach, just sets up to remove it.
            // TODO: verify "Are you sure you want to delete this?" message shows up.
            SetUpForTests(out repo, out controller, out item);
            int count = repo.Count();

            ActionResult ar = controller.Delete(item.ID); 
             
            int newCount = repo.Count(); 

            Assert.AreEqual(count, newCount);
          
            // TODO: flesh out this test
        }


        private static void DeleteAFoundEntity()
        {
            SetUpForTests(out repo, out controller, out item);
            int repoCount = repo.Count();

            ActionResult ar = controller.Delete(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(repoCount, repo.Count());  // shows this does not actually delete anything.  That is done in delete-confirm 
        }


        private static void ReturnDeleteWithActionMethodDeleteWithEmptyResult()
        {
            SetUpForTests(out repo, out controller, out item);
            ActionResult ar = controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;

            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.IsInstanceOfType(vr.Model, typeof(T));

        }
    }
}
