using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParentType = LambAndLentil.Domain.Entities.Menu;
using ChildType = LambAndLentil.Domain.Entities.Ingredient;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachAllIngredientsAndReturn : BaseControllerShouldDetachXAndReturn<ParentType, ChildType>
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
 