﻿using AutoMapper;
using LambAndLentil.UI.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
        //        cfg.CreateMap<Ingredient, Ingredient>();
        //        cfg.CreateMap<Ingredient, Ingredient>();
        //        cfg.CreateMap<Menu, Menu>();
        //        cfg.CreateMap<Menu, Menu>();
        //        cfg.CreateMap<Person, Person>();
        //        cfg.CreateMap<Person, Person>();
        //        cfg.CreateMap<Plan, Plan>();
        //        cfg.CreateMap<Plan, Plan>();
        //        cfg.CreateMap<Recipe, Recipe>();
        //        cfg.CreateMap<Recipe, Recipe>();
        //        cfg.CreateMap<ShoppingList, ShoppingList>();
        //        cfg.CreateMap<ShoppingList, ShoppingList>();
        //        cfg.CreateMap<BaseEntity, BaseEntity>();
        //        cfg.ShouldMapProperty = pi =>
        //pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);
                //cfg.AddProfile<FooProfile>(); not currently needed 
            });
           
        }
    }
}
