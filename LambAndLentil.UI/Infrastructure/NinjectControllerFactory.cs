 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using Moq;
using LambAndLentil.UI.Infrastructure.ModelMetaData;
using System.Collections.Generic;

namespace LambAndLentil.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IRepository>().To<EFRepository>(); 
            ninjectKernel= ModelMetaDataRegistry.AddMetaDataBindings(ninjectKernel);
        }
    }
}