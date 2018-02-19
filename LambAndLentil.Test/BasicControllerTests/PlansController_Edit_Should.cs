﻿
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Edit")]
    [TestClass]
    public class PlansController_Edit_Should: PlansController_Test_Should
    {
        public PlansController_Edit_Should():base()
        {

        }
        [Ignore]
        [TestMethod]
        public void CorrectPlansAreBoundInEdit()
        {
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CanSetUpToEditPlan()
        { 
            ViewResult view1 = (ViewResult)Controller.Edit(int.MaxValue);
            Plan p1 = (Plan)view1.Model;
            ViewResult view2 = (ViewResult)Controller.Edit(int.MaxValue - 1);
            Plan p2 = (Plan)view2.Model;
            ViewResult view3 = (ViewResult)Controller.Edit(int.MaxValue - 2);
            Plan p3 = (Plan)view3.Model;
             
            Assert.IsNotNull(view1);
            Assert.AreEqual(int.MaxValue, p1.ID);
            Assert.AreEqual(int.MaxValue - 1, p2.ID);
            Assert.AreEqual(int.MaxValue - 2, p3.ID);
            Assert.AreEqual("PlansController_Index_Test P1", p1.Name);
            Assert.AreEqual("PlansController_Index_Test P2", p2.Name);
            Assert.AreEqual("PlansController_Index_Test P3", p3.Name);
        }

         

        [TestMethod]
        public void ListEntityTVMCtr_CreateReturnsNonNull()
        { 
            ViewResult result = Controller.Create() as ViewResult;
             
            Assert.IsNotNull(result);
        }


    }
}
