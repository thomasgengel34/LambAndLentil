using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Infrastructure;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    public class RepoWrapperShould : IRepository
    {
        static IRepository<Ingredient> baseRepo;
        static RepoWrapper<Ingredient> Repo;


        public RepoWrapperShould()
        {
             baseRepo= new TestRepository<Ingredient>();
             Repo = new RepoWrapper<Ingredient>(baseRepo);
             
        }



        [TestMethod]
        public void BeAbleToUseGenericToCallTestRepository()
        {
            // Arrange

            // Act
           
            // Assert  
            Assert.IsNotNull(Repo);
        }

        [TestMethod]
        public void  BeAbleToUseGenericToCallJSONRepository()
        {
            // Arrange

            // Act
            IRepository<Ingredient> baseJRepo = new JSONRepository<Ingredient>();
            RepoWrapper<Ingredient> JRepo = new RepoWrapper<Ingredient>(baseJRepo);

            // Assert  
            Assert.IsNotNull(Repo);
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

        [Ignore]
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
