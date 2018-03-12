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
    internal class Edit<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            CanSetUpToEdit(); 
            ReturnIndexWithWarningForNonexistentIngredient();
        }



        private static void ReturnIndexWithWarningForNonexistentIngredient()
        {
            SetUpForTests(out repo, out controller, out item);
            repo.Remove(item);
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Edit(item.ID);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual("No " + item.DisplayName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }//RedirectToAction(UIViewType.Index.ToString()).WithError("No " + className + " was found with that id.");

        private static void CorrectPlansAreBoundInEdit()
        {
            Assert.Fail();
        }


        private static void CanSetUpToEdit()
        {
            SetUpForTests(out repo, out controller, out item);
            int id = item.ID;
            string name = item.Name; 
           
            AlertDecoratorResult adr=(AlertDecoratorResult)controller.Edit(item.ID);
            ViewResult vr = (ViewResult)adr.InnerResult;
            T returnedItem = (T)vr.Model;

            Assert.IsNotNull(vr);
            Assert.AreEqual(id, returnedItem.ID);
            Assert.AreEqual(name, returnedItem.Name);
        }

         private static void CannotEditNonExistentItem()
        {  // TODO: write this
            Assert.Fail();
        }

        private static void CorrectRecipesAreBoundInEdit()
        {  // TODO: write this
            Assert.Fail();
        }
        private static void CorrectShoppingListsAreBoundInEdit() => Assert.Fail();


    }
}
