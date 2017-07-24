using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System;
using LambAndLentil.Domain.Concrete;

namespace LambAndLentil.UI.Controllers
{
    public class MenusController : BaseController
    {
         

        // GET: Menus
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex<Menu,MenuVM>(page);
            return View(view.ViewName, view.Model);
        }

        // GET: Menus/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails< Menu,MenuVM>(UIControllerType.Menus, id, actionMethod);
        }



        // GET: Menus/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate<MenuVM>(actionMethod);
        }


        // GET: Menus/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit< Menu,MenuVM>(UIControllerType.Menus, id);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name, DayOfWeek, Description,CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser, MealType,Diners, Recipes, Ingredients")] MenuVM menuVM)
        {
            return BasePostEdit< Menu,MenuVM>(menuVM);
        }

        // GET: Menus/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        { 
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete< Menu,MenuVM>(UIControllerType.Menus, id);
        }



        // POST: Ingredients/Delete/5 
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Menu,  MenuVM>(UIControllerType.Menus, id);
        }


        public ActionResult AttachRecipe( int? menuID, int? recipeID)
        {  
            return BaseAttach<MenuVM, RecipeVM>(menuID, recipeID); 
        }

        public ActionResult DetachRecipe(int? menuID, int? recipeID)
        {
            return BaseAttach<MenuVM, IngredientVM>(menuID, recipeID, AttachOrDetach.Detach);
        }

        public ActionResult AttachIngredient(int? menuID, int? ingredientID)
        {
            return BaseAttach<MenuVM, IngredientVM>(menuID, ingredientID);
        }
         

        public ActionResult DetachIngredient(int? menuID, int? ingredientID)
        {
            return BaseAttach<MenuVM, IngredientVM>(menuID, ingredientID, AttachOrDetach.Detach);
        }

        private Menu GetMenu()
        {
            Menu recipe = (Menu)Session["Menu"];
            if (recipe == null)
            {
                recipe = new Menu();
                Session["Menu"] = recipe;
            }
            return recipe;
        }


    }
}
