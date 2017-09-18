﻿using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : IngredientsGenericController<Ingredient> {
        public IngredientsController(IRepository<  Ingredient> repository) : base(repository)
        {
            Repo=repository;
        } 
    }

    public class IngredientsGenericController< T> : BaseController<Ingredient>
          where T:Ingredient  
    {
        
        public IngredientsGenericController(IRepository<Ingredient> repository) : base(repository)
        {
            Repo = repository;
        }

        // GET: Ingredients  
        public ViewResult Index(int page = 1)
        {
            return BaseIndex(Repo,  page); 
        }
    
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo, UIControllerType.Ingredients, id,actionMethod);
        } 

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        } 

        // GET: Ingredients/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo,  UIControllerType.Ingredients, id, UIViewType.Edit);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate, ModifiedDate,  IngredientsList")]  Ingredient ingredient)
        {
            return BasePostEdit(Repo,  ingredient);
        }

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete(Repo,  UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Ingredients, id);
        }

    }
}
