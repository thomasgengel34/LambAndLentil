using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{


    public class PlansGenericController<T > : BaseController<Plan>,IGenericController<T>
          where T: Plan 
    {
        public PlansGenericController(IRepository<Plan> repository) : base(repository) => Repo = repository;

        // GET: Plans
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(Repo,  page);
            return View(view.ViewName, view.Model);
        }

        // GET: Plans/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo,  UIControllerType.Plans, id, actionMethod);
        }

        //GET: Plans/Create
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = actionMethod;
            return BaseCreate(actionMethod);
        }


        // GET: Plans/Edit/5
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo,  UIControllerType.Plans, id, UIViewType.Edit);
        }



        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] T t)
        {
            return BasePostEdit(Repo, t);
        }


        // GET: Plans/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete(Repo, UIControllerType.Plans, id);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Plans, id);
        }

        public ActionResult AttachIngredient(int? planID, Ingredient ingredient, int orderNumber=0)
        {
            return BaseAttach<Ingredient>(Repo, planID, ingredient, AttachOrDetach.Attach, orderNumber);
        }


        public ActionResult DetachIngredient(int? planID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach< Ingredient>(Repo,planID, ingredient, AttachOrDetach.Detach, orderNumber);
        }

        public ActionResult AttachRecipe(int? planID, Recipe recipe)
        {
            return BaseAttach<Recipe>(Repo, planID, recipe);
        }


        public ActionResult DetachRecipe(int? planID, Recipe recipe)
        {
            return BaseAttach<Recipe>(Repo, planID, recipe, AttachOrDetach.Detach);
        }

        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "")
        {
            BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);
        }

        public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID);

        public void DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected); 
        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) =>   
         BaseAttach(Repo, recipeID, recipe);
        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => throw new NotImplementedException();
    }
}
