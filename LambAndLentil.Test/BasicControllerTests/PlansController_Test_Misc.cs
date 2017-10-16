using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_Test_Misc: PlansController_Test_Should
    {
          

        [TestMethod]
        public void IsPublic()
        {
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }




        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            // Arrange

            // Act 
            Controller.PageSize = 4;

            var type = typeof(PlansController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, Controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index()
        {
            // Arrange


            // Act
            ViewResult result = Controller.Index(1) as ViewResult;


            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllListEntityTVM()
        {
            // Arrange

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Plan>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<Plan>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(6, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(6, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            //Assert.AreEqual("P1", (( ListEntity<Plan,Plan>)(view1.Model)). ListEntityTVM.FirstOrDefault().Name);
            //Assert.AreEqual("P2", (( ListEntity<Plan,Plan>)(view1.Model)). ListEntityTVM.Skip(1).FirstOrDefault().Name);
            //Assert.AreEqual("P3", (( ListEntity<Plan,Plan>)(view1.Model)). ListEntityTVM.Skip(2).FirstOrDefault().Name);
            //Assert.AreEqual("P5", (( ListEntity<Plan,Plan>)(view2.Model)). ListEntityTVM.FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange

             ListEntity<Plan> ilListEntity = new  ListEntity<Plan>();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Plan>)(view1.Model)).ListT.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(6, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest1", (( ListEntity<Plan>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest2", (( ListEntity<Plan>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest3", (( ListEntity<Plan>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void  ListEntityTVMCtr_Index_SecondPageIsCorrect()
        {

        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel()
        {

            // Arrange


            // Act

             ListEntity<Plan> resultT = ( ListEntity<Plan>)((ViewResult)Controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(6, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            // Arrange



            // Action
            int totalItems = (( ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = (( ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = (( ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = (( ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(6, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange 

            // Act
            var result = ( ListEntity<Plan>)(Controller.Index(1)).Model; 

            // Assert 
            Assert.IsTrue(result.ListT.Count() == 6);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest1", result.ListT.First().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest3", result.ListT.Skip(2).First().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsNegative()
        {
            // Arrange

            // AutoMapperConfigForTests.AMConfigForTests();
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass); 
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString()); 
        }

        [TestMethod]
        [TestCategory("Details")]
        public void WorksWithValidPlanID()
        {
            // Arrange  

            // Act
            ActionResult ar= Controller.Details(int.MaxValue) ;
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult ;

            // Assert 
            Assert.IsNotNull(view); 
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof( Plan));
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDTooHigh()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDPastIntLimit()
        {
            // Arrange

            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult result = Controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsZero()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass); 
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString()); 
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange

            ViewResult view = Controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundPlan()
        {
            // Arrange


            // Act 
            var view = Controller.Delete(int.MaxValue) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual(6, Repo.Count());  // shows this does not actually delete anything
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidPlan()
        {
            // Arrange


            // Act 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Plan was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            //Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            //Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange
            int count = Repo.Count();
            // Act
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = " ListEntityTVM", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(count - 1, Repo.Count());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPlan()
        {
            // Arrange  
            Plan pVM = new Plan() { ID = 6000, Name = "test CanDeleteValidPlan" };
            int count = Repo.Count();
            Repo.Add(pVM);
            int countPlus = Repo.Count();

            // Act - delete the plan
            ActionResult result = Controller.DeleteConfirmed(pVM.ID);
            int countEnding = Repo.Count();
            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert 
            Assert.AreEqual("test CanDeleteValidPlan has been deleted", adr.Message);
            Assert.AreEqual(count, countEnding);
            Assert.AreEqual(count + 1, countPlus);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CanSetUpToEditPlan()
        {
            // Arrange 

            // Act  
            ViewResult view1 = (ViewResult)Controller.Edit(int.MaxValue);
            Plan p1   = (Plan)view1.Model;  
            ViewResult view2 = (ViewResult)Controller.Edit(int.MaxValue - 1);
            Plan p2 = (Plan)view2.Model;
            ViewResult view3 = (ViewResult)Controller.Edit(int.MaxValue - 2);
            Plan p3 = (Plan)view3.Model;


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(int.MaxValue, p1.ID);
            Assert.AreEqual(int.MaxValue - 1, p2.ID);
            Assert.AreEqual(int.MaxValue - 2, p3.ID);  
            Assert.AreEqual("PlansController_Index_Test P1", p1.Name);
            Assert.AreEqual("PlansController_Index_Test P2", p2.Name);
            Assert.AreEqual("PlansController_Index_Test P3", p3.Name);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentPlan()
        {
            // Arrange

            // Act
            Plan result = (Plan)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void  ListEntityTVMCtr_CreateReturnsNonNull()
        {
            // Arrange


            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }



        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ComputesCorrectTotalCalories()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectPlanElementsAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.PlansController", PlansController_Test_Should.Controller.ToString());
        }

        private class FakeRepository : TestRepository<Plan> { }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Fake Repostory")]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange
            FakeRepository fakeRepo = new FakeRepository();
            PlansController fController = new PlansController(fakeRepo);
            // Act
            ActionResult ar = fController.BaseAttach(fakeRepo, int.MaxValue, new Ingredient());
            // Assert

        }


        [Ignore]
        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditDescription()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditCreationDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditAddedByUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddIngredientToIngredients()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveIngredientFromIngredients()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddRecipeToRecipesList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveRecipeFromRecipesList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddMenuToMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveMenuFromMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditIngredientsList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        } 
    }
}
