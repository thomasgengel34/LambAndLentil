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
             ListVM<Ingredient,IngredientVM>  vm = new  ListVM<Ingredient, IngredientVM>();

            Assert.IsNotNull(vm.Entities);
            Assert.IsNotNull(vm.List);
        }
    }
}
