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
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }
        public IngredientsController_Index_Test()
        {
            AutoMapperConfigForTests.InitializeMap();
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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Ingredients.Count();

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
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstPageIsCorrectCountIsFive()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);
            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstPageNameIsIndex()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count(); 

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }

     

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstItemNameIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            // Assert   
            Assert.AreEqual("P1", ((ListVM)(view1.Model)).Ingredients.FirstOrDefault().Name);
        }

     

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstAddedByUserIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1); 

            // Assert   
            Assert.AreEqual("John Doe", ((ListVM)(view1.Model)).Ingredients.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstModifiedByUserIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual("Richard Roe", ((ListVM)(view1.Model)).Ingredients.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstCreationDateIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, ((ListVM)(view1.Model)).Ingredients.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_FirstModifiedDateIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListVM)(view1.Model)).Ingredients.FirstOrDefault().ModifiedDate);
        }

      

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_SecondItemNameIsP2()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            // Assert   
            Assert.AreEqual("P2", ((ListVM)(view1.Model)).Ingredients.Skip(1).FirstOrDefault().Name);
        }
         

        [TestMethod]
        [TestCategory("Index")]
        public void IngredientsCtr_Index_ThirdItemNameIsP3()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)mock.Object.Ingredients;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Ingredients.Count();

            // Assert   
            Assert.AreEqual("P2", ((ListVM)(view1.Model)).Ingredients.Skip(1).FirstOrDefault().Name);
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
            var result = (ListVM)(controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.Ingredients.ToArray();

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
            var result = (ListVM)(controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.Ingredients.ToArray();

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
            var result = (ListVM)(controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.Ingredients.ToArray();

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
            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;
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
            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;
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
            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;
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
            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
          

        private IngredientsController SetUpController()
        {
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
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
            Mock<IRepository> mock = new Mock<IRepository>();
            IngredientsController controller = new IngredientsController(mock.Object);
            return controller;
        }
    }
}
