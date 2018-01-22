using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent  = LambAndLentil.Domain.Entities.Recipe;
using TChild  = LambAndLentil.Domain.Entities.Recipe; 

namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachAllRecipesAndReturn : BaseControllerShouldDetachXAndReturn<TParent, TChild>
    {
        // Recipe cannot attach a recipe as a child 

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
 