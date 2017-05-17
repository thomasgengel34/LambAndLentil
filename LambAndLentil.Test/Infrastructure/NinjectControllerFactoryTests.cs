using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.ModelMetaData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    public class NinjectControllerFactoryTests
    {
        
        [TestMethod]
        public void NinjectDataBindings_Bindings_IngredientIsCorrect()
        {
            // Arrange 
            IKernel kernel = new StandardKernel();

            // Act
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);
            var ingredient = kernel.Get<Ingredient>();

            // Assert 
            Assert.IsInstanceOfType(ingredient, typeof(Ingredient));
        }

    }
}
