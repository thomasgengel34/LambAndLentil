 
using Microsoft.VisualStudio.TestTools.UnitTesting; 

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Edit")]
    [TestClass]
    public class PlansController_Edit_Should: PlansController_Test_Should
    {
        public PlansController_Edit_Should():base()
        {

        }
        [Ignore]
        [TestMethod]
        public void CorrectPlansAreBoundInEdit()
        {
            Assert.Fail();
        }
    }
}
