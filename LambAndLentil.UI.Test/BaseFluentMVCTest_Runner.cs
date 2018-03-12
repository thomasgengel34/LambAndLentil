using System;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.FluentMVC.Test;
using LambAndLentil.UI.Controllers;
 

namespace LambAndLentil.FluentMVC.Test
{
    [TestClass]
    public class BaseFluentMVCTest_Runner
    { 
        [TestMethod]
        public void RunBaseFluentMVCTest_Runner()
        {
            TestsRunner<Ingredient>();

            TestsRunner<Recipe>();

            TestsRunner<Menu>();

            TestsRunner<Plan>();

            TestsRunner<ShoppingList>();

            TestsRunner<Person>();
        }



        public static void TestsRunner<T>()
            where T : BaseEntity, IEntity, new()
        { 
            BaseFluentMVCTest<T>.BaseRenderIndexDefaultView(); 
            BaseFluentMVCTest<T>.BaseRenderDetailsDefaultView();
            BaseFluentMVCTest<T>.BaseRendeDeleteDefaultView();
            BaseFluentMVCTest<T>.BaseRendeDeleteConfirmedDefaultView(); 
            BaseFluentMVCTest<T>.BaseDetachDefaultView();

            
            //public void RenderDetachAllDefaultView() => test.BaseDetachAllDefaultView();

          
            //public void RenderDetachASetOfDefaultView() => test.BaseDetachASetOfDefaultView();
        }

    }
}
