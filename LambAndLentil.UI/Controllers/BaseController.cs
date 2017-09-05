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
    public abstract class BaseController<T> : Controller

            where T : BaseVM, IEntity, new()
    {
        private readonly string className;

        public int PageSize { get; set; }

        protected static IRepository<T> Repo { get; set; }

        public BaseController(IRepository<T> repository)
        {
            Repo = repository;
            className = new RepositoryHelperMethods().GetClassName<T>();
            MvcApplication.InitializeMap();  // needed for testing. really??
        }


        //private Type GetControllerType()
        //{
        //    string controllerName = String.Concat(className, "sController");
        //    return Type.GetType(controllerName);
        //}

        //  TODO: filter

        public ViewResult BaseIndex(IRepository<T> Repo,  int page = 1)
        {
            PageSize = 8;
            ListVM<T> model = new ListVM<T>();
            // model.Entities = (IEnumerable<TVM>)BaseVM.GetIndexedModel<T>(PageSize, page);
            model.ListT = new BaseVM().GetIndexedModel<T>(Repo,  PageSize, page);
            model.PagingInfo = new BaseVM().PagingFunction<T>(Repo,  page, PageSize);
            return View(UIViewType.Index.ToString(), model);
        }

        public ActionResult BaseDetails(IRepository<T> Repo,  UIControllerType uIController, int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ActionResult result;
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete(Repo,  uIController, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed(Repo,  uIController, id);
            }
            else if (actionMethod == UIViewType.Details)
            {
                result = GuardId(Repo,  uIController, id);

                if (result == null)
                {
                    return RedirectToAction(UIViewType.BaseIndex.ToString()).WithError("No " + className + " was found with that id.");
                }
                else
                {
                    T item = Repo.GetById(id);
                    bool IsValid = IsModelValid(item);

                    if (IsValid)
                    {
                        return View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");
                    }
                    else
                    {
                        return View(UIViewType.Details.ToString(), item).WithError("Something is wrong with the data!");
                    }
                }
            }
            else if (actionMethod == UIViewType.Delete)
            {
                T item = Repo.GetById(id);
                return View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");   // not exactly what we want!
            }
            else if (actionMethod == UIViewType.Edit)
            {
                 
                    result = GuardId(Repo,  uIController, id); 
                T item;
                if (result != null)
                {
                    item = Repo.GetById(id);
                }
                else
                {
                    return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(className + " was not found");
                }
                if (result is EmptyResult)
                {
                    return View(UIViewType.Details.ToString(), item);
                } 
                return result as ViewResult;
            }
            else
            {
                return View(UIViewType.Index);
            }
        }

        private bool IsModelValid(T item)
        {
            // TODO: implement
            // intended as a second line of defense. JSON errors should be caught in the Repo methods.  This should catch logical errors in the model and mimic EF's model validation protocol(s)
            //
            return new ModelValidation().IsModelValid<T>(item);
        }

        public ViewResult BaseCreate(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = UIViewType.Create;
            T vm = new T();
            vm.CreationDate = DateTime.Now;
            return View(UIViewType.Details.ToString(), vm);
        }

        //public ActionResult BaseEdit(IRepository<T> Repo,  UIControllerType uIControllerType, int id = 1, UIViewType actionMethod = UIViewType.Edit)
        //{
        //    return BaseDetails(Repo,  uIControllerType, 1, UIViewType.Edit);
        //ActionResult result = new EmptyResult();
        //if (actionMethod == UIViewType.Delete)
        //{
        //    result = BaseDelete(Repo,  UIControllerType.Ingredients, id = 1, UIViewType.Details);

        //}
        //else
        //{
        //    if (id != 0)
        //    {
        //        result = GuardId(Repo,  uIControllerType, id);
        //    }
        //    T  item;
        //    if (result != null)
        //    {
        //        item = Repo.GetById(id);
        //    }
        //    else
        //    {
        //        return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(className + " was not found");
        //    }
        //    if (result is EmptyResult)
        //    {
        //        return View(UIViewType.Details.ToString(), item);
        //    }
        //}
        //return result as ViewResult;
        //   }

        public ActionResult BasePostEdit(IRepository<T> Repo,  T vm)
        {
            T item = (T)vm;
            // new ingredientVM - saves double writing the Create Post method
            if (vm.ID == 0 && ModelState.IsValid)
            {
                item = new T();

            }


            if (ModelState.IsValid)
            {

                Repo.Update(item, item.ID);
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been saved or modified", vm.Name));
            }
            else
            {
                return View(UIViewType.Details.ToString(), vm).WithWarning("Something is wrong with the data!");
            }
        }


        public ActionResult BaseDelete(IRepository<T> Repo,  UIControllerType uiControllerType, int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ActionResult result = GuardId(Repo,  uiControllerType, id);
            ViewBag.ActionMethod = UIViewType.Delete;
            T item;
            if (result == null)
            {
                return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(className + " was not found");
            }
            else
            {
                item = Repo.GetById(id);
            }
            if (result is EmptyResult)
            {
                return View(UIViewType.Details.ToString(), item);
            }
            return result;
        }

        public ActionResult BaseDeleteConfirmed(IRepository<T> Repo,  UIControllerType controllerType, int id)
        {
            ActionResult result = GuardId(Repo,  controllerType, id);
            T item = Repo.GetById(id);
            Repo.Remove(item);
            ViewBag.ActionMethod = UIViewType.Delete.ToString();
            if (result is EmptyResult)
            {
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been deleted", item.Name));
            }
            return result;
        }

        public ActionResult BaseAttach<TParent, TChild>(int? parentID, int? childID, AttachOrDetach attachOrDetach = AttachOrDetach.Attach)
            where TParent : BaseVM, IEntity
            where TChild : BaseVM, IEntity
        {

            // conditions guard against people trying thngs out manually  
            IRepository<TParent> repository = new JSONRepository<TParent>();
            IRepository<TChild> childRepository = new JSONRepository<TChild>();
            string entity = typeof(TParent).ToString().Split('.').Last();
            string childEntity = typeof(TChild).ToString().Split('.').Last();
            ViewBag.listOfChildren = childRepository.GetAll();
            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)parentID;


                BaseVM parentFromDB = repository.GetById(parentNonNullID);
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
                    IEntity child = childRepository.GetById(childNonNullID);

                    if (child == null)
                    {
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning("Please choose a(n) " + childEntity);
                    }
                    else
                    {
                        if (attachOrDetach == AttachOrDetach.Detach)
                        {
                            repository.DetachAnIndependentChild<TChild>(parent.ID, child.ID);
                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");
                        }
                        else
                        {
                            repository.AttachAnIndependentChild<TChild>(parent.ID, child.ID);
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

        protected ActionResult GuardId(IRepository<T> Repo,  UIControllerType tController, int id)
        {
            T item = Repo.GetById(id);

            if (item == null)
            {
                return null;
            }
            else
            {
                return new EmptyResult();
            }
        }




        //protected SelectList GetList<T>(IRepository<T> repo) 
        //    where T : BaseEntity, IEntity
        //    where TVM:BaseVM, IEntity
        //{
        //    var result = from m in Repo.GetAll()
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


        //protected IEntity Get<T>(IRepository<T> Repo,  int id)
        //     where T : BaseEntity, IEntity
        //    where TVM : BaseVM, IEntity
        //{
        //    IEntity result = null;

        //    if (typeof(T) == typeof(Ingredient))
        //    {
        //        result = (from m in Repo.GetAll()
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