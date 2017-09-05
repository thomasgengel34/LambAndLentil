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
    public class PlansController : PlansGenericController<PlanVM>
    {
        public PlansController(IRepository<PlanVM> repository) : base(repository)
        {
            Repo = repository;
        }

        public static IRepository<PlanVM> Repo { get; private set; }
    }



    public class PlansGenericController<T > : BaseController<PlanVM>
          where T: PlanVM 
    {
        public PlansGenericController(IRepository<PlanVM> repository) : base(repository)
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
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] PlanVM planVM)
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

        public ActionResult AttachIngredient(int? planID, int? ingredientID)
        {
            return BaseAttach<PlanVM,  IngredientVM>(planID, ingredientID);
        }


        public ActionResult DetachIngredient(int? planID, int? ingredientID)
        {
            return BaseAttach<PlanVM,  IngredientVM>(planID, ingredientID, AttachOrDetach.Detach);
        }
    }
}
