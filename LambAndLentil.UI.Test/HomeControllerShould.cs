using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.FluentMVCTesting;
using LambAndLentil.UI.Controllers;
using System.Linq.Expressions;
using System.Linq ;


namespace LambAndLentil.FluentMVC.Test
{
    [TestClass]
    public class HomeControllerShould
    {
        private HomeController controller;

        [TestInitialize]
        public void Setup()
        {
            controller = new HomeController();
        }


        [TestMethod]
        public void  RenderIndexDefaultView()
        {
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView(); 
        }

        [TestMethod]
        public void RenderAboutDefaultView()
        {
            controller.WithCallTo(c => c.About()).ShouldRenderDefaultView();
        }
    }
}
