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
    internal  class ClassPropertyChanges<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        
        private static T returnedItem { get; set; }

        internal static void TestRunner()
        {
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
           // ShouldNotEditWebAPIGeneratedIngredientsList(); TODO: write test 
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
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
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
            returnedItem =repo.GetById(item.ID);

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
    }
}
