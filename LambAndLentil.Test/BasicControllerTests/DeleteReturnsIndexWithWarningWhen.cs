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
        internal static void  DeletingInvalidEntity()
        {
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            T t = new T();
            string displayName = t.DisplayName ;

            Assert.IsNotNull(view);
              Assert.AreEqual("No "+t.DisplayName+ " was found with that id.", adr.Message); 
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }   
    }
}
