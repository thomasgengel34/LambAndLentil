using LambAndLentil.Domain.Entities; 
using LambAndLentil.UI.Infrastructure.ModelMetaData;
using LambAndLentil.UI.Infrastructure.ModelMetaData.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Linq;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    [TestCategory("MetaData Bindings")]
    public class MetaDataBindingsTest
    {
        [TestMethod]
        public void MetaDataBindings_DoesNotReturnNull()
        {
            // Arrange 
            IKernel kernel = new StandardKernel();

            // Act
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);

            // Assert
            Assert.IsNotNull(kernel); 
        }
        [TestMethod]
        public void MetaDataBindings_BindingsAreCorrect_LabelConventionFilter()
        {
            // Arrange 
            IKernel kernel = new StandardKernel();

            // Act
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);

            // Assert
           var prop = kernel.Get<Ingredient>();   // this would be fine for a test for standard bindings
           var m  = kernel.GetAll<IModelMetadataFilter>().First().GetType();  
           
            Assert.AreEqual(typeof(LambAndLentil.UI.Infrastructure.ModelMetaData.Filters.LabelConventionFilter),  m); 

        }

        [TestMethod]
        public void MetaDataBindings_BindingsAreCorrect_ReadOnlyTemplateSelectorFilter()
        {
            // Arrange 
            IKernel kernel = new StandardKernel();

            // Act
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);

            // Assert
            var prop = kernel.Get<Ingredient>();   // this would be fine for a test for standard bindings
          
            var m2 = kernel.GetAll<IModelMetadataFilter>().Skip(1).First().GetType(); 
           
            Assert.AreEqual(typeof(LambAndLentil.UI.Infrastructure.ModelMetaData.Filters.ReadOnlyTemplateSelectorFilter), m2);
          
        }

        [TestMethod]
        public void MetaDataBindings_BindingsAreCorrect_WaterMarkConventionFilter()
        {
            // Arrange 
            IKernel kernel = new StandardKernel();

            // Act
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);

            // Assert
            var prop = kernel.Get<Ingredient>();   // this would be fine for a test for standard bindings

            
            var m = kernel.GetAll<IModelMetadataFilter>().Last().GetType();
 
            Assert.AreEqual(typeof(LambAndLentil.UI.Infrastructure.ModelMetaData.Filters.WatermarkConventionFilter), m); 
        }

        [TestMethod]
        public void MetaDataBindings_Bindings_CountIsCorrect()
        {
            // Arrange 
            IKernel kernel = new StandardKernel();

            // Act
            kernel = ModelMetaDataRegistry.AddMetaDataBindings(kernel);

            // Assert
            var prop = kernel.Get<Ingredient>();   // this would be fine for a test for standard bindings


            var m = kernel.GetAll<IModelMetadataFilter>().Count();

            Assert.AreEqual(3, m);
        }

      
    }
}
