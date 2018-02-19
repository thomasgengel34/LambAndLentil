using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            ClassCleanup();
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
            T t2 = new T() { ID = 7000 };
            controller.WithCallTo(c => c.BaseDetails(7000)).ShouldRenderDefaultView();
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
            ClassCleanup();
           T parent  = new T() { ID = 1 };
            IRepository<T> repository = new TestRepository<T>();
            Ingredient childIngredient = new Ingredient() { ID = 2 };
            parent.Ingredients = new List<Ingredient>();
            parent.Ingredients.Add(childIngredient);
            repository.Save(parent);

            controller.WithCallTo(c => c.Detach(parent, childIngredient)).ShouldRenderDefaultView();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            Type type = typeof(T);
            string className = type.Name.Split('.').Last();
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + className + @"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
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
