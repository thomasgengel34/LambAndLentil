using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
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
            //ninjectKernel.Bind<IRepository>().To<EFRepository>();
            ninjectKernel.Bind<IRepository<Ingredient, IngredientVM>>().To<EFRepository<Ingredient, IngredientVM>>();
            ninjectKernel.Bind<IRepository<Recipe,RecipeVM>>().To<EFRepository<Recipe,RecipeVM>>();
            ninjectKernel.Bind<IRepository<Menu,MenuVM>>().To<EFRepository<Menu,MenuVM>>();
            ninjectKernel.Bind<IRepository<Plan,PlanVM>>().To<EFRepository<Plan,PlanVM>>();
            ninjectKernel.Bind<IRepository<ShoppingList,ShoppingListVM>>().To<EFRepository<ShoppingList,ShoppingListVM>>();
            ninjectKernel.Bind<IRepository<Person,PersonVM>>().To<EFRepository<Person,PersonVM>>();
            ninjectKernel = ModelMetaDataRegistry.AddMetaDataBindings(ninjectKernel);
        }
    }
}