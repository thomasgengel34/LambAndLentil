using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public interface IAttachDetachController
    {
   
         ActionResult AttachIngredient(int? iD, Ingredient child, int orderNumber = 0);
         ActionResult DetachIngredient(int? iD, Ingredient child, int orderNumber = 0);
        ActionResult DetachAllIngredients(int ID, List<Ingredient> selected);
        ActionResult DetachASetOfIngredients(int ID, List<Ingredient> selected);

        ActionResult AttachRecipe(int? recipeID, Recipe child, int orderNumber = 0);
        ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0);
        ActionResult DetachAllRecipes(int ID, List<Recipe> selected);
        ActionResult DetachASetORecipes(int ID, List<Recipe> selected);

        ActionResult AttachMenu(int iD, Menu child, int orderNumber = 0);
        ActionResult DetachMenu(int iD, Menu child, int orderNumber = 0);
        ActionResult DetachAllMenus(int ID, List<Menu> selected);
        ActionResult DetachASetOMenus(int ID, List<Menu> selected);

        ActionResult AttachPlan(int iD, Plan child, int orderNumber = 0);
        ActionResult DetachPlan(int iD, Menu child, int orderNumber = 0);
        ActionResult DetachAllPlans(int ID, List<Plan> selected);
        ActionResult DetachASetOPlans(int ID, List<Plan> selected);

        ActionResult AttachShoppingList(int iD, ShoppingList child, int orderNumber = 0);
        ActionResult DetachShoppingList(int iD, Menu child, int orderNumber = 0);
        ActionResult DetachAllShoppingLists(int ID, List<ShoppingList> selected);
        ActionResult DetachASetOShoppingLists(int ID, List<ShoppingList> selected);
    }
}