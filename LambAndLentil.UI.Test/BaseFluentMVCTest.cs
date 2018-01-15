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

        public BaseFluentMVCTest(BaseAttachDetachController<T> controllerParameter, IRepository<T> repo)
        {
            controller = controllerParameter;
            Repo = repo; 
        }

        public void BaseRenderIndexDefaultView()
        {
            controller.WithCallTo(c => c.BaseIndex(Repo, 1)).ShouldRenderView(UI.UIViewType.Index.ToString());
        }


        public void BaseRenderDetailsDefaultView()
        {
            controller.WithCallTo(c => c.BaseDetails(Repo, UI.UIControllerType.Ingredients, 1, UI.UIViewType.Details)).ShouldRenderDefaultView();
        }


        public void BaseRendeDeleteDefaultView()
        {
            controller.WithCallTo(c => c.BaseDelete(Repo, UI.UIControllerType.Ingredients, 1, UI.UIViewType.Delete)).ShouldRenderDefaultView();
        }


        public void BaseRendeDeleteConfirmedDefaultView()
        {
            controller.WithCallTo(c => c.BaseDeleteConfirmed(Repo, UI.UIControllerType.Ingredients, 1)).ShouldRenderDefaultView();
        }


        public void BaseRendePostEditDefaultView()
        {
            T t = new T();
            controller.WithCallTo(c => c.BasePostEdit(Repo, t)).ShouldRenderDefaultView();
        }

        public void BaseDetachDefaultView()
        {
            Ingredient ingredient = new Ingredient();
            controller.WithCallTo(c => c.Detach(Repo, 1, ingredient)).ShouldRenderDefaultView();
        }

        public void BaseDetachAllDefaultView()
        {  
            controller.WithCallTo(c => c.DetachAll<Ingredient>(1)).ShouldRenderDefaultView();
        }

        public void BaseDetachASetOfDefaultView()
        {
            List<Ingredient> selected = new List<Ingredient>();
            controller.WithCallTo(c => c.DetachASetOf(1,selected)).ShouldRenderDefaultView();
        } 
    }
}
