using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public class MenusController : BaseController
    {
        //private IRepository repository;

        public MenusController(IRepository repo) : base(repo)
        { 
           // repository = repo;
            List<Menu> menus = repository.Menus.ToList<Menu>();
            Menus = new Menus(menus);
        }

        // GET: Menus
        public ViewResult Index(int page = 1)
        { 
           ViewResult view = BaseIndex(UIControllerType.Menus, page);
           return View(view.ViewName, view.Model); 
        }

        // GET: Menus/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete<Menu, MenusController, MenuVM>(UIControllerType.Menus, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<Menu, MenusController>(UIControllerType.Menus, id);
            }
            return BaseDetails<Menu, MenusController, MenuVM>(UIControllerType.Menus, id);
        }



        // GET: Menus/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate<MenuVM>(actionMethod);
        }


        // GET: Menus/Edit/5
        public ViewResult Edit(int id=1)
        {
            return BaseEdit<Menu, MenusController, MenuVM>(UIControllerType.Menus, id);  
        } 

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name, DayOfWeek, Description,CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser, MealType,Diners")] MenuVM menuVM)
        {
        return BasePostEdit<Menu, MenusController, MenuVM>(menuVM);
    }

        // GET: Menus/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int  id=1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Menu, MenusController, MenuVM>(UIControllerType.Menus, id);
        }

        

        // POST: Ingredients/Delete/5 
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Menu, MenusController>(UIControllerType.Menus, id);
        } 
    }
}
