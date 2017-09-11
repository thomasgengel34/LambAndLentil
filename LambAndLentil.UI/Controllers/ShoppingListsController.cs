using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace LambAndLentil.UI.Controllers
{
    public class ShoppingListsController : ShoppingListsGenericController<ShoppingListVM>
    {
        public ShoppingListsController(IRepository<ShoppingListVM> repository) : base(repository)
        {
            Repo = repository;
        }
    }

    public class ShoppingListsGenericController<T> : BaseController<ShoppingListVM>
          where T : ShoppingListVM
    {
        public ShoppingListsGenericController(IRepository<ShoppingListVM> repository) : base(repository)
        {
            Repo = repository;
        }

       

        // GET: ShoppingList
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(Repo,  page);
            return View(view.ViewName, view.Model);
        }

        // GET: ShoppingList/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo,  UIControllerType.ShoppingLists, id, actionMethod);
        }

        // GET: ShoppingList/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        }



        // GET: ShoppingList/Edit/5
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo,  UIControllerType.ShoppingLists, id, UIViewType.Edit);  
        }



        //POST: ShoppingList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, Author, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] ShoppingListVM shoppingListVM)
        {
            return BasePostEdit(Repo,  shoppingListVM);
        }

        // GET: ShoppingList/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            return BaseDelete(Repo,  UIControllerType.ShoppingLists, id);
        }

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo,  UIControllerType.ShoppingLists, id);
        }

        public ActionResult AttachIngredient(int? shoppingListID, IngredientVM ingredientVM)
        {
            return BaseAttach<IngredientVM>(Repo, shoppingListID, ingredientVM);
        }


        public ActionResult DetachIngredient(int? shoppingListID, IngredientVM ingredientVM)
        {
            return BaseAttach<IngredientVM>(Repo, shoppingListID, ingredientVM, AttachOrDetach.Detach);
        }
    }
} 
