using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
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
        static ListVM<IngredientVM> ilvm;
        static IRepository<IngredientVM> Repo;
        static IngredientsController controller;

        public IngredientsController_Index_Test()
        {
            AutoMapperConfigForTests.InitializeMap();
            ilvm = new ListVM<IngredientVM>();
            Repo = new TestRepository<IngredientVM>();
            controller = SetUpIngredientsController(Repo);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void Index()
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

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<IngredientVM>)(view2.Model)).ListT.Count();

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
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView2Count0()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<IngredientVM>)(view2.Model)).ListT.Count();

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
            ViewResult view = controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllIngredientsView2NameIsIndex()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<IngredientVM>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrectCountIsFive()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);
            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("IngredientsController_Index_Test P1", ((ListVM<IngredientVM>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstIngredientAddedByUserIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", ((ListVM<IngredientVM>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual("Richard Roe", ((ListVM<IngredientVM>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, ((ListVM<IngredientVM>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListVM<IngredientVM>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("IngredientsController_Index_Test P2", ((ListVM<IngredientVM>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ilvm = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = ilvm.ListT.ToArray();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<IngredientVM>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("IngredientsController_Index_Test P3", ((ListVM<IngredientVM>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
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
            var result = (ListVM<IngredientVM>)(controller.Index(1)).Model;

            // Assert 
            Assert.IsTrue(result.ListT.Count() == 5);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange


            // Act
            var result = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("IngredientsController_Index_Test P1", ingrArray1[0].Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = (ListVM<IngredientVM>)(controller.Index(1)).Model;
            IngredientVM[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("IngredientsController_Index_Test P3", ingrArray1[2].Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
            ListVM<IngredientVM> resultT = (ListVM<IngredientVM>)((ViewResult)controller.Index(2)).Model;
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
            ListVM<IngredientVM> resultT = (ListVM<IngredientVM>)((ViewResult)controller.Index(2)).Model;
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
            ListVM<IngredientVM> resultT = (ListVM<IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
            //   PagingInfo pageInfoT =ilvm.PagingInfo;

            // Assert 
            Assert.AreEqual(5, pageInfoT.TotalItems);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange


            // Act 
            ListVM<IngredientVM> resultT = (ListVM<IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        public IngredientsController SetUpIngredientsController(IRepository<IngredientVM> repo)
        {
            ilvm.ListT = new List<IngredientVM> {
                        new IngredientVM{ID = int.MaxValue, Name = "IngredientsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                        new IngredientVM{ID = int.MaxValue-1, Name = "IngredientsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                        new IngredientVM{ID = int.MaxValue-2, Name = "IngredientsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                        new IngredientVM{ID = int.MaxValue-3, Name = "IngredientsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                        new IngredientVM{ID = int.MaxValue-4, Name = "IngredientsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
                    }.AsQueryable();

            foreach (IngredientVM ingredientVM in ilvm.ListT)
            {
                Repo.Add(ingredientVM);
            }

            IngredientsController controller = new IngredientsController(Repo);
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

        [TestCleanup()]
        public void TestCleanup()
        {
            ClassCleanup();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";
        
            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
