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
            ninjectKernel.Bind<IRepository<Ingredient, IngredientVM>>().To<JSONRepository<Ingredient, IngredientVM>>();

            ninjectKernel.Bind<IRepository<Recipe,RecipeVM>>().To<JSONRepository<Recipe,RecipeVM>>();
            ninjectKernel.Bind<IRepository<Menu,MenuVM>>().To<JSONRepository<Menu,MenuVM>>();
            ninjectKernel.Bind<IRepository<Plan,PlanVM>>().To<JSONRepository<Plan,PlanVM>>();
            ninjectKernel.Bind<IRepository<ShoppingList,ShoppingListVM>>().To<JSONRepository<ShoppingList,ShoppingListVM>>();
            ninjectKernel.Bind<IRepository<Person,PersonVM>>().To<JSONRepository<Person,PersonVM>>();
            ninjectKernel = ModelMetaDataRegistry.AddMetaDataBindings(ninjectKernel);
        }
    }
}