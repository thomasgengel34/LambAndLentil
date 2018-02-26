using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class BasicIndex<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
       internal static void TestRunner()
        {
            ContainsAllView1Count5();
            ContainsAllView1NameIsIndex();
            ContainsAllView2Count0();
            ContainsAllView2NameIsIndex();
            ContainsAllView2NotNull();
            FirstCreationDateIsCorrect();
            FirstAddedByUserIsCorrect();
            FirstItemNameIsCorrect();
            FirstModifiedByUserIsCorrect();
            FirstModifiedDateIsCorrect();
            FirstPageIsNotNull();
            FirstPageNameIsIndex();
            Index();
            ReturnNonNullIndex();
            SecondItemNameIsCorrect();
            SecondPageIsCorrect();
            ShowAll();
            ThirdItemNameIsCorrect();
        } 

       private static void Index()
        {
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

       private static void ContainsAllView2NotNull()
        {
            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)controller.Index(2);

            Assert.IsNotNull(view2);
        }

       private static void ContainsAllView1Count5()
        {
            IRepository<T> repo = Buildrepository();
            int count = repo.Count();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));

            ListEntity = (ListEntity<T>)((ViewResult)(controller.Index(1))).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray() as T[];
            int count1 = ingrArray1.Count();

            Assert.AreEqual(count, count1);
        }

        private static IRepository<T> Buildrepository()
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

       private static void ContainsAllView2Count0()
        {
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)controller.Index(2);

            int count2 = ((ListEntity<T>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            Assert.AreEqual(0, count2);
        }

        // TODO: break up into multiple tests
       private static void ContainsAllView1NameIsIndex()
        {
            ViewResult view = (ViewResult)controller.Index(1);
            Assert.AreEqual(UIViewType.Index.ToString(), view.ViewName);
        }

       private static void ContainsAllView1NameIsIndex__1()
        {
            ViewResult view = (ViewResult)controller.Index(1);
            Assert.AreEqual("Index", view.ViewName);
        }

        // TODO: see if this test is redundant.  Maybe check the count on each page and the total instead of what it is doing. If so, rename. 
       private static void ContainsAllView2NameIsIndex()
        {
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;


            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)controller.Index(2);


            Assert.AreEqual(UIViewType.Index.ToString(), view2.ViewName);
        }


       private static void FirstPageNameIsIndex()
        {
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(UIViewType.Index.ToString(), view1.ViewName);
        }




       private static void FirstItemNameIsCorrect()
        {
            T item = new T();
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(item.Name, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().Name);
        }




       private static void FirstAddedByUserIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;


            ViewResult view1 = (ViewResult)controller.Index(1);

            string userName = WindowsIdentity.GetCurrent().Name;
            Assert.AreEqual(userName, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }




       private static void FirstModifiedByUserIsCorrect()
        {

            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;

            Assert.AreEqual(userName, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }


       private static void FirstCreationDateIsCorrect()
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

            ViewResult view1 = (ViewResult)controller.Index(1);

            Assert.AreEqual(dateTime, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }


       private static void FirstModifiedDateIsCorrect()
        {
            IRepository<T> repo = Buildrepository();

            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));



            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            DateTime dateTime = ingrArray1[0].ModifiedDate;
            ViewResult view1 = (ViewResult)controller.Index(1);

            Assert.AreEqual(dateTime, ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }

       private static void FirstPageIsNotNull()
        {
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;

            ViewResult view1 = (ViewResult)controller.Index(1);

            Assert.IsNotNull(view1);
        }


       private static void SecondItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual(ingrArray1[0].Name, ((ListEntity<T>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }

       private static void ThirdItemNameIsCorrect()
        {
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.AreEqual("ControllerTest3", ((ListEntity<T>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [Ignore]
       private static void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }
         

       private static void ContainsAllView1Count6()
        {
            int count = repo.Count();

            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            Assert.AreEqual(count, count1);
        }






        // TODO: refactor and combine with other tests
       private static void ShowAll()
        {
            int repoCount = repo.Count();

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<Recipe>)view1.Model).ListT.Count();

            ViewResult view2 = (ViewResult)controller.Index(2);

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
       private static void FlagAnPlanFlaggedInAPlan()
        {
            Assert.Fail();
        }

        [Ignore]
       private static void FlagAnPlanFlaggedInTwoPlans()
        {
            Assert.Fail();
        }

        [Ignore]
       private static void WhenAFlagHasBeenRemovedFromOnePlanStillThereForSecondFlaggedPlan()
        {
            Assert.Fail();
        }


       private static void ContainsAll()
        {
            int repoCount = repo.Count();

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)controller.Index(2);

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

       private static void FirstPageCountIsCorrect()
        {
            controller.PageSize = 8;
            ViewResult view = (ViewResult)controller.Index(1);
            int count1 = ((ListEntity<T>)(view.Model)).ListT.Count();

            Assert.AreEqual(controller.PageSize, count1);
        }

       private static void ReturnNonNullIndex()
        {
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);

        }


        private static void FlagAnIngredientFlaggedInTwoPersons() => Assert.Fail(); 
    }
}
