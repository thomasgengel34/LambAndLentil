using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public abstract class BaseController<T> : Controller
            where T : BaseEntity, IEntity, new()
    {
        private string className;

        public int PageSize { get; set; } 
        internal static IRepository<T> Repo;

        public BaseController(IRepository<T> repository)
        {
            Repo = repository;
            className = new RepositoryHelperMethods().GetClassName<T>();
            PageSize = 8;
        }


        //  TODO: filter   
        public ActionResult BaseIndex(int? page = 1)
        { 
            int pageInt = page ?? 1;
            ListEntity<T> model = new ListEntity<T>
            {
                ListT = new BaseEntity().GetIndexedModel(Repo, PageSize, pageInt),
                PagingInfo = new BaseEntity().PagingFunction(Repo, page, PageSize)
            };
            return View(UIViewType.Index.ToString(), model);
        }



        public ActionResult BaseDetails(int id)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return HandleNullItem();
            }
            else if (IsModelValid(item))
            {
                return HandleSuccess(item);
            }
            else
            {
                return HandleError(item);
            }

        }

        private ActionResult HandleError(T item) => View(UIViewType.Details.ToString(), item).WithError("Something is wrong with the data!");

        private ActionResult HandleSuccess(T item) => View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");

        private ActionResult HandleNullItem() => RedirectToAction(UIViewType.Index.ToString()).WithError("No " + className + " was found with that id.");

        public ActionResult BaseEdit(int id)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return HandleNullItem();
            }
            else
            {
                return HandleSuccess(item);
            }
        }

        private bool IsModelValid(T item) => new ModelValidation().IsModelValid<T>(item);

        public ActionResult BaseCreate()
        { 
            T item = new T();            
            return HandleSuccess(item);
        }



        public ActionResult BasePostEdit(T entity)
        {
            T item = entity;
            bool isValid = IsModelValid(entity);
            if (isValid)
            {
                Repo.Update(entity, entity.ID);
                return HandleSavedOrModified(entity);
            }
            else
            {
                return HandleError(item);
            }
        }

        private ActionResult HandleSavedOrModified(T entity) => RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format($"{entity.Name} has been saved or modified"));

        public ActionResult BaseDelete(int id = 1)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return HandleNullItem();
            }
            else
            {
                return View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");   //  TODO: in view,   Need a button with an Are You SURE you want to delete this?
            }
        }

        public ActionResult BaseDeleteConfirmed(int id = 1)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return HandleNullItem();
            }
            else
            {
                Repo.Remove(item);
                return HandleHasBeenDeleted(item);
            }
        }

        private ActionResult HandleHasBeenDeleted(T item) => RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format($"{item.Name} has been deleted"));

        public void BaseAddIngredientToIngredientsList(int id, string addedIngredient)
        {
            T item = Repo.GetById(id);

            if (item != null)
            {
                item.IngredientsList += String.Concat(", ", addedIngredient);
                Repo.Update(item, item.ID);
            }
        }
    }
}