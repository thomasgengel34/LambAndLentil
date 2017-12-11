using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfIngredientsAndReturn : BaseControllerShouldDetachXAndReturn<IngredientType, IngredientType>
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
