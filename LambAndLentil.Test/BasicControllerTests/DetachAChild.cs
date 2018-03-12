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

namespace LambAndLentil.Test.BasicTests
{
    internal class DetachAChild<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            ClassCleanup();
            DetachAnIngredientChild();
            DetachAValidMenuChildFromAValidParent();
            //DetachAPlanChild();    TODO: write these tests
            //DetachARecipeChild();
            //DetachAShoppingListChild();
            NotDeleteAnIngredientAfterIngredientIsDetached();
            ReturnIndexViewWithWarningWhenDetachingNonExistingIngredientAttachedToANonExistingMenu();
            ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu();
            SuccessfullyDetachRecipeChild();
            ClassCleanup();
        }



        private static void DetachAnIngredientChild()
        {
            SetUpForTests(out repo, out controller, out item);
            Ingredient child = new Ingredient() { ID = 101 };
            item.Ingredients.Add(child);
            repo.Save(item);
            controller.Detach(item, child);
            var returnedEntity = repo.GetById(item.ID);
            var trueOrFalse = (returnedEntity.Ingredients == null) || (returnedEntity.Ingredients.Count() == 0);

            Assert.IsTrue(trueOrFalse);

            // TODO: flesh out test
        }



        private static void DetachAValidMenuChildFromAValidParent()
        {
            SetUpForTests(out repo, out controller, out item);
            Menu menu = new Menu();
            if (item.CanHaveChild(menu))
            {
                item.Menus = new List<Menu>();
                Menu ingredient0 = new Menu() { ID = 1000 };

                List<Menu> list = new List<Menu>() { ingredient0 };
                item.Menus.AddRange(list);
                repo.Save(item);

                ActionResult ar = controller.DetachAll(item, new Menu());
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
               RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                var routeValues = rtrr.RouteValues.Values;

                string childName = "Menu";
                Assert.IsNotNull(ar);
                Assert.AreEqual("All " + childName + "s Were Successfully Detached!", adr.Message);
                Assert.AreEqual("alert-success", adr.AlertClass);
                // TODO:flesh out test 
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(item.ID.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            }

        }//   RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");

        private static void NotDeleteAnIngredientAfterIngredientIsDetached()
        {
            SetUpForTests(out repo, out controller, out item);
            Ingredient ingredient = new Ingredient { ID = 100 };
            IRepository<Ingredient> childRepo = new TestRepository<Ingredient>();
            childRepo.Save(ingredient);
            repo.Update(item, item.ID);
            controller.Detach(item, ingredient);
            Ingredient returnedIngredient = childRepo.GetById(ingredient.ID);
            Assert.IsNotNull(ingredient);
        }


        private static void ReturnIndexViewWithWarningWhenDetachingNonExistingIngredientAttachedToANonExistingMenu()
        {
            IRepository<Menu> menuRepo = new TestRepository<Menu>();
            IGenericController<Menu> menusController = new MenusController(menuRepo);
            Menu menu = new Menu() { ID = 700 };
            menu = null;
            AlertDecoratorResult adr = (AlertDecoratorResult)menusController.Detach(menu, (Ingredient)null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }




        private static void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu()
        {
            IRepository<Menu> menuRepo = new TestRepository<Menu>();
            IGenericController<Menu> menusController = new MenusController(menuRepo);
            Menu menu = new Menu();
            menu = null;
            AlertDecoratorResult adr = (AlertDecoratorResult)menusController.Detach(menu, (Ingredient)null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        private static void SuccessfullyDetachRecipeChild()
        {
            SetUpForTests(out repo, out controller, out item);

            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachRecipeChild" };


            Type type = typeof(T);
            IGenericController<T> AttachController = BaseControllerTestFactory();

            AttachController.Attach(item, child);
            IGenericController<T> DetachController = BaseControllerTestFactory();
            DetachController.Detach(item, child);
            IEntity ReturnedItem = repo.GetById(item.ID);
            IEntity itemR = item;

            Assert.AreEqual(0, itemR.Recipes.Count());
            Assert.AreEqual(0, ReturnedItem.Recipes.Count());
        }

        private static void DetachAPlanChild() => throw new NotImplementedException();
        private static void DetachARecipeChild() => throw new NotImplementedException();
        private static void DetachAShoppingListChild() => throw new NotImplementedException();
    }
}
