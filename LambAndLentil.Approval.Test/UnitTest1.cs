using ApprovalTests;
using ApprovalTests.Utilities;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;


 #if DEBUGC:\Dev\TGE\LambAndLentil\LambAndLentil.Approval.Test\bin\
namespace LambAndLentil.Approval.Test
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class UnitTest1
    {
        [TestMethod]
        public void MyTestMethod()
        {
            // Approvals.Verify("Hello world\r\nWelcome to Approval Tests");   <== this works
           // MvcApprovals.VerifyMvcPage(new HomeController().Index);  < == this is advocated on the video but MvcApprovals is not present. Neither is PortFactory.  
           //Approvals.
         


        }
    }
}
 #endif