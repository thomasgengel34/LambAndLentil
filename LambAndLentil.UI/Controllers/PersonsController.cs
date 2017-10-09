using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{ 
    public class PersonsController : PersonsGenericController<Person>
    {
        public PersonsController(IRepository<Person> repository) : base(repository)
        {
            Repo = repository;
        }

      //  public static IRepository<Person> Repo { get; private set; }
    }


    public class PersonsGenericController<T> : BaseController<Person> 
        where T: Person, new()
        //where Person :  BaseEntity,  IEntity
        //  where Person : BaseVM, IEntity, new()
    {

        public PersonsGenericController(IRepository<Person> repository) : base(repository)
        {
            Repo = repository;
        }

        // GET: Persons
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(Repo, page);
            return View(view.ViewName, view.Model);
        }

        // GET: Persons/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo,  UIControllerType.Persons, id, actionMethod);
        }


        //GET: Persons/Create
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = actionMethod;
            return BaseCreate(actionMethod);
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo,  UIControllerType.Persons, id, UIViewType.Edit);
        }



        //POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,FirstName,LastName, Name, Description, Weight,  MinCalories, MaxCalories, NoGarlic, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] Person person)
        {
          //  person.Name = String.Concat(person.FirstName, " ", person.LastName);

            return BasePostEdit(Repo, person);

        }


        // GET: Persons/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1)
        {
            return BaseDelete(Repo,  UIControllerType.Persons, id);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Persons, id);
        }

        public ActionResult AttachIngredient(int? personID, Ingredient ingredient)
          {
            return BaseAttach< Ingredient>(Repo, personID, ingredient);
        }

        public ActionResult RemoveIngredient(int? personID, Ingredient ingredient)
        {
            return BaseAttach< Ingredient>(Repo, personID, ingredient, AttachOrDetach.Detach);
        } 
    }
}
