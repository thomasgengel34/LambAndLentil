﻿using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    internal class BasicCreate<T> : BaseControllerTest<T>
       where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        {
            Create();
        }

       private static void Create()
        {
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Create();
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Here it is!", adr.Message);
        }
    }
}
 