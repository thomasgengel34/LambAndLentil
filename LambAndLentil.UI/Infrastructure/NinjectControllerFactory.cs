using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.ModelMetaData;
using LambAndLentil.UI.Models;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

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
            //ninjectKernel.Bind<IRepository>().To<JSONRepository>();
            ninjectKernel.Bind<IRepository<Ingredient >>().To<JSONRepository<Ingredient >>();

            ninjectKernel.Bind<IRepository< Recipe >>().To<JSONRepository< Recipe>>();
            ninjectKernel.Bind<IRepository< Menu >>().To<JSONRepository< Menu >>();
            ninjectKernel.Bind<IRepository< Plan >>().To<JSONRepository< Plan >>();
            ninjectKernel.Bind<IRepository< ShoppingList >>().To<JSONRepository< ShoppingList >>();
            ninjectKernel.Bind<IRepository< Person >>().To<JSONRepository< Person >>();
            ninjectKernel = ModelMetaDataRegistry.AddMetaDataBindings(ninjectKernel);
        }
    }
}