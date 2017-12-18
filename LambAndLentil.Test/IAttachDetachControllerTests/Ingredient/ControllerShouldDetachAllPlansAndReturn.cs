﻿using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Ingredient;
using TChild = LambAndLentil.Domain.Entities.Plan;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    public class ControllerShouldDetachAllIPlansAndReturn : BaseControllerShouldDetachXAndReturn<TParent, TChild>
    {
        // since Plan cannot be attached to an ingredient, these tests should return with an error


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
       => BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();

        [TestMethod]
        public void DetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        => BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
    }
}
