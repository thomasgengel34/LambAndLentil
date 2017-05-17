using LambAndLentil.UI.Infrastructure.ModelMetaData.Filters;
using Ninject;
using Ninject.Extensions.Conventions;

namespace LambAndLentil.UI.Infrastructure.ModelMetaData
{

    public static class ModelMetaDataRegistry
    { 
        public static IKernel AddMetaDataBindings(IKernel kernel)
        { 
            kernel.Bind(x => x
                          .FromAssembliesMatching("LambAndLentil.UI.dll")
                          .SelectAllClasses().InheritedFrom(typeof(IModelMetadataFilter))
                          .BindAllInterfaces()); 
            return kernel;
        }
    }
}