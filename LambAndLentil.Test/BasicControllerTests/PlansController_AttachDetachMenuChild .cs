using System;
using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class PlansController_AttachDetachChild : PlansController_Test_Should
    {
       
         [Ignore]
        [TestMethod]
        public void SuccessfullyAttachChild()
        {
            Assert.Fail();
        }



          [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstMenuChild()
        {
            Assert.Fail();
        }

        
        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        { 
            //Plan.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            //Plan.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            //Plan.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            //Plan.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            //Repo.Save((Plan)Plan);
            //int initialMenuCount = Plan.Menus.Count();
             
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<Plan> selected = Plan.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            //Controller.DetachASetOf(Plan, selected);
            //Plan returnedPlan = Repo.GetById(Plan.ID);
             
          //  Assert.AreEqual(initialMenuCount - 2, returnedPlan.Menus.Count());
        }


        [TestMethod]
        public void  DetachtheLastMenuChild() => BaseDetachTheLastMenuChild(Repo, Controller, Plan);

      

       
        [TestMethod]
        public void  DetachAllMenuChildren() =>   BaseDetachAllMenuChildren( Repo, Controller, Plan ); 

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullMenuChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownMenuChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}
