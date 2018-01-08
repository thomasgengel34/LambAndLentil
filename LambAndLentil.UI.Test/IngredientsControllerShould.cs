using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.FluentMVCTesting;
using LambAndLentil.UI.Controllers;
using System.Linq.Expressions;
using System.Linq ;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.FluentMVC.Test
{
    [TestClass]
    public class IngredientsControllerShould
    {
        private IngredientsController controller;
        public  IRepository<Ingredient> Repo  { get; set; }

        [TestInitialize]
        public void Setup()
        {
          Repo  = new TestRepository<Ingredient>();

            controller = new IngredientsController(Repo);
        }


        [TestMethod]
        public void  RenderIndexDefaultView()
        {
            controller.WithCallTo(c => c.BaseIndex(Repo,1)).ShouldRenderView(UI.UIViewType.Index.ToString()); 
        }

        [TestMethod]
        public void RenderDetailsDefaultView()
        {
            controller.WithCallTo(c => c.BaseDetails(Repo,UI.UIControllerType.Ingredients,1,  UI.UIViewType.Details)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void RendeDeleteDefaultView()
        {
            controller.WithCallTo(c => c.BaseDelete(Repo,UI.UIControllerType.Ingredients,1,UI.UIViewType.Delete)).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void RendeDeleteConfirmedDefaultView()
        {
            controller.WithCallTo(c => c.BaseDeleteConfirmed(Repo, UI.UIControllerType.Ingredients, 1 )).ShouldRenderDefaultView();
        }
    }
}
