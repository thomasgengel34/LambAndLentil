using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("IngredientsController")]
    [TestCategory("Index")]
    public class IngredientsController_Index_Test:IngredientsController_Test_Should
    { 
         
        
  
        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson() => Assert.Fail();
    }
}
