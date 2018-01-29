using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("IngredientsController")]
    [TestCategory("Details")]
    public class IngredientsController_Detail_Should : IngredientsController_Test_Should
    {
        public IngredientsController_Detail_Should()
        {

        }
         
      

        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {
            int count = Repo.Count();

            Controller.DeleteConfirmed(int.MaxValue);
            Ingredient item = Repo.GetById(int.MaxValue);

            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(item);
            //   Assert.Fail();  // TODO: make sure the correct item was deleted before removing this line 
        }

         

        [TestMethod]
        public void BeSuccessfulWithValidIngredientID()
        {
            // sut = system.under.test 
            Ingredient sut = new Ingredient { ID = 60000 };

            Repo.Save(sut);
            ActionResult ar = Controller.Details(sut.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            ViewResult viewResult = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewResult.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        } 
    }
}
