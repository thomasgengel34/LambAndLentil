using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.FluentMVC.Test
{
    [TestClass]
    public class ShoppingListsControllerShould
    {
        private ShoppingListsController controller;
        public IRepository<ShoppingList> Repo { get; set; }
        private BaseFluentMVCTest<ShoppingList> test;

        [TestInitialize]
        public void Setup()
        {
            Repo = new TestRepository<ShoppingList>();
            controller = new ShoppingListsController(Repo);
            test = new BaseFluentMVCTest<ShoppingList>(controller, Repo);
        }

        [TestMethod]
        public void RenderIndexDefaultView() => test.BaseRenderIndexDefaultView();

        [TestMethod]
        public void RenderDetailsDefaultView() => test.BaseRenderDetailsDefaultView();

        [TestMethod]
        public void RendeDeleteDefaultView() => test.BaseRendeDeleteDefaultView();

        [TestMethod]
        public void RendeDeleteConfirmedDefaultView() => test.BaseRendeDeleteConfirmedDefaultView();

        [TestMethod]
        public void RenderDetachDefaultView() => test.BaseDetachDefaultView();

        [TestMethod]
        public void RenderDetachAllDefaultView() => test.BaseDetachAllDefaultView();

        [TestMethod]
        public void RenderDetachASetOfDefaultView() =>test.BaseDetachASetOfDefaultView();
    }
}
