using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Collections.Generic;

namespace LambAndLentil.UI.Controllers
{
    public class PlansController : PlansGenericController<Plan>
    {
        public PlansController(IRepository<Plan> repository) : base(repository)
        {
            Repo = repository;
        }

       // public static IRepository<Plan> Repo { get; private set; }
    }



    public class PlansGenericController<T > : BaseController<Plan>
          where T: Plan 
    {
        public PlansGenericController(IRepository<Plan> repository) : base(repository)
        {
            Repo = repository;
        }

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
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] Plan planVM)
        {
            return BasePostEdit(Repo, planVM);
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

        public ActionResult AttachIngredient(int? planID, Ingredient ingredient)
        {
            return BaseAttach<Ingredient>(Repo, planID, ingredient);
        }


        public ActionResult DetachIngredient(int? planID, Ingredient ingredient)
        {
            return BaseAttach< Ingredient>(Repo,planID, ingredient, AttachOrDetach.Detach);
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
    }
}
