using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TParent = LambAndLentil.Domain.Entities.Recipe;
using TChild = LambAndLentil.Domain.Entities.Plan;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
    public class ControllerShouldDetachAllIPlansAndReturn : BaseControllerShouldDetachXAndReturn<TParent, TChild>
    {
        // Recipe cannot have a Plan child

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParentWhenDetaching();


        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild() =>
        BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild();


        [TestMethod]
        public void DetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        {
            BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
        }
    }
}
