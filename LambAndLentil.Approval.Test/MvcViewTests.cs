using System;
using System.Web.Mvc;
using ApprovalTests.Asp; 
using ApprovalTests.Reporters; 
using NUnit.Framework;
using LambAndLentil.UI.Controllers;

#if DEBUG

namespace Tests
{
    [TestFixture]
    public class MvcViewTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
        public void InitialViewState()
        {
            PortFactory.MvcPort = 37768;

            Func<ActionResult> calculateUrlForView = new HomeController().TestInitialViewState;
            
            MvcApprovals.VerifyMvcPage(calculateUrlForView);
        }


        [Test]
        [UseReporter(typeof(FileLauncherReporter), typeof(ClipboardReporter))]
        public void PlayingState()
        {
            PortFactory.MvcPort = 37768;

            Func<ActionResult> calculateUrlForView = new HomeController().TestPlayingViewState;
           
            MvcApprovals.VerifyMvcPage(calculateUrlForView);
        }
    
    }
}

#endif