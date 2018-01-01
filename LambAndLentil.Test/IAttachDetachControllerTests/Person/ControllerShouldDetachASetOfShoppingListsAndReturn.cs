using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Person;
using TChild = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Person
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfShoppingListsAndReturn : BaseControllerShouldDetachXAndReturn<TParent, TChild>
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
