using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    [TestCategory("Index")]
    public class MenusController_Index_Test<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    { 
        [Ignore]
        [TestMethod]
        public void FlagAnMenuFlaggedInAPerson() => 
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void FlagAnMenuFlaggedInTwoPersons() =>
             
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson() => 
            Assert.Fail();

         
    }
}
