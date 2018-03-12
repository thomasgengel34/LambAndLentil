using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    internal class PostEdit<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {


        internal static void TestRunner()
        {
            BindDescription();
            BindIngredients();
            BindIngredientsList();
            CanPostEdit();
            ChangeName();
            ModifiedDateUpDatesInEdit();
            NotBindNotIdentifiedToBeBoundInEdit();
            NotCreateASecondElementOnPostEditingOneElement();
            NotChangeIDInPostEditActionMethod();
            ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved();
            SaveAValidItemAndReturnIndexView();
           // SaveEditedPersonWithCorrectName();  TODO: fix
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
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
        }


        private static void CanPostEdit()
        { 
            SetUpForTests(out repo, out controller, out item);
            item.Name = "Name has been changed";
            repo.Save(item);
            ViewResult view1 = (ViewResult)controller.Edit(item.ID);

            T returnedItem = repo.GetById(item.ID);

            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedItem.Name);
            Assert.AreEqual(item.ID, returnedItem.ID);
            Assert.AreEqual(item.Description, returnedItem.Description);
            Assert.AreEqual(item.CreationDate, returnedItem.CreationDate);
        }




        private static void NotChangeIDInPostEditActionMethod()
        {
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

        private static void BindDescription()
        {
            item.Description = "Changed";

            controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(item.Description, returnedItem.Description);
        }



        private static void BindIngredients()
        {
            item.Ingredients = new List<Ingredient> {
                new Ingredient { Name = "Changed" },
                new Ingredient { Name = "Changed 2" },
                new Ingredient { Name = "Changed Up" }
            };

            ActionResult ar = controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Changed", returnedItem.Ingredients.First().Name);
            Assert.AreEqual("Changed Up", returnedItem.Ingredients.Last().Name);
        }


        private static void ChangeName()
        {
            item.Name = "Changed";

            controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Changed", returnedItem.Name);
        }



        private static void NotCreateASecondElementOnPostEditingOneElement()
        {
            int initialCount = repo.Count();
            item.Name = "Changed";
            controller.PostEdit(item);

            Assert.AreEqual(initialCount, repo.Count());
        }

        private static void SaveAValidItemAndReturnIndexView()
        {
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(item);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());

        }


        private static void ModifiedDateUpDatesInEdit()
        {
            DateTime ModifiedDate = DateTime.Now;
            item.ModifiedDate = ModifiedDate;
            controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreNotEqual(ModifiedDate, returnedItem.ModifiedDate);
        }

        private static void BindIngredientsList()
        {
            item.IngredientsList = "This, That, Those";
            repo.Save(item);

            ActionResult ar = controller.PostEdit(item);

            T returnedItem = repo.GetById(item.ID);

            Assert.IsNotNull(returnedItem);
            Assert.AreEqual(item.IngredientsList, returnedItem.IngredientsList);
        }

        private static void SaveEditedPersonWithCorrectName()
        {
            SetUpForTests(out repo, out controller, out item);
            if (item.GetType() == typeof(Person))
            {
                Person person = new Person() { ID = 8900,
                    FirstName = "SaveEditedPersonTest",
                    LastName = ""
                };
                 
                ActionResult ar  = controller.PostEdit(person as T);
                ViewResult view = (ViewResult)ar ;
                ListEntity<Person> listEntity  = (ListEntity<Person>)view.Model;
                Person returnedPerson = repo.GetById(person.ID) as Person;

                Assert.AreEqual("SaveEditedPersonTest ", returnedPerson.Name);
            } 
        }

        private static void SaveAllPropertiesInBaseEntity()
        {
            SetUpForTests(out repo, out controller, out item);
            controller.PostEdit(item);
            T returneditem = repo.GetById(item.ID);

            Assert.AreEqual(returneditem.Name, item.Name);
            Assert.AreEqual(returneditem.Description, item.Description);
            Assert.AreEqual(returneditem.CreationDate.Day, item.CreationDate.Day);
            Assert.AreEqual(returneditem.ModifiedDate.Day, item.ModifiedDate.Day);
            Assert.AreEqual(returneditem.AddedByUser, item.AddedByUser);
            Assert.AreEqual(returneditem.ModifiedByUser, item.ModifiedByUser);
        }


        //TODO: write 
        private static void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved() =>
             Assert.Fail();

        //TODO: write 
        private static void NotSaveLogicallyInvalidModel() => Assert.Fail();

        //TODO: write 
        private static void NotSaveModelFlaggedInvalidByDataAnnotation() => Assert.Fail();
        // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

        //TODO: write 
        private static void CannotPostEditNonExistingItem() => Assert.Fail();


    }
}
