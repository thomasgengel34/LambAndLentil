using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class ClassPropertyChanges<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        private static T returnedItem { get; set; }

        internal static void TestRunner()
        {
            ClassCleanup();
            CannotAlterModifiedByUserByHand();
            CannotAlterModifiedDateByHand();
            DoesNotEditAddedByUser();
            DoesNotEditCreationDate();

            ShouldAddIngredientToIngredients();
            ShouldEditDescription();
            ShouldEditIngredientsList();
            ShouldEditName();
            ShouldEditUserGeneratedIngredientsList();
            SaveTheCreationDateOnCreationWithNoParameterCtor();
            SaveTheCreationDateOnMenuCreationWithDateTimeParameter();
            SaveEditedItemWithNameChange();
            // ShouldNotEditWebAPIGeneratedIngredientsList(); TODO: write test 
            ClassCleanup();
        }

        private static void SetUpItemAndrepo(out T item, out IRepository<T> repo)
        {
            repo = new TestRepository<T>();
            T itemToRemove = repo.GetById(1000);
            if (itemToRemove != null)
            {
                repo.Remove(itemToRemove);
            }
            item = new T
            {
                ID = 10000,
                Description = "original"
            };
            repo.Save(item);
        }

        internal static void ShouldEditName()
        {
            SetUpItemAndrepo(out item, out repo);
            item.Name = "Name is changed";
            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Name is changed", returnedItem.Name);
        }


        internal static void Copy()
        {
            SetUpItemAndrepo(out item, out repo);
            item.ID = 42;
            controller.PostEdit(item);
            returnedItem = repo.GetById(42);
            item = repo.GetById(1000);

            Assert.AreEqual(42, returnedItem.ID);
            Assert.IsNotNull(item);
        }


        internal static void ShouldEditDescription()
        {
            SetUpItemAndrepo(out item, out repo);

            item.Description = "changed";
            IGenericController<T> controller = BaseControllerTestFactory();
            ActionResult ar = controller.PostEdit(item);

            returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(item.Description, returnedItem.Description);
        }



        internal static void DoesNotEditCreationDate()
        {
            SetUpItemAndrepo(out item, out repo);
            DateTime dateTime = new DateTime(1776, 7, 4);

            item.CreationDate = dateTime;
            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreNotEqual(dateTime.Year, returnedItem.CreationDate.Year);
        }


        internal static void DoesNotEditAddedByUser()
        {
            SetUpItemAndrepo(out item, out repo);
            string user = "Abraham Lincoln";

            item.AddedByUser = user;

            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreNotEqual(user, returnedItem.AddedByUser);
        }


        internal static void CannotAlterModifiedByUserByHand()
        {
            SetUpItemAndrepo(out item, out repo);
            string user = "Abraham Lincoln";

            item.ModifiedByUser = user;
            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreNotEqual(user, returnedItem.ModifiedByUser);
        }



        internal static void CannotAlterModifiedDateByHand()
        {
            SetUpItemAndrepo(out item, out repo);
            DateTime dateTime = new DateTime(1776, 7, 4);
            item.ModifiedDate = dateTime;

            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreNotEqual(dateTime.Year, returnedItem.ModifiedDate.Year);
        }


        internal static void ShouldEditUserGeneratedIngredientsList()
        {
            SetUpItemAndrepo(out item, out repo);
            item.IngredientsList = "Edited";

            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Edited", returnedItem.IngredientsList);
        }



        internal static void ShouldNotEditWebAPIGeneratedIngredientsList() =>

            Assert.Fail();


        internal static void ShouldAddIngredientToIngredients()
        {
            SetUpItemAndrepo(out item, out repo);
            int initialCount = item.Ingredients.Count;

            item.Ingredients.Add(new Ingredient() { ID = 134, Name = "ShouldAddIngredientToIngredients" });
            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(initialCount + 1, item.Ingredients.Count);
            Assert.AreEqual("ShouldAddIngredientToIngredients", item.Ingredients[initialCount].Name);
        }



        internal static void ShouldEditIngredientsList()
        {
            SetUpItemAndrepo(out item, out repo);
            item.IngredientsList = "Edited";

            controller.PostEdit(item);
            returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Edited", returnedItem.IngredientsList);
        }

        internal static void SaveTheCreationDateOnCreationWithNoParameterCtor()
        {
            SetUpItemAndrepo(out item, out repo);
            DateTime CreationDate = DateTime.Now;

            T newItem = new T() { ID = 2000 };
            TimeSpan timeSpan = CreationDate - newItem.CreationDate;

            Assert.AreEqual(CreationDate.Date, newItem.CreationDate.Date);
        }



        internal static void SaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {
            DateTime CreationDate = new DateTime(2010, 1, 1);

            if (typeof(T) == typeof(Ingredient))
            {
                Ingredient Ingredient = new Ingredient(CreationDate);
                Assert.AreEqual(CreationDate, Ingredient.CreationDate);
            }
            if (typeof(T) == typeof(Menu))
            {
                Menu Menu = new Menu(CreationDate);
                Assert.AreEqual(CreationDate, Menu.CreationDate);
            }
            if (typeof(T) == typeof(Person))
            {
                Person Person = new Person(CreationDate);
                Assert.AreEqual(CreationDate, Person.CreationDate);
            }
            if (typeof(T) == typeof(Plan))
            {
                Plan Plan = new Plan(CreationDate);
                Assert.AreEqual(CreationDate, Plan.CreationDate);
            }
            if (typeof(T) == typeof(Recipe))
            {
                Recipe Recipe = new Recipe(CreationDate);
                Assert.AreEqual(CreationDate, Recipe.CreationDate);
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingList ShoppingList = new ShoppingList(CreationDate);
                Assert.AreEqual(CreationDate, ShoppingList.CreationDate);
            }
        }


        private static void SaveEditedItemWithNameChange()
        {
            item.Name = "0000 test Edited";
            ActionResult ar = controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("0000 test Edited", returnedItem.Name);
        }


        private static void ShouldEditMealType()
        {
            Assert.Fail();
        }

        private static void ShouldEditDayOfWeek()
        {
            Assert.Fail();
        }

        private static void ShouldEditDiners()
        {
            Assert.Fail();
        }



        public void ShouldEditMaxCalories()
        {
            Assert.Fail();
        }

        public void ShouldEditMinCalories()
        {
            Assert.Fail();
        }

        public void ShouldEditWeight()
        {
            Assert.Fail();
        }

        public void WeightCannotBeLessThanZero()
        {
            Assert.Fail();
        }

        public void WeightCannotBeMoreThanOneThousand()
        {
            Assert.Fail();
        }

        public void CanEditNoGarlic()
        {
            Assert.Fail();
        }
    }
}
