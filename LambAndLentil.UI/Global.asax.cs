using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LambAndLentil.UI.Infrastructure;
using AutoMapper;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            InitializeMap();
        }
        public static void InitializeMap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Ingredient, IngredientVM>();
                cfg.CreateMap<IngredientVM, Ingredient>();
                cfg.CreateMap<Menu, MenuVM>();
                cfg.CreateMap<MenuVM, Menu>();
                cfg.CreateMap<PersonVM, Person>();
                cfg.CreateMap<Person, PersonVM>();
                cfg.CreateMap<PlanVM, Plan>();
                cfg.CreateMap<Plan, PlanVM>();
                cfg.CreateMap<Recipe, RecipeVM>();
                cfg.CreateMap<RecipeVM, Recipe>();
                cfg.CreateMap<ShoppingList, ShoppingListVM>();
                cfg.CreateMap<ShoppingListVM, ShoppingList>();
                //cfg.AddProfile<FooProfile>(); not currently needed

            });
        }
    }
}
