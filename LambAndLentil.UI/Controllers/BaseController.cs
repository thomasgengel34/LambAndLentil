using AutoMapper;
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
    public abstract class BaseController<T, TVM> : Controller
        where T : BaseEntity, IEntity
            where TVM : BaseVM, IEntity, new()
    {
        private readonly string className;

        public int PageSize { get; set; }

        protected static IRepository<T, TVM> repo { get; set; }

        public BaseController(IRepository<T, TVM> repository)
        {
            repo = repository;
            className = new RepositoryHelperMethods().GetClassName<TVM>();
            MvcApplication.InitializeMap();  // needed for testing
        }


        private Type GetControllerType()
        {
            string controllerName = String.Concat(className, "sController");
            return Type.GetType(controllerName);
        }

        //  TODO: filter

        public ViewResult BaseIndex(IRepository<T, TVM> repo, int page = 1)
        {
            PageSize = 8;
            ListVM<T, TVM> model = new ListVM<T, TVM>();
            // model.Entities = (IEnumerable<TVM>)BaseVM.GetIndexedModel<T,TVM>(PageSize, page);
            model.ListTVM = new BaseVM().GetIndexedModel<T, TVM>(repo, PageSize, page);
            model.PagingInfo = new BaseVM().PagingFunction<T, TVM>(repo, page, PageSize);
            return View(UIViewType.Index.ToString(), model);
        }

        public ActionResult BaseDetails(IRepository<T, TVM> repo, UIControllerType uIController, int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete(repo, uIController, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed(repo, uIController, id);
            }
            else
            {
                ActionResult result = GuardId(repo, uIController, id);
                // MvcApplication.InitializeMap(); needed for testing.  This is being run in Application_Start normally.  Move to Repository


                if (result == null)
                {
                    return RedirectToAction(UIViewType.BaseIndex.ToString()).WithError("No " + className + " was found with that id.");
                }
                else
                {
                    TVM item = repo.GetTVMById(id);
                    if (result is EmptyResult)
                    {
                        return View(UIViewType.Details.ToString(), item).WithError("Something is wrong with the data!");
                    }
                    else
                    {
                        return View(UIViewType.Details.ToString(), item).WithSuccess("Item Has been added or modified!");
                    }
                }
            }
        }


        public ViewResult BaseCreate(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = UIViewType.Create;
            TVM vm = new TVM();
            vm.CreationDate = DateTime.Now;
            return View(UIViewType.Details.ToString(), vm);
        }

        public ViewResult BaseEdit(IRepository<T, TVM> repo, UIControllerType uIControllerType, int id = 1, UIViewType actionMethod = UIViewType.Edit)
        {
            MvcApplication.InitializeMap();  // needed for testing.  This is being run in Application_Start normally.  Move to Repository
            ActionResult result = new EmptyResult();
            if (actionMethod == UIViewType.Delete)
            {
                result = BaseDelete(repo, UIControllerType.Ingredients, id = 1, UIViewType.Details);

            }
            else
            {
                if (id != 0)
                {
                    result = GuardId(repo, uIControllerType, id);
                }
                TVM item;
                if (result != null)
                {
                    item = repo.GetTVMById(id);
                }
                else
                {
                    return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(className + " was not found");
                }
                if (result is EmptyResult)
                {
                    return View(UIViewType.Details.ToString(), item);
                }
            }
            return result as ViewResult;
        }

        public ActionResult BasePostEdit(IRepository<T, TVM> repo, TVM vm)
        {
            //   // needed for testing.  This is being run in Application_Start normally.  
            IController TController = (IController)GetControllerType();
            TVM item = (TVM)vm;
            // new ingredientVM - saves double writing the Create Post method
            if (vm.ID == 0 && ModelState.IsValid)
            {
                item = new TVM();

            }


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


        public ActionResult BaseDelete(IRepository<T, TVM> repo, UIControllerType uiControllerType, int id = 1, UIViewType actionMethod = UIViewType.Delete)
        { 
            ActionResult result = GuardId(repo, uiControllerType, id); 
            ViewBag.ActionMethod = UIViewType.Delete; 
            TVM item;
            if (result != null)
            {
                item = repo.GetTVMById(id);
            }
            else
            {
                return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(className + " was not found");
            }
            if (result is EmptyResult)
            {
                return View(UIViewType.Details.ToString(), item);
            }
            return result;
        }

        public ActionResult BaseDeleteConfirmed(IRepository<T, TVM> repo, UIControllerType controllerType, int id)
        {
            ActionResult result = GuardId(repo, controllerType, id);
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
            where TParentVM : BaseVM, IEntity
            where TChild : BaseEntity, IEntity
            where TChildVM : BaseVM, IEntity
        {

            // conditions guard against people trying thngs out manually  
            IRepository<TParent, TParentVM> repository = new JSONRepository<TParent, TParentVM>();
            IRepository<TChild, TChildVM> childRepository = new JSONRepository<TChild, TChildVM>();
            string entity = typeof(TParent).ToString().Split('.').Last();
            string childEntity = typeof(TChild).ToString().Split('.').Last();
            ViewBag.listOfChildren = childRepository.GetAllTVM();
            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)parentID;


                BaseVM parentFromDB = repository.GetTVMById(parentNonNullID);
                TParent parent = Mapper.Map<BaseVM, TParent>(parentFromDB);

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

        protected ActionResult GuardId(IRepository<T, TVM> repo, UIControllerType tController, int id)
        {
            TVM item = repo.GetTVMById(id);

            if (item == null)
            {
                return null;
            }
            else
            {
                return new EmptyResult();
            }
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