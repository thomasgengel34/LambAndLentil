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
        public ActionResult Details(int id = 1)
        {
            return BaseDetails<Plan,PlansController,PlanVM>(UIControllerType.Plans, id);
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
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat")] PlanVM planVM)
        {
            return BasePostEdit<Plan, PlansController, PlanVM>(planVM);
        }
         
        
        // GET: Plans/Delete/5
        public ActionResult Delete(int id = 1)
        {
            return BaseDelete<Plan, PlansController, PlanVM>(UIControllerType.Plans, id);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Plan, PlansController>(UIControllerType.Plans,id);
        }
    }
}
