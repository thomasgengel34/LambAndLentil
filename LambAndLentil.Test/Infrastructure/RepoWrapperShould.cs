using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Infrastructure;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Concrete;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    public class RepoWrapperShould : IRepository
    {
        static IRepository<IngredientVM> baseRepo;
        static RepoWrapper<IngredientVM> repo;


        public RepoWrapperShould()
        {
             baseRepo= new TestRepository<IngredientVM>();
             repo = new RepoWrapper<IngredientVM>(baseRepo);
             
        }



        [TestMethod]
        public void BeAbleToUseGenericToCallTestRepository()
        {
            // Arrange

            // Act
           
            // Assert  
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void  BeAbleToUseGenericToCallJSONRepository()
        {
            // Arrange

            // Act
            IRepository<IngredientVM> baseJRepo = new JSONRepository<IngredientVM>();
            RepoWrapper<IngredientVM> Jrepo = new RepoWrapper<IngredientVM>(baseJRepo);

            // Assert  
            Assert.IsNotNull(Jrepo);
        }

        [Ignore]
        [TestMethod]
        public void PassThroughGetByIDToUnderlyingClass()
        {
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void PassThroughCountToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughGetAllToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughQueryToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughAddToUnderlyingClass()
        {
            Assert.Fail();
        }


        [TestMethod]
        public void PassThroughRemoveToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughUpdateToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughSaveToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughAttachAnIndependentChildToUnderlyingClass()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void PassThroughDetachAnIndependentChildToUnderlyingClass()
        {
            Assert.Fail();
        }
    }
}
