using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.FluentMVC.Test
{
    [TestClass]
    public class PlansControllerShould
    {
        private PlansController controller;
        public IRepository<Plan> Repo { get; set; }
        private BaseFluentMVCTest<Plan> test;

        [TestInitialize]
        public void Setup()
        {
            Repo = new TestRepository<Plan>();
            controller = new PlansController(Repo);
            test = new BaseFluentMVCTest<Plan>(controller, Repo);
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
