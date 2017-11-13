﻿using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{  
    public class PersonsGenericController<T> : BaseController<Person> ,IGenericController<T>
        where T: Person, new() 
    {

        public PersonsGenericController(IRepository<Person> repository) : base(repository) => Repo = repository;

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
        public ActionResult PostEdit([Bind(Include = "ID,FirstName,LastName, Name, Description, Weight,  MinCalories, MaxCalories, NoGarlic, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] T t)
        {
          

            return BasePostEdit(Repo, t);

        }


        // GET: Persons/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
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

        public ActionResult AttachIngredient(int? personID, Ingredient ingredient, int orderNumber=0)
          {
            return BaseAttach< Ingredient>(Repo, personID, ingredient);
        }

        public ActionResult DetachIngredient(int? personID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach< Ingredient>(Repo, personID, ingredient, AttachOrDetach.Detach);
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