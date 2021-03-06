﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Reflection;
using LambAndLentil.UI;

namespace LambAndLentil.Tests.Routes
{
    [TestClass]
    [TestCategory("Route")]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string Controller, string action,
             object routeProperties = null, string httpMethod = "GET")
        {

            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            // Act - process the route
            RouteData result
                = routes.GetRouteData(CreateHttpContext(url, httpMethod));
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, Controller,
                action, routeProperties));
        }


        private bool TestIncomingRouteResult(RouteData routeResult,
            string Controller, string action, object propertySet = null)
        {

            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase
                    .Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["Controller"], Controller)
                && valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name)
                            && valCompare(routeResult.Values[pi.Name],
                            pi.GetValue(propertySet, null))))
                    {

                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void TestRouteFail(string url)
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void Routes_TestIncomingRoutes()
        {

            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Home", "Home", "Index");
            TestRouteMatch("~/Home/Index", "Home", "Index");
         //   TestRouteMatch("~/Home/OtherAction", "Home", "Index");
            TestRouteMatch("~/Home/About", "Home", "About");
            TestRouteMatch("~/Ingredients/Index", "Ingredients", "Index");
            TestRouteMatch("~/Ingredients", "Ingredients", "Index");
            TestRouteMatch("~/Recipes/Index", "Recipes", "Index");
            TestRouteMatch("~/Recipes", "Recipes", "Index");
            TestRouteMatch("~/Persons/Index", "Persons", "Index");
            TestRouteMatch("~/Persons", "Persons", "Index");
            TestRouteMatch("~/Plans/Index", "Plans", "Index");
            TestRouteMatch("~/Plans", "Plans", "Index");
            TestRouteMatch("~/ShoppingList/Index", "ShoppingList", "Index");
            TestRouteMatch("~/ShoppingList", "ShoppingList", "Index");


           
            //TestRouteMatch("~/Home/About/MyId/More/Segments", "Home", "About",
            //    new
            //    {
            //        id = "MyId",
            //        catchall = "More/Segments"
            //    });

         //    TestRouteFail("~/Home/OtherAction");
            //TestRouteFail("~/Account/Index");
            //TestRouteFail("~/Account/About");
            // need to be sure Nav routes are ignored as well as other routes that should not be used
        }
    }
}
