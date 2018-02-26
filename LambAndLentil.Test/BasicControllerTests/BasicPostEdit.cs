using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class BasicPostEdit<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            NotBindNotIdentifiedToBeBoundInEdit();
            NotChangeIDInPostEditActionMethod();
            CanPostEdit();
            ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved();
        }

        private static void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {
            IRepository<T> repo;
            IGenericController<T> controller;
            T item;
            SetUpForTests(out repo, out controller, out item);

            T invalidItem = new T()
            {
                ID = -2
            };

            ActionResult ar = controller.PostEdit(invalidItem);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
        }


        private static void CanPostEdit()
        {
            IRepository<T> repo;
            IGenericController<T> controller;
            T item;
            SetUpForTests(out repo, out controller, out item);
            item.Name = "Name has been changed";
            repo.Save(item);
            ViewResult view1 = (ViewResult)controller.Edit(item.ID);

            T returnedItem = repo.GetById(item.ID);

            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedItem.Name);
            Assert.AreEqual(item.Description, returnedItem.Description);
            Assert.AreEqual(item.CreationDate, returnedItem.CreationDate);
        }

        private static void NotChangeIDInPostEditActionMethod()
        {
            IRepository<T> repo;
            IGenericController<T> controller;
            T item;
            SetUpForTests(out repo, out controller, out item);
            int originalID = item.ID;
            item.ID = 7000;
            int initialCount = repo.Count();

            controller.PostEdit(item);
            T returnedItem = repo.GetById(7000);
            T originalItem = repo.GetById(originalID);
            int newCount = repo.Count();

            Assert.AreEqual(originalID, originalItem.ID);
            Assert.AreEqual(item.ID, returnedItem.ID);
            Assert.AreEqual(initialCount + 1, newCount);
        }

        private static void NotBindNotIdentifiedToBeBoundInEdit()
        {
            IRepository<T> repo;
            IGenericController<T> controller;
            T item;
            SetUpForTests(out repo, out controller, out item);

            item.AddedByUser = "Changed";
            item.ModifiedByUser = "Should Not Be Original";
            controller.PostEdit((T)item);
            T returnedT = repo.GetById(item.ID);

            Assert.AreNotEqual("Changed", returnedT.AddedByUser);
            Assert.AreNotEqual("Should Not Be Original", returnedT.ModifiedByUser);
        }

        //TODO: write 
        private static void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved() =>
             Assert.Fail();

        //TODO: write 
        private static void NotSaveLogicallyInvalidModel() => Assert.Fail();

        //TODO: write 
        private static void NotSaveModelFlaggedInvalidByDataAnnotation() => Assert.Fail();
        // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

    }
}
