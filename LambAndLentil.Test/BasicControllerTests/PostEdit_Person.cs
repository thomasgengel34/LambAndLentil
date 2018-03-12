using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicTests
{ 
    internal class PostEdit_Person<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        {
            if (typeof(T) == typeof(Person))
            { 
                PostEditFirstName();
                PostEditLastName();
                PostEditPersonsFullName(); 
                UnAssignAnIngredientFromAPerson();
            }
        }


        private static void PostEditPersonsFullName()
        {
            SetUpForTests(out repo, out controller, out item);
            Person person = item as Person;
            person.FirstName = "Reynard";
            person.LastName = "Finkelstein";
            controller.PostEdit(person as T);
            Person returnedPerson = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Reynard Finkelstein", returnedPerson.Name);
        }

        private static void PostEditFirstName()
        {
            SetUpForTests(out repo, out controller, out item);
            Person person = item as Person;
            person.FirstName = "Reynard";

            controller.PostEdit(person as T);
            Person returnedPerson = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Reynard Created", returnedPerson.Name);
        }


     
       private static void PostEditLastName()
        {
            SetUpForTests(out repo, out controller, out item);
            Person person = item as Person;
            person.LastName = "Luc";
            controller.PostEdit(person as T);
            Person returnedPerson = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Luc", returnedPerson.LastName);
        }



        private static void UnAssignAnIngredientFromAPerson()
        {
            if (typeof(T) == typeof(Person))
            {
                SetUpForTests(out repo, out controller, out item);
                Ingredient ingredient = new Ingredient() { ID = 42 };
                item.Ingredients.Add(ingredient);
                repo.Save(item);

                AlertDecoratorResult adr = (AlertDecoratorResult)controller.Detach(item, ingredient);
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

                var routeValues = rtrr.RouteValues.Values;

                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(item.ID.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
                Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
            }
        } 
    }
}
