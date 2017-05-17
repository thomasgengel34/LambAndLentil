using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.UI.Models;
using System.Linq.Expressions;
using Microsoft.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Reflection;
using AutoMapper;
using System.Collections;

namespace LambAndLentil.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        public int PageSize { get; set; }

        protected IRepository repository;
        public Ingredients Ingredients { get; set; }
        public Recipes Recipes { get; set; }
        public Menus Menus { get; set; }
        public Persons Persons { get; set; }
        public Plans Plans { get; set; }
        public ShoppingLists ShoppingLists { get; set; }

        public BaseController(IRepository repo)
        {
            repository = repo;
        }

        // I want the filter here
        // currently not filtering because Filter() needs to be written - Maker is vestigial. Filter should handle ascending and descending alternatives for all columns. Whew.  
        public ViewResult BaseIndex(UIControllerType T, int page = 1)
        {
            PageSize = 8;
            //  string entity =  T.ToString().Split('.').Last();
            string entity = T.ToString();
            ListVM model = BaseVM.GetIndexedModel(entity, repository, PageSize, page);
            model.PagingInfo = BaseVM.PagingFunction(entity, repository, page, PageSize);
            return View(UIViewType.Index.ToString(), model);

        }

        public ActionResult BaseDetails<T, TController, TVM>(UIControllerType uIController, int id = 1)
            where T : BaseEntity
            where TController : BaseController
            where TVM : BaseVM
        {

            ActionResult result = GuardId<T, TController>(uIController, id);
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally. 
            string entity = typeof(T).ToString().Split('.').Last();
            BaseEntity item = BaseVM.GetBaseEntity(entity, repository, id);

            TVM itemVM = Mapper.Map<T, TVM>((T)item);
            if (result is EmptyResult)
            {
                return View(UIViewType.Details.ToString(), itemVM);
            }
            return result;
        }


        public ViewResult BaseCreate<TVM>(UIViewType actionMethod) where TVM : BaseVM, new()
        {
            ViewBag.ActionMethod = UIViewType.Create;
            TVM vm = new TVM();
            return View(UIViewType.Edit.ToString(), vm);
        }

        public ViewResult BaseEdit<T, TController, TVM>(UIControllerType uIControllerType, int id = 1, UIViewType actionMethod = UIViewType.Edit)
        where T : BaseEntity
        where TController : BaseController
        where TVM : BaseVM
        {
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally.  
            ActionResult result = new EmptyResult();  
            if (actionMethod == UIViewType.Delete)
            {
                 result=BaseDelete<T, TController, TVM>(UIControllerType.Ingredients, id = 1, UIViewType.Delete);
                 
            }
            else
            {
                if (id != 0)
                {
                    result = GuardId<T, TController>(uIControllerType, id);
                }

                string entity = typeof(T).ToString().Split('.').Last();
                BaseEntity item = BaseVM.GetBaseEntity(entity, repository, id);

                TVM itemVM = Mapper.Map<T, TVM>((T)item);
                if (result is EmptyResult)
                {
                    return View(UIViewType.Edit.ToString(), itemVM);
                }
               }
            return result as ViewResult; 
        }

        public ActionResult BasePostEdit<T, TController, TVM>(IBaseVM vm)
                     where T : BaseEntity, new()
                     where TController : BaseController
                     where TVM : BaseVM
        {
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally. 


            T item;
            // new ingredientVM - saves double writing the Create Post method
            if (vm.ID == 0)
            {
                item = new T();
            }

            item = Mapper.Map<TVM, T>((TVM)vm);

            if (ModelState.IsValid)
            {
                string entity = typeof(T).ToString().Split('.').Last();
                //   int testSuccess= BaseVM.Save(entity, repository, item);
                repository.Save<T>(item);
                return RedirectToAction<IngredientsController>(c => c.Index(1)).WithSuccess(string.Format("{0} has been saved", vm.Name));
            }
            else
            {
                return View(UIViewType.Edit.ToString(),vm).WithWarning("Something is wrong with the data!");
            }
        }


        public ActionResult BaseDelete<T, TController, TVM>(UIControllerType uiControllerType, int id = 1, UIViewType actionMethod = UIViewType.Delete)
            where T : BaseEntity
            where TController : BaseController
            where TVM : BaseVM
        {
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally.  
            ActionResult result = GuardId<T, TController>(uiControllerType, id);
            string entity = typeof(T).ToString().Split('.').Last();
            BaseEntity item = BaseVM.GetBaseEntity(entity, repository, id);
            if (actionMethod == UIViewType.Delete)
            {
                ViewBag.ActionMethod = UIViewType.Delete;
            }
            TVM vm = Mapper.Map<T, TVM>((T)item);
            if (result is EmptyResult)
            {
                return View(UIViewType.Details.ToString(), vm);
            }
            return result;
        }

        public ActionResult BaseDeleteConfirmed<T, TController>(UIControllerType uiControllerType, int id)
           where T : BaseEntity
            where TController : BaseController
        {
            ActionResult result = GuardId<T, TController>(uiControllerType, id);
            string entity = typeof(T).ToString().Split('.').Last();
            BaseEntity item = BaseVM.GetBaseEntity(entity, repository, id);
            repository.Delete<T>(id);
            ViewBag.ActionMethod = "delete";
            if (result is EmptyResult)
            {
                return RedirectToAction<IngredientsController>(c => c.Index(1)).WithSuccess(string.Format("{0} has been deleted", item.Name));
            }
            return result;
        }


        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        protected ActionResult GuardId<T, TController>(UIControllerType tController, int id)
            where T : BaseEntity
            where TController : BaseController
        {

            string entity = typeof(T).Name.ToLower();
            T item = (T)BaseVM.GetBaseEntity(entity, repository, id);
            if (item == null && entity == "shoppinglist")
            {
                return RedirectToAction<TController>(c => c.BaseIndex(tController, 1)).WithError("No shopping list was found with that id.");
            }
            else
            {
                if (item == null)
                {
                    return RedirectToAction<TController>(c => c.BaseIndex(tController, 1)).WithError("No " + entity + " was found with that id.");
                }
            }
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}