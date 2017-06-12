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
    public class PlansController : BaseController
    { 
        public PlansController(IRepository repo) : base(repo)
        {  List<Plan> plans = repository.Plans.ToList<Plan>();
            Plans = new Plans(plans); 
        }


        // GET: Plans
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(UIControllerType.Plans,page);
            return View(view.ViewName, view.Model); 
        }

        // GET: Plans/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete<Plan, PlansController, PlanVM>(UIControllerType.Plans, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<Plan, PlansController>(UIControllerType.Plans, id);
            }
            return BaseDetails<Plan, PlansController, PlanVM>(UIControllerType.Plans, id);
        }

        // GET: Plans/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = actionMethod;
            return BaseCreate<PlanVM>(actionMethod);
        }


        // GET: Plans/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Plan, PlansController, PlanVM>(UIControllerType.Plans, id);
        }



        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] PlanVM planVM)
        {
            return BasePostEdit<Plan, PlansController, PlanVM>(planVM);
        }


        // GET: Plans/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Plan, PlansController, PlanVM>(UIControllerType.Plans, id);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Plan, PlansController>(UIControllerType.Plans,id);
        }
    }
}
