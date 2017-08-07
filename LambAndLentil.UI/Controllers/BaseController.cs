﻿using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.Web.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        public int PageSize { get; set; }

       

        private Type GetControllerType<TVM>()
        {

            char[] charsToTrim = { 'V', 'M' };
            string className = typeof(TVM).ToString().TrimEnd(charsToTrim);
            string controllerName = String.Concat(className, "sController");
            return Type.GetType(controllerName);
        }

        //  TODO: filter

        public ViewResult BaseIndex<T,TVM>(int page = 1)
            where T:BaseEntity,IEntity
            where TVM : BaseVM
        {
            PageSize = 8;
            ListVM<T,TVM> model = new ListVM<T,TVM>();
            // model.Entities = (IEnumerable<TVM>)BaseVM.GetIndexedModel<T,TVM>(PageSize, page);
            model.List =  BaseVM.GetIndexedModel<T,TVM>(PageSize, page);
            model.PagingInfo = BaseVM.PagingFunction<T,TVM>(page, PageSize);
            return View(UIViewType.Index.ToString(), model);
        }



        public ActionResult BaseDetails<T, TVM>(UIControllerType uIController, int id = 1, UIViewType actionMethod = UIViewType.Details)
            where T :BaseEntity,IEntity
           where TVM : BaseVM, IEntity
        {
          IRepository<T, TVM> repo = new  JSONRepository<T, TVM>();

            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete<T,TVM>(uIController, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<T,TVM>(uIController, id);
            }
            else
            {
                ActionResult result = GuardId<T,TVM>(uIController, id);
                MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally.  Move to Repository

                TVM item = repo.GetTVMById(id);

                // TVM itemVM = Mapper.Map<T, TVM>((T)item); Move to Repository
                if (result is EmptyResult)
                {
                    return View(UIViewType.Details.ToString(), item);
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

        public ViewResult BaseEdit<T, TVM>(UIControllerType uIControllerType, int id = 1, UIViewType actionMethod = UIViewType.Edit)
            where T : BaseEntity,IEntity
        where TVM : BaseVM, IEntity
        {
            IRepository<T, TVM> repo = new  JSONRepository<T, TVM>();

            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally.  Move to Repository
            ActionResult result = new EmptyResult();
            if (actionMethod == UIViewType.Delete)
            {
                result = BaseDelete<T,TVM>(UIControllerType.Ingredients, id = 1, UIViewType.Details);

            }
            else
            {
                if (id != 0)
                {
                    result = GuardId<T,TVM>(uIControllerType, id);
                }


                TVM item = repo.GetTVMById(id);


                if (result is EmptyResult)
                {
                    return View(UIViewType.Details.ToString(), item);
                }
            }
            return result as ViewResult;
        }

        public ActionResult BasePostEdit<T, TVM>(IBaseVM vm)
            where T : BaseEntity,IEntity
            where TVM : BaseVM, IEntity, new()
        {
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally. 
            IRepository<T, TVM> repo = new  JSONRepository<T, TVM>();
            IController TController = (IController)GetControllerType<TVM>();
            TVM item = (TVM)vm;
            // new ingredientVM - saves double writing the Create Post method
            if (vm.ID == 0 && ModelState.IsValid)
            {
                item = new TVM();

            }

            //TODO: move this to Repository
            // item = Mapper.Map<TVM, T>((TVM)vm);

            if (ModelState.IsValid)
            {

                repo.UpdateTVM(item, item.ID);
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been saved", vm.Name));
            }
            else
            {
                return View(UIViewType.Details.ToString(), vm).WithWarning("Something is wrong with the data!");
            }
        }


        public ActionResult BaseDelete<T,TVM>(UIControllerType uiControllerType, int id = 1, UIViewType actionMethod = UIViewType.Delete)
            where T:BaseEntity,IEntity
            where TVM : BaseVM, IEntity
        {
            IRepository<T,TVM> repo = new  JSONRepository<T,TVM>();
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally.  
            ActionResult result = GuardId<T,TVM>(uiControllerType, id);
            TVM item = repo.GetTVMById(id);

            if (actionMethod == UIViewType.Delete)
            {
                ViewBag.ActionMethod = UIViewType.Delete;
            }
            //  TVM vm = Mapper.Map<T, TVM>((T)item); MOVE TO Repository
            if (result is EmptyResult)
            {
                return View(UIViewType.Details.ToString(), item);
            }
            return result;
        }

        public ActionResult BaseDeleteConfirmed<T,TVM>(UIControllerType controllerType, int id)
            where T :BaseEntity,IEntity
            where TVM : BaseVM, IEntity
        {
            IRepository<T,TVM> repo = new  JSONRepository<T,TVM>();
            ActionResult result = GuardId<T,TVM>(controllerType, id);
            TVM item = repo.GetTVMById(id);
            repo.RemoveTVM(item);
            ViewBag.ActionMethod = UIViewType.Delete.ToString();
            if (result is EmptyResult)
            {
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been deleted", item.Name));
            }
            return result;
        }
                                                      
        public ActionResult BaseAttach<TParent, TParentVM, TChild, TChildVM>(int? parentID, int? childID, AttachOrDetach attachOrDetach = AttachOrDetach.Attach)
            where TParent : BaseEntity, IEntity
            where TParentVM:BaseVM, IEntity
            where TChild : BaseEntity, IEntity
            where TChildVM:BaseVM,IEntity
        {
             
            // conditions guard against people trying thngs out manually  
            IRepository< TParent,TParentVM> repository = new  JSONRepository<TParent,TParentVM>();
            IRepository<TChild,TChildVM> childRepository = new  JSONRepository<TChild,TChildVM>();
            string entity = typeof(TParent).ToString().Split('.').Last();
            string childEntity = typeof(TChild).ToString().Split('.').Last();
            ViewBag.listOfChildren =   childRepository.GetAllTVM();
            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)parentID;

                 
                BaseVM parentFromDB = repository.GetTVMById(parentNonNullID);
                TParent parent = Mapper.Map<BaseVM, TParent >(parentFromDB);
                    
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }

                if (childID == null)
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(entity + " was not found"); ;
                }
                else
                {
                    int childNonNullID = (int)childID;
                    IEntity child = childRepository.GetTById(childNonNullID);

                    if (child == null)
                    {
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning("Please choose a(n) " + childEntity);
                    }
                    else
                    {
                        if (attachOrDetach == AttachOrDetach.Detach)
                        {
                            repository.DetachAnIndependentChild<TParent, TChild>(parent.ID, child.ID);
                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");
                        }
                        else
                        {
                            repository.AttachAnIndependentChild<TParent, TChild>(parent.ID, child.ID);
                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + "was Successfully Attached!");
                        }
                    }
                }
            }
        }


        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        protected ActionResult GuardId<T,TVM>(UIControllerType tController, int id)
            where T:BaseEntity,IEntity
            where TVM : class, IEntity
        {
            IRepository<T,TVM> repo = new  JSONRepository<T,TVM>();

            TVM item = repo.GetTVMById(id);
            string className = new  RepositoryHelperMethods().GetClassName<TVM>();
            if (item == null)
            {
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithError("No " + className + "  was found with that id.");
            }
            return new EmptyResult();
        }




        //protected SelectList GetList<T>(IRepository<T,TVM> repo) 
        //    where T : BaseEntity, IEntity
        //    where TVM:BaseVM, IEntity
        //{
        //    var result = from m in repo.GetAll()
        //                 orderby m.Name
        //                 select new SelectListItem
        //                 {
        //                     Text = m.Name,
        //                     Value = m.ID.ToString()
        //                 };
        //    SelectList list = null;
        //    if (result.Count() == 0)
        //    {
        //        List<string> item = new List<string>();
        //        item.Add("Nothing was found");
        //        list = new SelectList(item);
        //    }
        //    else
        //    {
        //        list = new SelectList(result, "Value", "Text", result.First());
        //    }
        //    return list;
        //}


        //protected IEntity Get<T>(IRepository<T,TVM> repo, int id)
        //     where T : BaseEntity, IEntity
        //    where TVM : BaseVM, IEntity
        //{
        //    IEntity result = null;

        //    if (typeof(T) == typeof(Ingredient))
        //    {
        //        result = (from m in repo.GetAll()
        //                  where m.ID == id
        //                  select m).FirstOrDefault();
        //    }
        //    return result;
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //}


    }
}