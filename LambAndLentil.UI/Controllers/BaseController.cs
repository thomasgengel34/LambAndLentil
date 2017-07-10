using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

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


        public ActionResult BaseDetails<T, TController, TVM>(UIControllerType uIController, int id = 1, UIViewType actionMethod = UIViewType.Details)
           where T : BaseEntity
           where TController : BaseController
           where TVM : BaseVM
        {
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete<T, TController, TVM>(uIController, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<T, TController>(uIController, id);
            }
            else
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
        }


        public ViewResult BaseCreate<TVM>(UIViewType actionMethod) where TVM : BaseVM, new()
        {
            ViewBag.ActionMethod = UIViewType.Create;
            TVM vm = new TVM();
            vm.CreationDate = DateTime.Now;
            return View(UIViewType.Details.ToString(), vm);
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
                result = BaseDelete<T, TController, TVM>(UIControllerType.Ingredients, id = 1, UIViewType.Details);

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
                    return View(UIViewType.Details.ToString(), itemVM);
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
            if (vm.ID == 0 && ModelState.IsValid)
            {
                item = new T();

            }

            item = Mapper.Map<TVM, T>((TVM)vm);

            if (ModelState.IsValid)
            {
                string entity = typeof(T).ToString().Split('.').Last();
                repository.Save<T>(item);
                UIControllerType controllerType = GetControllerType(entity);
                // vm.Name = vm.Name;   
                return RedirectToAction<TController>(c => c.BaseIndex(controllerType, 1)).WithSuccess(string.Format("{0} has been saved", vm.Name));
            }
            else
            {
                return View(UIViewType.Details.ToString(), vm).WithWarning("Something is wrong with the data!");
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

        public ActionResult BaseDeleteConfirmed<T, TController>(UIControllerType controllerType, int id)
           where T : BaseEntity
            where TController : BaseController
        {
            ActionResult result = GuardId<T, TController>(controllerType, id);
            string entity = typeof(T).ToString().Split('.').Last();
            BaseEntity item = BaseVM.GetBaseEntity(entity, repository, id);
            repository.Delete<T>(id);
            ViewBag.ActionMethod = UIViewType.Delete.ToString();
            if (result is EmptyResult)
            {
                return RedirectToAction<TController>(c => c.BaseIndex(controllerType, 1)).WithSuccess(string.Format("{0} has been deleted", item.Name));
            }
            return result;
        }

        public ActionResult BaseAttach<TParent, TChild>(int? parentID, int? childID) where TParent : Recipe 
            where TChild : Ingredient
        {  // conditions guard against people trying thngs out manually  
            ViewBag.listOfIngredients = GetListOfIngredients();
            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Recipe was not found");
            }
            else
            {
                string entity = typeof(TParent).ToString().Split('.').Last();
              
                   TParent parent = (TParent)repository.Recipes.Where(m => m.ID == parentID).SingleOrDefault();
                    if (parent == null)
                    {
                        // todo: log error - this could be a developer problem
                        return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Recipe was not found");
                    }
                
                if (childID == null)
                {    // this should have a warning that the child is not in the db
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Details }).WithWarning("Ingredient was not found"); ;
                }
                else
                {
                    TChild child = (TChild)repository.Ingredients.Where(m => m.ID == childID).SingleOrDefault();
                    if (child == null)
                    {
                        return RedirectToAction(UIViewType.Edit.ToString(), new { id = parentID, actionMethod = UIViewType.Details }).WithWarning("Please choose an ingredient");
                    }
                    else
                    {
                        parent.Ingredients.Add(child);
                        repository.Save<TParent>(parent);

                        return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Details }).WithSuccess("Successfully added!");
                    }
                }
            }
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

        //// gross violation of open/closed principle following
        private UIControllerType GetControllerType(string entity)
        {
            UIControllerType controllerType = UIControllerType.Ingredients;
            //// gross violation of open/closed principle following
            switch (entity)
            {
                case "Ingredient":
                    controllerType = UIControllerType.Ingredients;
                    break;
                case "Recipe":
                    controllerType = UIControllerType.Recipes;
                    break;
                case "Menu":
                    controllerType = UIControllerType.Menus;
                    break;
                case "Plan":
                    controllerType = UIControllerType.Plans;
                    break;
                case "Person":
                    controllerType = UIControllerType.Persons;
                    break;
                case "ShoppingList":
                    controllerType = UIControllerType.ShoppingLists;
                    break;
                default:
                    break;
            }
            return controllerType;
        }


        protected SelectList GetListOfRecipes()
        {
            var result = from m in repository.Recipes
                         orderby m.Name
                         select new SelectListItem
                         {
                             Text = m.Name,
                             Value = m.ID.ToString()
                         };
            SelectList list = null;
            if (result.Count() == 0)
            {
                List<string> item = new List<string>();
                item.Add("Nothing was found");
                list = new SelectList(item);
            }
            else
            {
                list = new SelectList(result, "Value", "Text", result.First());
            }
            return list;
        }

        protected SelectList GetListOfIngredients()
        {
            var result = from m in repository.Ingredients
                         orderby m.Name
                         select new SelectListItem
                         {
                             Text = m.Name,
                             Value = m.ID.ToString()
                         };
            SelectList list = null;
            if (result.Count() == 0)
            {
                List<string> item = new List<string>();
                item.Add("Nothing was found");
                list = new SelectList(item);
            }
            else
            {
                list = new SelectList(result, "Value", "Text", result.First());
            }
            return list;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}