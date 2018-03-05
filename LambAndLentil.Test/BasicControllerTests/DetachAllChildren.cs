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
    internal class DetachAllChildren<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            DetachAllIngredientChildren();
            DetachAllMenuChildren();
            DetachAllPlanChildren();
            DetachAllRecipeChildren();
            DetachAllShoppingListChildren();
        }



        private static void DetachAllIngredientChildren()
        {
            SetUpForTests(out repo, out controller, out item);
            List<Ingredient> list = Generate_List_WithFiveIngredientChildren();
            List<IEntity> selection = new List<IEntity>();
            foreach (var ingredient in list)
            {
                selection.Add(ingredient);
            } 
            controller.DetachASetOf(item, selection);
            var returnedEntity = repo.GetById(item.ID);
            var trueOrFalse = (returnedEntity.Ingredients == null) || (returnedEntity.Ingredients.Count() == 0);

            Assert.IsTrue(trueOrFalse);
        }


       private static List<Ingredient> Generate_List_WithFiveIngredientChildren()
        {
            Ingredient ingredient0 = new Ingredient() { ID = 1000 };
            Ingredient ingredient1 = new Ingredient() { ID = 1001 };
            Ingredient ingredient2 = new Ingredient() { ID = 1002 };
            Ingredient ingredient3 = new Ingredient() { ID = 1003 };
            Ingredient ingredient4 = new Ingredient() { ID = 1004 };

            List<Ingredient> list = new List<Ingredient>() { ingredient0, ingredient1, ingredient2, ingredient3, ingredient4 };

            return list;
        }


        private static void DetachAllMenuChildren()
        {
            SetUpForTests(out repo, out controller, out item);

            item.Menus = new List<Menu>();
            Menu ingredient0 = new Menu() { ID = 1000 };
            Menu ingredient1 = new Menu() { ID = 1001 };
            Menu ingredient2 = new Menu() { ID = 1002 };
            Menu ingredient3 = new Menu() { ID = 1003 };
            Menu ingredient4 = new Menu() { ID = 1004 }; 
            List<Menu> list = new List<Menu>() { ingredient0, ingredient1, ingredient2, ingredient3, ingredient4 };
            item.Menus.AddRange(list);
            repo.Save(item);

            ActionResult ar = controller.DetachAll(item, new Menu());
            RedirectToRouteResult rdr = (RedirectToRouteResult)ar;
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;


            string childName = "Menu";
            Assert.IsNotNull(ar);
            Assert.AreEqual("All " + childName + "s Were Successfully Detached!", adr.Message);
            Assert.AreEqual("alert-success" , adr.AlertClass);

            // TODO:flesh out test
        }//   RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");




        private static void DetachAllPlanChildren() => throw new NotImplementedException();
        private static void DetachAllRecipeChildren() => throw new NotImplementedException();
        private static void DetachAllShoppingListChildren() => throw new NotImplementedException();
    }
}
