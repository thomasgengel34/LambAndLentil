using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    class DeleteReturnsIndexWithWarningWhen<T>: BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        public void  DeletingInvalidEntity()
        {
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            Assert.IsNotNull(view);
            string className = ClassName;
            if (ClassName == "ShoppingList")
            {
                className = "Shopping List";
            }
            Assert.AreEqual("No "+className+ " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }   
    }
}
