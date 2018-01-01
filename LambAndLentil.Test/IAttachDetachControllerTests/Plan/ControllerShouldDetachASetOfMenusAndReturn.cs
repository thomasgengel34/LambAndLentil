using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Plan;
using TChild = LambAndLentil.Domain.Entities.Menu;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Plan
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfMenusAndReturnRRRRR : BaseControllerShouldDetachXAndReturn<TParent, TChild>
    {


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients() => BaseDetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients() =>
            BaseDetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients(); 


        [TestMethod]
        public void DetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients() => 
            BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients(); 


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients() => 
            BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients(); 


        [TestMethod]
        public void DetailWithWarningWhenIDisNotForAFoundParentWhenDetachASetOfIngredients() => 
            BaseDetailWithWarningWhenIDisNotForAFoundParentWhenDetachASetOfIngredients(); 

       
           
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotFound() =>        
            BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients(); 
         
   
    }
}
