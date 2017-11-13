using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public class MenusGenericController<Menu> : BaseController<Menu>, IGenericController<Menu> 
            where Menu : BaseEntity, IEntity, new()
    {
        public MenusGenericController(IRepository<Menu> repository) : base(repository) => Repo = repository;

        // GET: Menus
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(Repo,  page);
            return View(view.ViewName, view.Model);
        }

        // GET: Menus/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo,  UIControllerType.Menus, id, actionMethod);
        }



        // GET: Menus/Create
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        }


        // GET: Menus/Edit/5
        public ActionResult Edit(int id = 1)
        {
             return BaseDetails(Repo,  UIControllerType.Menus, id, UIViewType.Edit);
        }

        //POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name, DayOfWeek, Description,CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser, MealType,Diners, Recipes, Ingredients")] Menu t)
        {
            return BasePostEdit(Repo, t);
        }

        // GET: Menus/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete(Repo, UIControllerType.Menus, id);
        }



        // POST: Ingredients/Delete/5 
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Menus, id);
        }


        public ActionResult AttachRecipe(int? menuID, Recipe recipe, int orderNumber)=> 
            BaseAttach<Recipe>(Repo, menuID, recipe); 
       
        public ActionResult DetachRecipe(int? menuID, Recipe recipe )
        {
            return BaseAttach<Recipe>(Repo, menuID, recipe , AttachOrDetach.Detach);
        }
 public ActionResult DetachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) => throw new NotImplementedException();
        public ActionResult AttachIngredient(int? menuID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach<Ingredient>(Repo,menuID, ingredient, AttachOrDetach.Attach,  orderNumber);
        }


        public ActionResult DetachIngredient(int? menuID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach< Ingredient>(Repo,menuID, ingredient, AttachOrDetach.Detach);
        }

        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "")
        {
            BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);
        }

        public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID);

        public void DetachAllIngredients(int ID, List<Ingredient> selected) => 
            
            BaseDetachAllIngredientChildren(Repo, ID, selected);
        public ActionResult AttachRecipe(int iD, Recipe recipe) => throw new NotImplementedException();
    }
}
