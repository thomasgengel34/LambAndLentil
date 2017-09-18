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
using System.Collections.Generic; 

namespace LambAndLentil.UI.Controllers
{
    public abstract class BaseController<T> : Controller
            where T : BaseEntity, IEntity, new()
    {
        public    string ClassName { get; private set; } 

        public int PageSize { get; set; }

        public static IRepository<T> Repo { get; set; }

        public static IRepository<Ingredient> IngredientRepo { get; set; }
        public static IRepository<Menu> MenuRepo { get; set; }

        public BaseController(IRepository<T> repository)
        {
            //if ( repository.GetType() ==typeof(TestRepository<Ingredient>))
            //{
            //    TestRepository<Ingredient> r = new TestRepository<Ingredient>();

            //    Repo =  r ;
            //}
            //else if (repository.GetType() == typeof(TestRepository<Menu>))
            //{
            //    TestRepository<Menu> r = new TestRepository<Menu>();

            //    Repo = (IRepository<T>)r;
            //}
           Repo = repository;
            MvcApplication.InitializeMap();  // needed for testing. really??
            ClassName = new RepositoryHelperMethods().GetClassName<T>();  
        }



        //  TODO: filter

        public ViewResult BaseIndex(IRepository<T> Repo, int page = 1)
        {
            PageSize = 8;
            ListEntity<T> model = new ListEntity<T>
            {
                // model.Entities = (IEnumerable<TVM>)BaseVM.GetIndexedModel<T>(PageSize, page);
                ListT = new BaseEntity().GetIndexedModel<T>(Repo, PageSize, page),
                PagingInfo = new BaseEntity().PagingFunction<T>(Repo, page, PageSize)
            };
            return View(UIViewType.Index.ToString(), model);
        }

        public ActionResult BaseDetails(IRepository<T> Repo, UIControllerType uIController, int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ActionResult result;
            ViewBag.Title = actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            {
                return BaseDelete(Repo, uIController, id);
            }
            else if (actionMethod == UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed(Repo, uIController, id);
            }
            else if (actionMethod == UIViewType.Details)
            {
                result = GuardId(Repo, uIController, id);

                if (result == null)
                {
                    return RedirectToAction(UIViewType.BaseIndex.ToString()).WithError("No " + ClassName + " was found with that id.");
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

                result = GuardId(Repo, uIController, id);
                T item;
                if (result != null)
                {
                    item = Repo.GetById(id);
                }
                else
                {
                    return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(ClassName + " was not found");
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
            T vm = new T
            {
                CreationDate = DateTime.Now
            };
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

        public ActionResult BasePostEdit(IRepository<T> Repo, T vm)
        {
             T item =  vm;
            // new ingredient - saves double writing the Create Post method
            bool isValid = IsModelValid(vm);
             
            if (isValid)
            {
                if (vm.ID == 0  )
                {
                    item = new T(); 
                }
                Repo.Update(item, item.ID);
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been saved or modified", vm.Name));
            }
            else
            {
                return View(UIViewType.Details.ToString(), vm).WithWarning("Something is wrong with the data!");
            }
        }


        public ActionResult BaseDelete(IRepository<T> Repo, UIControllerType uiControllerType, int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ActionResult result = GuardId(Repo, uiControllerType, id);
            ViewBag.ActionMethod = UIViewType.Delete;
            T item;
            if (result == null)
            {
                return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(ClassName + " was not found");
            }
            else
            {
                item = Repo.GetById(id);
                return View(UIViewType.Details.ToString(), item);
            }
        }

        public ActionResult BaseDeleteConfirmed(IRepository<T> Repo, UIControllerType controllerType, int id)
        {
            ActionResult result = GuardId(Repo, controllerType, id);
            T item = Repo.GetById(id);
            if (item == null)
            {
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithWarning(ClassName + " was not found");
            }
            else
            {
                Repo.Remove(item);
                ViewBag.ActionMethod = UIViewType.Delete.ToString();  // needed? evaluate when View is written 
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been deleted", item.Name));
            }
        }

        public ActionResult BaseAttach<TChild>(IRepository<T> Repo, int? parentID, TChild child, AttachOrDetach attachOrDetach = AttachOrDetach.Attach)
         where TChild : Ingredient 
        // where TChild : BaseVM, IEntity // todo: expand
        {
            // conditions guard against people trying thngs out manually  

            // child repository needs to be same generic class as parent repository
            // do it simply for now
            IRepository<TChild> childRepository;
            Type t = Repo.GetType();
            if (t == typeof(TestRepository<T>))
            {
                childRepository = new TestRepository<TChild>();
            }
            else if (t == typeof(JSONRepository<T>))
            {
                childRepository = new JSONRepository<TChild>();
            }
            else
            {
                throw new Exception("I have no idea what repository that is, but I do not like it.");
            }
            
            char[] charsToTrim = { 'V', 'M' }; 
            string entity = typeof(T).ToString().Split('.').Last();
            string childEntity = typeof(TChild).ToString().Split('.').Last().TrimEnd(charsToTrim); 
            ViewBag.listOfChildren = childRepository.GetAll();

            Ingredient ingredientChild;

            //if (typeof(T)==typeof(Ingredient))
            //{
              ingredientChild= Mapper.Map<Ingredient,Ingredient>(child);
            //}


            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)parentID;


                T parent = Repo.GetById(parentNonNullID);
                // T parentFromDB =Repo.GetById(parentNonNullID);
                //T  parent = Mapper.Map<BaseVM, T >(parentFromDB);

                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }

                if (child == null)
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(entity + " was not found"); ;
                }
                else
                {
                    if (child == null)
                    {
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning("Please choose a(n) " + childEntity);
                    }
                    else
                    { 
                        //string childClassName;  // obtain this
                        //Type z = Type.GetType(childClassName);
                        var childEntity1 = Mapper.Map<Ingredient, Ingredient >(child);  // expand


                        if (attachOrDetach == AttachOrDetach.Detach)
                        {

                            Repo.DetachAnIndependentChild<Ingredient>(parent.ID, ingredientChild);
                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity  + " was Successfully Detached!");
                        }
                        else
                        {
                            Repo.AttachAnIndependentChild<Ingredient>(parent.ID, ingredientChild);
                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity  + " was Successfully Attached!");
                        }
                    }
                }
            }
        }

        // needed??
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

       public ActionResult GuardId(IRepository<T> Repo, UIControllerType tController, int id)
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