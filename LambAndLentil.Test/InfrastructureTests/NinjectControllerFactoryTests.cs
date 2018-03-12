using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.ModelMetaData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    [TestCategory("Ninject")]
    public class NinjectControllerFactoryTests
    {
        
        [TestMethod]
        public void NinjectDataBindings_Bindings_IngredientIsCorrect()
        { 
            IKernel kernel = new StandardKernel();
             
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);
            var ingredient = kernel.Get<Ingredient>();
             
            Assert.IsInstanceOfType(ingredient, typeof(Ingredient));
        }

    }
}
