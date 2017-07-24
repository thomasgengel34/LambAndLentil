using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public class PersonsController : BaseController
    { 
        


        // GET: Persons
        public ViewResult Index(int page=1)
        {
            ViewResult view = BaseIndex<Person,PersonVM>(page);
            return View(view.ViewName, view.Model);
        }

        // GET: Persons/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails<Person,PersonVM>(UIControllerType.Persons, id, actionMethod);
        }
         

        // GET: Persons/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = actionMethod;
            return BaseCreate<PersonVM>(actionMethod);
        }

        // GET: Persons/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Person,PersonVM>(UIControllerType.Persons, id);
        }



        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,FirstName,LastName, Name, Description, Weight,  MinCalories, MaxCalories, NoGarlic, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] PersonVM personVM)
        { 
            personVM.Name = String.Concat(personVM.FirstName, " ", personVM.LastName);
            
            return BasePostEdit<Person, PersonVM>(personVM);
            
        }


        // GET: Persons/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1)
        {
            return BaseDelete<Person, PersonVM>(UIControllerType.Persons, id);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Person, PersonVM>(UIControllerType.Persons, id);
        }
        }
}
