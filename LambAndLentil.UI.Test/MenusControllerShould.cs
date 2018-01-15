using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.FluentMVC.Test
{
    [TestClass]
    public class MenusControllerShould
    {
        private MenusController controller;
        public IRepository<Menu> Repo { get; set; }
        private BaseFluentMVCTest<Menu> test;

        [TestInitialize]
        public void Setup()
        {
            Repo = new TestRepository<Menu>();
            controller = new MenusController(Repo);
            test = new BaseFluentMVCTest<Menu>(controller, Repo);
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
