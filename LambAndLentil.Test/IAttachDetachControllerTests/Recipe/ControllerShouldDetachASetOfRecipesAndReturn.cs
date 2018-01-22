using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChildType = LambAndLentil.Domain.Entities.Recipe;
using ParentType = LambAndLentil.Domain.Entities.Recipe;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
     [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfRecipesAndReturn :  BaseControllerShouldDetachXAndReturn<ParentType, ChildType>
    {
        // Recipe cannot have a recipe child 

        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
       => BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();

        [TestMethod]
        public void DetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        => BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
    }
}
