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
         
        public PlansController(IRepository<Plan, PlanVM> repo)
        {

        }
         
        // GET: Plans
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex<Plan,PlanVM>(page);
            return View(view.ViewName, view.Model); 
        }

        // GET: Plans/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails<Plan,PlanVM>(UIControllerType.Plans, id, actionMethod);
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
            return BaseEdit<Plan, PlanVM>(UIControllerType.Plans, id);
        }



        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] PlanVM planVM)
        {
            return BasePostEdit<Plan, PlanVM>(planVM);
        }


        // GET: Plans/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Plan, PlanVM>(UIControllerType.Plans, id);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Plan, PlanVM>(UIControllerType.Plans,id);
        }
    }
}
