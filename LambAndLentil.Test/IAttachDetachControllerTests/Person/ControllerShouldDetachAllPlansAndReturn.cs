﻿using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParentType = LambAndLentil.Domain.Entities.Person;
using ChildType = LambAndLentil.Domain.Entities.Plan;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Person
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachAllPlansAndReturn: BaseControllerShouldDetachXAndReturn<ParentType, ChildType>
    {
        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied() => BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied() => BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnList() =>
           BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll();


        [TestMethod]
        public void DetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll() =>
BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll();

      
    }
}
 