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
            controller.WithCallTo(c => c.BaseIndex( 1)).ShouldRenderView(UI.UIViewType.Index.ToString());
        }


        public void BaseRenderDetailsDefaultView()
        {
            controller.WithCallTo(c => c.BaseDetails( 1)).ShouldRenderDefaultView();
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
            Ingredient ingredient = new Ingredient();
            controller.WithCallTo(c => c.Detach(1, ingredient)).ShouldRenderDefaultView();
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
