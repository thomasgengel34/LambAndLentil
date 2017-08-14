using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.HtmlHelpers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{
    
    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsController_Index_Test
    { 
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static ListVM<Ingredient, IngredientVM> ilvm;
        static IRepository<Ingredient, IngredientVM> repo;
        static IngredientsController  controller;

        public IngredientsController_Index_Test()
        {
            AutoMapperConfigForTests.InitializeMap(); 
            ilvm = new ListVM<Ingredient, IngredientVM>();
            repo = new TestRepository<Ingredient, IngredientVM>();
            controller = SetUpController();
        }



        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index()
        {
            // Arrange
             

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index1()
        {
            // Arrange
           

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert 
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView2NotNull()
        {
            // Arrange 

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient, IngredientVM>)(view2.Model)).ListTVM.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView1Count5()
        {
            // Arrange 
          
            // Act
           ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView2Count0()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient, IngredientVM>)(view2.Model)).ListTVM.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }
         

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView1NameIsIndex()
        {
            // Arrange  

            // Act
            ViewResult view =  controller.Index(1);
           

            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView2NameIsIndex()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient, IngredientVM>)(view2.Model)).ListTVM.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrectCountIsFive()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);
            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            // Assert   
            Assert.AreEqual("IngredientsController_Index_Test P1", ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstIngredientAddedByUserIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual("Richard Roe", ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            // Assert   
            Assert.AreEqual("IngredientsController_Index_Test P2",  ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListTVM.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Count();

            // Assert   
            Assert.AreEqual("IngredientsController_Index_Test P3", ((ListVM<Ingredient, IngredientVM>)(view1.Model)).ListTVM.Skip(2).FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }



        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_IndexCanPaginate_ArrayLengthIsCorrect()
        {
            // Arrange
          

            // Act
            var result = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model; 

            // Assert 
            Assert.IsTrue(result.ListTVM.Count() == 5);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange
        

            // Act
            var result = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = result.ListTVM.ToArray();

            // Assert  
            Assert.AreEqual( "IngredientsController_Index_Test P1",ingrArray1[0].Name);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = (ListVM<Ingredient, IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = result.ListTVM.ToArray();

            // Assert  
            Assert.AreEqual("IngredientsController_Index_Test P3",ingrArray1[2].Name );
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
            ListVM<Ingredient, IngredientVM> resultT = (ListVM<Ingredient, IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange
       

            // Act 
            ListVM<Ingredient, IngredientVM> resultT = (ListVM<Ingredient, IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert   
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_TotalItemsCorrect()
        {
            // Arrange 

            // Act 
            ListVM<Ingredient, IngredientVM> resultT = (ListVM<Ingredient, IngredientVM>)((ViewResult)controller.Index(2)).Model;
              PagingInfo pageInfoT =resultT.PagingInfo;
            //   PagingInfo pageInfoT =ilvm.PagingInfo;

            // Assert 
            Assert.AreEqual(5, pageInfoT.TotalItems);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange
          

            // Act 
            ListVM<Ingredient, IngredientVM> resultT = (ListVM<Ingredient, IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
         

        private IngredientsController  SetUpController()
        {

            ilvm.ListT = new List<Ingredient> {
                new Ingredient {ID = int.MaxValue, Name = "IngredientsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = int.MaxValue-1, Name = "IngredientsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = int.MaxValue-2, Name = "IngredientsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Ingredient {ID = int.MaxValue-3, Name = "IngredientsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = int.MaxValue-4, Name = "IngredientsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (Ingredient ingredient in ilvm.ListT)
            {
                repo.AddT(ingredient);
            }

            IngredientsController  controller = new IngredientsController(repo);
            controller.PageSize = 3;

            return controller;

 
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


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";
            int count = int.MaxValue;
            try
            {

                for (int i = count; i > count - 6; i--)
                {
                    File.Delete(string.Concat(path, i, ".txt"));
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
