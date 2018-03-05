using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    public class BasicTests<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            ClassCleanup();
            ShouldCreate();
            ReturnDetailsWhenIDIsFound();
            CannotEditNonexistentT();
            GetTheClassNameCorrect();
            InheritBaseController();
            InheritBaseAttachDetachController();
            InheritsFromBaseControllerCorrectly();
            IsPublic();
            SaveTheCreationDateBetweenPostedEdits();
            SaveOneItem();
            GetTheClassNameCorrect();
            SaveEditedTWithDescriptionChange();
            DeleteAFoundEntity();
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


        private static void ShouldCreate()
        {
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory();
            ActionResult ar = controller.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            T t = (T)vr.Model;
            string modelName = t.Name;

            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
            // TODO:  Menu as child exclusive test
            //Assert.AreEqual(DayOfWeek.Sunday, Menu.DayOfWeek);
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


        private static void ShouldAddIngredientToIngredientsList()
        {
            IRepository<Ingredient> repo = new TestRepository<Ingredient>();
            IGenericController<Ingredient> controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient() { ID = 3500, IngredientsList = "start" };
            string addedIngredient = "added ingredient";
            repo.Save(ingredient, repo.FullPath);
            controller.AddIngredientToIngredientsList(ingredient.ID, addedIngredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            string listWithAddedIngredient = String.Concat(ingredient.IngredientsList, ", ", addedIngredient);
            Assert.AreEqual(listWithAddedIngredient, returnedIngredient.IngredientsList);
        }




        private static void SuccessfullyDetachRecipeChild()
        {
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachRecipeChild" };
            repo.Save(parent, repo.FullPath);
            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachRecipeChild" };
            IRepository<Recipe> childrepo = new TestRepository<Recipe>();
            childrepo.Save(child, repo.FullPath);

            Type type = typeof(T);
            IGenericController<T> AttachController = BaseControllerTestFactory();

            AttachController.Attach(parent, child);
            IGenericController<T> DetachController = BaseControllerTestFactory();
            DetachController.Detach(parent, child);
            IEntity ReturnedItem = repo.GetById(parent.ID);
            IEntity parentR = parent;

            Assert.AreEqual(0, parentR.Recipes.Count());
            Assert.AreEqual(0, ReturnedItem.Recipes.Count());
        }


        private static void DeleteAFoundEntity()
        {
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



        private static void ReturnDetailsWhenIDIsFound()
        {
            T item = new T() { ID = 1492 };
            IRepository<T> repo = new TestRepository<T>();
            repo.Save(item, repo.FullPath);
            IGenericController<T> controller = BaseControllerTestFactory();
            ActionResult ar = controller.Details(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;
            int returnedID = ((T)(vr.Model)).ID;

            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(item.ID, returnedID);
        } //View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");

        private static void ReturnDeleteWithActionMethodDeleteWithEmptyResult()
        {
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

        private static void SaveEditedTWithDescriptionChange()
        {
            SetUpForTests(out repo, out controller, out item);
            item = new T() { ID = 40000 };
            item.Description = "Pre-test";
            repo.Save(item, repo.FullPath);

            item.Description = "Post-test";

            ActionResult ar = controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Post-test", returnedItem.Description);

        }

        private static void SaveTheCreationDateBetweenPostedEdits()
        {

            SetUpForTests(out repo, out controller, out item);
            DateTime creationDate = new DateTime(2010, 1, 1);
            item = new T()
            {
                ID = 290,
                CreationDate = creationDate
            };
            repo.Save(item, repo.FullPath);
            ActionResult ar = controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(creationDate, item.CreationDate);
            Assert.AreEqual(item.CreationDate, returnedItem.CreationDate);
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

        private static void SaveOneItem()
        { 
            IRepository<T> repo = new TestRepository<T>();

            T item = new T()
            {
                ID = 90000,
                Name = "test SaveOneItem"
            }; 
            repo.Save(item);

            T returnedItem= repo.GetById(item.ID);


            Assert.AreEqual(item.ID, returnedItem.ID);
            Assert.AreEqual(item.Name, returnedItem.Name);

        }

        private static void ComputesCorrectTotalCalories() => Assert.Fail();

        private static void CopyModifySaveWithANewName() => Assert.Fail();

        private static void CorrectPlanElementsAreBoundInEdit() => Assert.Fail();


        private static void AllowUserToConfirmDeleteRequestAndCallConfirmDelete() => Assert.Fail();


    }
}
