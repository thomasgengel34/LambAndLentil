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
            ninjectKernel.Bind<IRepository<IngredientVM>>().To<JSONRepository<IngredientVM>>();

            ninjectKernel.Bind<IRepository< RecipeVM>>().To<JSONRepository< RecipeVM>>();
            ninjectKernel.Bind<IRepository< MenuVM>>().To<JSONRepository< MenuVM>>();
            ninjectKernel.Bind<IRepository< PlanVM>>().To<JSONRepository< PlanVM>>();
            ninjectKernel.Bind<IRepository< ShoppingListVM>>().To<JSONRepository< ShoppingListVM>>();
            ninjectKernel.Bind<IRepository< PersonVM>>().To<JSONRepository< PersonVM>>();
            ninjectKernel = ModelMetaDataRegistry.AddMetaDataBindings(ninjectKernel);
        }
    }
}