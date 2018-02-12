using System.Collections.Generic;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using TestStack.FluentMVCTesting;

namespace LambAndLentil.FluentMVC.Test
{
    internal class BaseFluentMVCTest<T>
        where T : BaseEntity, IEntity, new()
    {
        private BaseAttachDetachController<T> controller;
        private IRepository<T> Repo;
        private T t;

        public BaseFluentMVCTest(BaseAttachDetachController<T> controllerParameter, IRepository<T> repo)
        {
            controller = controllerParameter;
            Repo = repo;
            t = new T() { ID = 7000 };
        }

        public void BaseRenderIndexDefaultView()
        {
            controller.WithCallTo(c => c.BaseIndex(1)).ShouldRenderView(UI.UIViewType.Index.ToString());
        }


        public void BaseRenderDetailsDefaultView()
        {
            controller.WithCallTo(c => c.BaseDetails(1)).ShouldRenderDefaultView();
        }


        public void BaseRendeDeleteDefaultView()
        {
            controller.WithCallTo(c => c.BaseDelete(1)).ShouldRenderDefaultView();
        }


        public void BaseRendeDeleteConfirmedDefaultView()
        {
            controller.WithCallTo(c => c.BaseDeleteConfirmed(1)).ShouldRenderDefaultView();
        }


        public void BaseRendePostEditDefaultView()
        {
            T t = new T();
            controller.WithCallTo(c => c.BasePostEdit(t)).ShouldRenderDefaultView();
        }

        public void BaseDetachDefaultView()
        {
           T parent  = new T() { ID = 1 };
            Ingredient childIngredient = new Ingredient() { ID = 2 };
            parent.Ingredients = new List<Ingredient>();
            parent.Ingredients.Add(childIngredient);
            Repo.Save(parent);

            controller.WithCallTo(c => c.Detach(parent, childIngredient)).ShouldRenderDefaultView();
        }

        public void BaseDetachAllDefaultView()
        {
            IEntity ingredient = new Ingredient() { ID = 4000 };
            controller.WithCallTo(c => c.DetachAll(t,ingredient)).ShouldRenderDefaultView();
        }

        public void BaseDetachASetOfDefaultView()
        {
            List<IEntity> selected = new List<IEntity>();
            controller.WithCallTo(c => c.DetachASetOf(t, selected)).ShouldRenderDefaultView();
        }
    }
}
