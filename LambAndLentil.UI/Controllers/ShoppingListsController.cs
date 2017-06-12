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
    public class ShoppingListsController : BaseController
    {
        

        public ShoppingListsController(IRepository repo): base(repo)
        {
            List<ShoppingList>shoppingLists = repository.ShoppingLists.ToList<ShoppingList>();
            ShoppingLists = new ShoppingLists(shoppingLists);
        }


        // GET: ShoppingList
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(UIControllerType.ShoppingLists, page);
            return View(view.ViewName, view.Model);
        }

        // GET: ShoppingList/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete<ShoppingList, ShoppingListsController, ShoppingListVM>(UIControllerType.ShoppingLists, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<ShoppingList, ShoppingListsController>(UIControllerType.ShoppingLists, id);
            }
            return BaseDetails<ShoppingList, ShoppingListsController, ShoppingListVM>(UIControllerType.ShoppingLists, id);
        }

        // GET: ShoppingList/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate<ShoppingListVM>(actionMethod);
        }



        // GET: ShoppingList/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<ShoppingList, ShoppingListsController, ShoppingListVM>(UIControllerType.ShoppingLists,id);
        }



        // POST: ShoppingList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, Author, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] ShoppingListVM shoppingListVM)
        {
            return BasePostEdit<ShoppingList, ShoppingListsController, ShoppingListVM>(shoppingListVM);
        }

        // GET: ShoppingList/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int  id=1, UIViewType actionMethod = UIViewType.Delete)
        {
            return BaseDelete<ShoppingList, ShoppingListsController, ShoppingListVM>(UIControllerType.ShoppingLists,id);
        }

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<ShoppingList, ShoppingListsController>(UIControllerType.ShoppingLists, id);
        } 
    }
}
