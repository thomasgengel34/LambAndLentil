using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    public class BasicIndex<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        internal static void Index()
        {
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        internal static void ContainsAllView2NotNull()
        {
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            Assert.IsNotNull(view2);
        }

        internal static void ContainsAllView1Count5()
        {
            IRepository<T> repo = BuildRepository();
            int count = repo.Count();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));

            ListEntity = (ListEntity<T>)((ViewResult)(controller.Index(1))).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray() as T[];
            int count1 = ingrArray1.Count();

            Assert.AreEqual(count, count1);
        }

        private static IRepository<T> BuildRepository()
        {
            IRepository<T> repo = new TestRepository<T>();
            ClassCleanup();
            T t1 = new T() { ID = 10001 };
            T t2 = new T() { ID = 10002 };
            T t3 = new T() { ID = 10003 };
            T t4 = new T() { ID = 10004 };
            T t5 = new T() { ID = 10005 };
            T t6 = new T() { ID = 10006 };
            repo.Save(t1);
            repo.Save(t2);
            repo.Save(t3);
            repo.Save(t4);
            repo.Save(t5);
            repo.Save(t6);
            return repo;
        }

        internal static void ContainsAllView2Count0()
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
        internal static void ContainsAllView1NameIsIndex()
        {
            ViewResult view = (ViewResult)Controller.Index(1);
            Assert.AreEqual(UIViewType.Index.ToString(), view.ViewName);
        }

        internal static void ContainsAllView1NameIsIndex__1()
        {
            ViewResult view = (ViewResult)Controller.Index(1);
            Assert.AreEqual("Index", view.ViewName);
        }

        // TODO: see if this test is redundant.  Maybe check the count on each page and the total instead of what it is doing. If so, rename. 
        internal static void ContainsAllView2NameIsIndex()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;


            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);


            Assert.AreEqual(UIViewType.Index.ToString(), view2.ViewName);
        }


        internal static void FirstPageNameIsIndex()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(UIViewType.Index.ToString(), view1.ViewName);
        }




        internal static void FirstItemNameIsCorrect()
        {
            T item = new T();
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(item.Name, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().Name);
        }




        internal static void FirstAddedByUserIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;


            ViewResult view1 = (ViewResult)Controller.Index(1);

            string userName = WindowsIdentity.GetCurrent().Name;
            Assert.AreEqual(userName, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }




        internal static void FirstModifiedByUserIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;

            Assert.AreEqual(userName, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }


        internal static void FirstCreationDateIsCorrect()
        {
            ClassCleanup();
            DateTime dateTime = DateTime.Now;
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
            T item = new T() { CreationDate = dateTime };
            ClassCleanup();
            repo.Save(item);
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();

            ViewResult view1 = (ViewResult)Controller.Index(1);

            Assert.AreEqual(dateTime, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }


        internal static void FirstModifiedDateIsCorrect()
        {
            IRepository<T> repo = BuildRepository();

            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));



            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            DateTime dateTime = ingrArray1[0].ModifiedDate;
            ViewResult view1 = (ViewResult)Controller.Index(1);

            Assert.AreEqual(dateTime, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }

        internal static void FirstPageIsNotNull()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            Assert.IsNotNull(view1);
        }


        internal static void SecondItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(ingrArray1[0].Name, ((ListEntity<T>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }

        internal static void ThirdItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual("ControllerTest3", ((ListEntity<T>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [Ignore]
        internal static void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }
         

        internal static void ContainsAllView1Count6()
        {
            int count = Repo.Count();

            ListEntity = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            Assert.AreEqual(count, count1);
        }






        // TODO: refactor and combine with other tests
        internal static void ShowAll()
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
        internal static void FlagAnPlanFlaggedInAPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        internal static void FlagAnPlanFlaggedInTwoPlans()
        {
            Assert.Fail();
        }

        [Ignore]
        internal static void WhenAFlagHasBeenRemovedFromOnePlanStillThereForSecondFlaggedPlan()
        {
            Assert.Fail();
        }


        internal static void ContainsAll()
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

        internal static void FirstPageCountIsCorrect()
        {
            Controller.PageSize = 8;
            ViewResult view = (ViewResult)Controller.Index(1);
            int count1 = ((ListEntity<T>)(view.Model)).ListT.Count();

            Assert.AreEqual(Controller.PageSize, count1);
        }



        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson() => Assert.Fail();
    }
}
