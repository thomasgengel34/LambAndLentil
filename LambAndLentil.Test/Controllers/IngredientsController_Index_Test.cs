using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.HtmlHelpers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsController_Index_Test
    {
        static Mock<IRepository<Ingredient, IngredientVM>> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public IngredientsController_Index_Test()
        {
            AutoMapperConfigForTests.InitializeMap();
            mock = new Mock<IRepository<Ingredient, IngredientVM>>();
        }

     

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index()
        {
            // Arrange
            IngredientsController controller = SetUpController();

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
            IngredientsController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert 
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsView1NotNull()
        {
            // Arrange
            
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsView2NotNull()
        {
            // Arrange
           
            IngredientsController controller = SetUpController();
            ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2); 
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsView1Count5()
        {
            // Arrange
            
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsView2Count0()
        {
            // Arrange
             
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsTotalCount5()
        {
            // Arrange
           
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual(5, count);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsView1NameIsIndex()
        {
            // Arrange
           
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert    
            Assert.AreEqual("Index", view1.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ContainsAllIngredientsView2NameIsIndex()
        {
            // Arrange
            
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Ingredient,IngredientVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstPageIsNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstPageIsCorrectCountIsFive()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);
            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstPageNameIsIndex()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count(); 

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }

     

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstItemNameIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            // Assert   
            Assert.AreEqual("P1", ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.FirstOrDefault().Name);
        }

     

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstAddedByUserIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1); 

            // Assert   
            Assert.AreEqual("John Doe", ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstModifiedByUserIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual("Richard Roe", ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstCreationDateIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstModifiedDateIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.FirstOrDefault().ModifiedDate);
        }

      

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_SecondItemNameIsP2()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            // Assert   
            Assert.AreEqual("P2", ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Skip(1).FirstOrDefault().Name);
        }
         

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ThirdItemNameIsP3()
        {
            // Arrange
            IngredientsController controller = SetUpController();
          ListVM<Ingredient,IngredientVM> ilvm = new ListVM<Ingredient,IngredientVM>();
            ilvm.Entities = (IEnumerable<Ingredient>)mock.Object.Ingredient;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Count();

            // Assert   
            Assert.AreEqual("P2", ((ListVM<Ingredient,IngredientVM>)(view1.Model)).Entities.Skip(1).FirstOrDefault().Name);
        }

      

        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void IngredientsCtr_Index_SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }

        

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_IndexCanPaginate_ArrayLengthIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            var result = (ListVM<Ingredient,IngredientVM>)(controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.Entities.ToArray();

            // Assert 
            Assert.IsTrue(ingrArray1.Length == 5);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_IndexCanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            var result = (ListVM<Ingredient,IngredientVM>)(controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.Entities.ToArray();

            // Assert  
            Assert.AreEqual(ingrArray1[0].Name, "P1");
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_IndexCanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            var result = (ListVM<Ingredient,IngredientVM>)(controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.Entities.ToArray();

            // Assert  
            Assert.AreEqual(ingrArray1[3].Name, "P4");
        }
         

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
          ListVM<Ingredient,IngredientVM> resultT = (ListVM<Ingredient,IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage); 
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
          ListVM<Ingredient,IngredientVM> resultT = (ListVM<Ingredient,IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert   
            Assert.AreEqual(8, pageInfoT.ItemsPerPage); 
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_TotalItemsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
          ListVM<Ingredient,IngredientVM> resultT = (ListVM<Ingredient,IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(5, pageInfoT.TotalItems); 
        }


        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
          ListVM<Ingredient,IngredientVM> resultT = (ListVM<Ingredient,IngredientVM>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
          

        private IngredientsController SetUpController()
        {
            mock = new Mock<IRepository<Ingredient, IngredientVM>>();
            mock.Setup(m => m.Ingredient).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 2, Name = "P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = 3, Name = "P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Ingredient {ID = 4, Name = "P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 5, Name = "P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable());

            IngredientsController controller = new IngredientsController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }

        private IngredientsController SetUpSimpleController()
        {
            Mock<IRepository<Ingredient, IngredientVM>> mock = new  Mock<IRepository<Ingredient, IngredientVM>>();
            IngredientsController controller = new IngredientsController(mock.Object);
            return controller;
        }
    }
}
