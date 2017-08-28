using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.Models
{
    [TestClass]
    public class ListVMShould
    {
        [TestMethod]
        public void HaveEntitiesAndListInitializedOnConstruction()
        {
             ListVM< IngredientVM>  vm = new  ListVM<IngredientVM>();

            Assert.IsNotNull(vm.ListT); 
        }
    }
}
