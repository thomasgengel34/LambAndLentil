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
    internal class BaseControllerTest_BasicTests<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            BaseDetachAllIngredientChildren();
            BaseShouldCreate();
            BaseReturnDetailsWhenIDIsFound();
            CannotEditNonexistentT();
            GetTheClassNameCorrect();
            InheritBaseController();
            InheritBaseAttachDetachController();
            InheritsFromBaseControllerCorrectly();
            IsPublic();
            SaveTheCreationDateBetweenPostedEdits();
            GetTheClassNameCorrect();
            SaveEditedTWithDescriptionChange();
            DeleteAFoundEntity();


        }

        private static void IsPublic()
        {
            controller = BaseControllerTestFactory(typeof(T));
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            Assert.AreEqual(isPublic, true);
        }

        private static void InheritsFromBaseControllerCorrectly()
        {
            controller = BaseControllerTestFactory(typeof(T));
            controller.PageSize = 4;

            var type = controller.GetType();
            var DoesDisposeExist = type.GetMethod("Dispose");
            Type baseType = typeof(BaseController<T>);
            bool isBase = baseType.IsInstanceOfType(controller);

            Assert.AreEqual(isBase, true);
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }


        private static void BaseShouldCreate()
        {
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
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
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
            var type = controller.GetType();
            var DoesDisposeExist = type.GetMethod("Dispose");

            Assert.IsNotNull(DoesDisposeExist);
        }

        private static void BePublic()
        {
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
            var type = controller.GetType();
            bool isPublic = type.IsPublic;

            Assert.AreEqual(isPublic, true);
        }

        private static void GetTheClassNameCorrect()
        {
            Type type = typeof(T);
            IGenericController<T> controller = BaseControllerTestFactory(type);
            var controllerType = controller.GetType();
            Assert.AreEqual(type.Name + "sController", controllerType.Name);
        }


        private static void BaseShouldAddIngredientToIngredientsList()
        {
            IRepository<Ingredient> repo = new TestRepository<Ingredient>();
            IGenericController<Ingredient> controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient() { ID = 3500, IngredientsList = "start" };
            string addedIngredient = "added ingredient";
            repo.Save(ingredient);
            controller.AddIngredientToIngredientsList(ingredient.ID, addedIngredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            string listWithAddedIngredient = String.Concat(ingredient.IngredientsList, ", ", addedIngredient);
            Assert.AreEqual(listWithAddedIngredient, returnedIngredient.IngredientsList);
        }




        private static void BaseSuccessfullyDetachRecipeChild()
        {
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachRecipeChild" };
            repo.Save(parent);
            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachRecipeChild" };
            IRepository<Recipe> childrepo = new TestRepository<Recipe>();
            childrepo.Save(child);

            Type type = typeof(T);
            IGenericController<T> AttachController = BaseControllerTestFactory(type);

            AttachController.Attach(parent, child);
            IGenericController<T> DetachController = BaseControllerTestFactory(type);
            DetachController.Detach(parent, child);
            IEntity ReturnedItem = repo.GetById(parent.ID);
            IEntity parentR = parent;

            Assert.AreEqual(0, parentR.Recipes.Count());
            Assert.AreEqual(0, ReturnedItem.Recipes.Count());
        }

        private static void BaseSuccessfullyDetachFirstRecipeChild(IGenericController<T> AttachController, IGenericController<T> DetachController, int orderNumber = 0)
        {

            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachFirstRecipeChild" };
            repo.Save(parent);
            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachhRecipeChild" };
            IRepository<Recipe> childrepo = new TestRepository<Recipe>();
            childrepo.Save(child);

            Type type = typeof(T);
            IGenericController<T> Attachcontroller = BaseControllerTestFactory(type);
            Attachcontroller.Attach(parent, child);
            IEntity parentR = (IEntity)parent;


            IGenericController<T> Detachcontroller = BaseControllerTestFactory(type);
            Detachcontroller.Detach(parent, child);
            IEntity ReturnedItem = repo.GetById(parent.ID);


            Assert.AreEqual(0, parentR.Recipes.Count());
            Assert.AreEqual(0, ReturnedItem.Recipes.Count());
        }
        private static void DeleteAFoundEntity()
        {
            int repoCount = repo.Count();

            ActionResult ar = controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string viewName = ((ViewResult)adr.InnerResult).ViewName;

            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(repoCount, repo.Count());  // shows this does not actually delete anything.  That is done in delete-confirm
        }



        private static void BaseReturnDetailsWhenIDIsFound()
        {
            T item = new T() { ID = 1492 };
            IRepository<T> repo = new TestRepository<T>();
            repo.Save(item);
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
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

        private static void BaseReturnDeleteWithActionMethodDeleteWithEmptyResult()
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

        private static void BaseUpdateTheModificationDateBetweenPostedEdits()
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




        private static void BaseDetachAllIngredientChildren()
        {
            //T parent = Generate_T_WithFiveIngredientChildren();
            //repo.Save(parent);
            //List<IEntity> selection = parent
            //controller.DetachASetOf(parent, selection);
            //var returnedEntity = repo.GetById(parent.ID);
            //var trueOrFalse = (returnedEntity.Ingredients == null) || (returnedEntity.Ingredients.Count() == 0);

            //Assert.IsTrue(trueOrFalse);
        }


        private T Generate_T_WithFiveIngredientChildren()
        {
            IRepository<Ingredient> repo = new TestRepository<Ingredient>();
            T item = new T { ID = 31425926 };

            Ingredient ingredient0 = new Ingredient() { ID = 1000 };
            Ingredient ingredient1 = new Ingredient() { ID = 1001 };
            Ingredient ingredient2 = new Ingredient() { ID = 1002 };
            Ingredient ingredient3 = new Ingredient() { ID = 1003 };
            Ingredient ingredient4 = new Ingredient() { ID = 1004 };

            item.Ingredients = new List<Ingredient>();

            List<Ingredient> list = new List<Ingredient>() { ingredient0, ingredient1, ingredient2, ingredient3, ingredient4 };
            item.Ingredients.AddRange(list);
            return item as T;

        }

        private static void BaseDetachAllMenuChildren()
        {
            T item = new T { ID = 31425926 };

            Menu ingredient0 = new Menu() { ID = 1000 };
            Menu ingredient1 = new Menu() { ID = 1001 };
            Menu ingredient2 = new Menu() { ID = 1002 };
            Menu ingredient3 = new Menu() { ID = 1003 };
            Menu ingredient4 = new Menu() { ID = 1004 };

            item.Menus = new List<Menu>();

            List<Menu> list = new List<Menu>() { ingredient0, ingredient1, ingredient2, ingredient3, ingredient4 };
            item.Menus.AddRange(list);
            repo.Save(item);

            ActionResult ar = controller.DetachAll(item, new Menu());
            Assert.IsNotNull(ar);
            // TODO:flesh out test
        }// 


        private static void BaseDetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet<TParent>()
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
            repo.Save(item as T);

            List<IEntity> selected = new List<IEntity>() { new Ingredient { ID = 6000 } };
            controller.DetachASetOf(item, selected);
            IEntity returnedEntity = repo.GetById(item.ID);

            Assert.AreEqual(initialIngredientCount, returnedEntity.Ingredients.Count());
        }

        private static void BaseReturnsIndexWithWarningWithUnknownParentID()
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

        private static void BaseReturnsIndexWithWarningWithNullParent()
        {
            ActionResult ar = controller.Attach(null, new Ingredient());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            Assert.AreEqual(item.DisplayName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        private static void BaseDetachTheLastIngredientChild()
        {
            T item = new T();
            item.Ingredients = new List<Ingredient>();
            item.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            item.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            item.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            item.Ingredients.Add(new Ingredient { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save((T)item);
            int initialIngredientCount = item.Ingredients.Count();

            Ingredient LastIngredient = item.Ingredients.LastOrDefault();
            item.Ingredients.RemoveAt(initialIngredientCount - 1);
            bool IsLastIngredientStillThere = item.Ingredients.Contains(LastIngredient);

            Assert.AreEqual(initialIngredientCount - 1, item.Ingredients.Count());
            Assert.IsFalse(IsLastIngredientStillThere);
        }

        private static void BaseReturnsDetailWithWarningIfAttachingNullChild()
        {

            repo.Save(item);
            Ingredient ingredient = null;
            var ar = controller.Attach(item, ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Child was not found", adr.Message);
        }


        private static void BaseReturnsDetailWithWarningWithUnknownChildID(IEntity entity, IGenericController<T> controller)
        {
            var ar = controller.Attach(entity, (Ingredient)null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Child was not found", adr.Message);
        }


        private static void BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild()
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





        private static void BaseDetachTheLastMenuChild()
        {
            item.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            item.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            item.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            item.Menus.Add(new Menu { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save((T)item);
            int initialMenuCount = item.Menus.Count();

            Menu LastMenu = item.Menus.LastOrDefault();
            item.Menus.RemoveAt(initialMenuCount - 1);
            bool IsLastMenuStillThere = item.Menus.Contains(LastMenu);

            Assert.AreEqual(initialMenuCount - 1, item.Menus.Count());
            Assert.IsFalse(IsLastMenuStillThere);
        }

        private static void BaseDetachTheLastRecipeChild()
        {
            SetUpForTests(out repo, out controller, out item);
            item.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            item.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            item.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            item.Recipes.Add(new Recipe { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save((T)item);
            int initialRecipeCount = item.Recipes.Count();

            Recipe LastRecipe = item.Recipes.LastOrDefault();
            item.Recipes.RemoveAt(initialRecipeCount - 1);
            bool IsLastRecipeStillThere = item.Recipes.Contains(LastRecipe);

            Assert.AreEqual(initialRecipeCount - 1, item.Recipes.Count());
            Assert.IsFalse(IsLastRecipeStillThere);
        }

        private static void BaseDetachTheLastShoppingListChild(IRepository<T> repo, IGenericController<T> controller, IEntity Entity)
        {
            SetUpForTests(out repo, out controller, out item);
            Entity.ShoppingLists.Add(new ShoppingList { ID = 4005, Name = "Butter" });
            Entity.ShoppingLists.Add(new ShoppingList { ID = 4006, Name = "Cayenne Pepper" });
            Entity.ShoppingLists.Add(new ShoppingList { ID = 4007, Name = "Cheese" });
            Entity.ShoppingLists.Add(new ShoppingList { ID = 54008, Name = "Chopped Green Pepper" });
            repo.Save((T)Entity);
            int initialShoppingListCount = Entity.ShoppingLists.Count();

            ShoppingList LastShoppingList = Entity.ShoppingLists.LastOrDefault();
            Entity.ShoppingLists.RemoveAt(initialShoppingListCount - 1);
            bool IsLastShoppingListStillThere = Entity.ShoppingLists.Contains(LastShoppingList);

            Assert.AreEqual(initialShoppingListCount - 1, Entity.ShoppingLists.Count());
            Assert.IsFalse(IsLastShoppingListStillThere);
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
            repo.Save(item);

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
            repo.Save(item);
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

 
        private static void FlagAnIngredientFlaggedInAPerson() => Assert.Fail();
       
        private static void FlagAnIngredientFlaggedInTwoPersons() => Assert.Fail();
        
        private static void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()=> Assert.Fail();
       
        private static void ComputesCorrectTotalCalories()=> Assert.Fail();
       
        private static void CopyModifySaveWithANewName() => Assert.Fail();

        private static void CorrectPlanElementsAreBoundInEdit() => Assert.Fail();

        private static void FlagAnMenuFlaggedInAPerson() => Assert.Fail(); 

        private static void FlagAnMenuFlaggedInTwoPersons() => Assert.Fail(); 

        private static void FlagAnRecipeFlaggedInAPerson() => Assert.Fail();

        private static void FlagAnRecipeFlaggedInTwoPersons() => Assert.Fail();

         private static  void AllowUserToConfirmDeleteRequestAndCallConfirmDelete() => Assert.Fail();
        

    }
}
