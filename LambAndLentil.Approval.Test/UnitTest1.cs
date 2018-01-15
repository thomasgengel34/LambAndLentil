using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;


//#if DEBUG
namespace LambAndLentil.Approval.Test
{
    [TestClass]
    public class UnitTest1
    {
     

        [UseReporter(typeof(DiffReporter) )]
        public void InitialViewState()
        {
            Approvals.Verify("Hello, World!");
            
        }

         
    }
}
//#endif
