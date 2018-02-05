using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    public class BasicIndex<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        public void Index()
        {
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        public void ContainsAllView2NotNull()
        {
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            Assert.IsNotNull(view2);
        }

        public void ContainsAllView1Count5()
        {
            int count = Repo.Count();

            ListEntity = (ListEntity<T>)((ViewResult)(Controller.Index(1))).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray() as T[];
            int count1 = ingrArray1.Count();

            Assert.AreEqual(6, count1);
        }

        public void ContainsAllView2Count0()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<T>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            Assert.AreEqual(0, count2);
        }

        // TODO: break up into multiple tests
        public void ContainsAllView1NameIsIndex()
        {
            ViewResult view = (ViewResult)Controller.Index(1); 
            Assert.AreEqual(UIViewType.Index.ToString(), view.ViewName); 
        }

        public void ContainsAllView1NameIsIndex__1()
        {
            ViewResult view = (ViewResult)Controller.Index(1);
            Assert.AreEqual("Index", view.ViewName);
        }

        // TODO: see if this test is redundant.  Maybe check the count on each page and the total instead of what it is doing. If so, rename. 
        public void ContainsAllView2NameIsIndex()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;


            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);


            Assert.AreEqual(UIViewType.Index.ToString(), view2.ViewName);
        }


        public void FirstPageNameIsIndex()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(UIViewType.Index.ToString(), view1.ViewName);
        }




        public void FirstItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual("ControllerTest1", ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().Name);
        }




        public void FirstAddedByUserIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;


            ViewResult view1 = (ViewResult)Controller.Index(1);


            Assert.AreEqual("John Doe", ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

         


        public void FirstModifiedByUserIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;

            Assert.AreEqual(userName, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }


        public void FirstCreationDateIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;


            ViewResult view1 = (ViewResult)Controller.Index(1);


            Assert.AreEqual(DateTime.MinValue, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }


        public void FirstModifiedDateIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;


            ViewResult view1 = (ViewResult)Controller.Index(1);


            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }

        public void FirstPageIsNotNull()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            Assert.IsNotNull(view1);
        }


        public void SecondItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual("ControllerTest2", ((ListEntity<T>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }

        public void ThirdItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual("ControllerTest3", ((ListEntity<T>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [Ignore]
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }

        public void ReturnIndexWithErrorWhenIDIsNegative()
        {
            ViewResult view = Controller.Details(-1) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            Assert.IsNotNull(view);
            Assert.AreEqual("No " + ClassName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        public void ContainsAllView1Count6()
        {
            int count = Repo.Count();

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            Assert.AreEqual(count, count1);
        }

    




        // TODO: refactor and combine with other tests
        public void ShowAll()
        {
            int repoCount = Repo.Count(); 

            ViewResult view1 = (ViewResult)Controller.Index(1); 

            int count1 = ((ListEntity<Recipe>)view1.Model).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Recipe>)view2.Model).ListT.Count();

            int count = count1 + count2;

            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(repoCount, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName); 
        }

        [Ignore] 
        public void FlagAnPlanFlaggedInAPlan()
        {
            Assert.Fail();
        }

        [Ignore] 
        public void FlagAnPlanFlaggedInTwoPlans()
        {
            Assert.Fail();
        }

        [Ignore] 
        public void WhenAFlagHasBeenRemovedFromOnePlanStillThereForSecondFlaggedPlan()
        {
            Assert.Fail();
        }


        public void ContainsAll()
        {
            int repoCount = Repo.Count();

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<T>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(repoCount, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);
        }

        public void FirstPageCountIsCorrect()
        {
            Controller.PageSize = 8;
            ViewResult view = (ViewResult)Controller.Index(1);
            int count1 = ((ListEntity<T>)(view.Model)).ListT.Count();

            Assert.AreEqual(Controller.PageSize, count1);
        }
    }
}
