using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    public class BasicTests<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            ClassCleanup();  
            CannotEditNonexistentT(); 
            GetTheClassNameCorrect();
            InheritBaseController();
            InheritBaseAttachDetachController();
            InheritsFromBaseControllerCorrectly();
            IsPublic(); 
            ClassCleanup();
        }

        private static void IsPublic()
        {
            controller = BaseControllerTestFactory();
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            Assert.AreEqual(isPublic, true);
        }

        private static void InheritsFromBaseControllerCorrectly()
        {
            controller = BaseControllerTestFactory();
            controller.PageSize = 4;

            var type = controller.GetType();
            var DoesDisposeExist = type.GetMethod("Dispose");
            Type baseType = typeof(BaseController<T>);
            bool isBase = baseType.IsInstanceOfType(controller);

            Assert.AreEqual(isBase, true);
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }



        private static void DisposeExists()
        {
            IGenericController<T> controller = BaseControllerTestFactory();
            var type = controller.GetType();
            var DoesDisposeExist = type.GetMethod("Dispose");

            Assert.IsNotNull(DoesDisposeExist);
        }

        private static void BePublic()
        {
            IGenericController<T> controller = BaseControllerTestFactory();
            var type = controller.GetType();
            bool isPublic = type.IsPublic;

            Assert.AreEqual(isPublic, true);
        }

        private static void GetTheClassNameCorrect()
        {
            Type type = typeof(T);
            IGenericController<T> controller = BaseControllerTestFactory();
            var controllerType = controller.GetType();
            Assert.AreEqual(type.Name + "sController", controllerType.Name);
        }
         
         

        private static void UpdateTheModificationDateBetweenPostedEdits()
        {
            DateTime CreationDate = item.CreationDate;
            DateTime modDate = item.ModifiedDate;

            controller.PostEdit(item);
            IEntity returnedEntity = repo.GetById(item.ID);
            DateTime newCreationDate = item.CreationDate;
            DateTime newModDate = item.ModifiedDate;

            Assert.AreEqual(CreationDate, newCreationDate);
            Assert.AreNotEqual(modDate, newModDate);
        }

         

        private static void DetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet<TParent>()
            where TParent : BaseEntity, IEntity, new()
        {
            IRepository<T> repo;
            IGenericController<T> controller;

            SetUpForTests(out repo, out controller, out item);

            item = new T() { ID = 7654321 };
            item.Ingredients.Clear();
            item.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            item.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            item.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            item.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            int initialIngredientCount = item.Ingredients.Count();
            repo.Save(item as T, repo.FullPath);

            List<IEntity> selected = new List<IEntity>() { new Ingredient { ID = 6000 } };
            controller.DetachASetOf(item, selected);
            IEntity returnedEntity = repo.GetById(item.ID);

            Assert.AreEqual(initialIngredientCount, returnedEntity.Ingredients.Count());
        }

        private static void ReturnsIndexWithWarningWithUnknownParentID()
        {
            T item = new T();
            string displayName = item.DisplayName;
            item = null;
            ActionResult ar = controller.Attach(item, new Ingredient());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            Assert.AreEqual(displayName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        private static void ReturnsIndexWithWarningWithNullParent()
        {
            ActionResult ar = controller.Attach(null, new Ingredient());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            Assert.AreEqual(item.DisplayName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }


        private static void ReturnsDetailWithWarningIfAttachingNullChild()
        {
            repo.Save(item, repo.FullPath);
            Ingredient ingredient = null;
            var ar = controller.Attach(item, ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Child was not found", adr.Message);
        }


        private static void ReturnsDetailWithWarningWithUnknownChildID(IEntity entity, IGenericController<T> controller)
        {
            var ar = controller.Attach(entity, (Ingredient)null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Child was not found", adr.Message);
        }


        private static void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild()
        {

            Ingredient ingredient = new Ingredient() { ID = 8000 };
            item.Ingredients = new List<Ingredient>();
            item.Ingredients.Add(ingredient);
            repo.Update(item, item.ID);

            var ar = controller.Detach(item, ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("No child was attached!", adr.Message);
            Assert.AreEqual(item.ID, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }


        private static void CannotEditNonexistentT()
        {
            SetUpForTests(out repo, out controller, out item);
            T itemReturned = (T)((ViewResult)controller.Edit(333333333)).ViewData.Model;

            Assert.IsNull(itemReturned);
        }
         

        private static void InheritBaseController()
        {
            string typeName = typeof(T).Name;
            Type type = Type.GetType("LambAndLentil.UI.Controllers." + typeName + "sController, LambAndLentil.UI", true);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<T>)));
        }


        private static void InheritBaseAttachDetachController()
        {
            string typeName = typeof(T).Name;
            Type type = Type.GetType("LambAndLentil.UI.Controllers." + typeName + "sController, LambAndLentil.UI", true);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseAttachDetachController<T>)));
        }
         

        private static void ComputesCorrectTotalCalories() => Assert.Fail();

        private static void CopyModifySaveWithANewName() => Assert.Fail();

        private static void CorrectMenuPropertiesAreBoundInEdit() => Assert.Fail();

        private static void CorrectPlanElementsAreBoundInEdit() => Assert.Fail();

        private static void AllowUserToConfirmDeleteRequestAndCallConfirmDelete() => Assert.Fail();

       private static void CorrectPropertiesAreBoundInEdit()=> Assert.Fail(); 
    }
}
