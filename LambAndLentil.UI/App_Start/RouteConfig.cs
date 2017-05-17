using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LambAndLentil.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*[/\\])?favicon\.((ico)|(png))(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:"HomeIndex",
                url:"",
                 defaults: new {
                     Controller = "Home",
                     action = "Index",
                     maker = (string)null, page = 1 }
                 );

            routes.MapRoute(
               name: "HomeAlone",
               url: "Home",
                defaults: new
                {
                    Controller = "Home",
                    action = "Index",
                    maker = (string)null,
                    page = 1
                }
                );

            routes.MapRoute(
              name: "HomeAbout",
              url: "Home/About",
               defaults: new
               {
                   Controller = "Home",
                   action = "About",
                   maker = (string)null,
                   page = 1
               }
               );

            routes.MapRoute(
             name: "HomeMisc",
             url: "Home/{action}",
              defaults: new
              {
                  Controller = "Home",
                  action = "Index",
                  maker = (string)null,
                  page = 1
              }
              );

            routes.MapRoute(
                name: "maker1 Route",
                url: "",
                defaults: new {
                    Controller = "Ingredients",
                    action = "ListAsc",
                    maker = (string)null, page = 1 }
                 );

            routes.MapRoute(
              name: "IngredientsAlone",
              url: "Ingredients",
               defaults: new
               {
                   Controller = "Ingredients",
                   action = "Index",
                   maker = (string)null,
                   page = 1
               }
               );

            routes.MapRoute(
              name: "IngredientsDeleteConfirmed(int id)",
              url: "Ingredients/DeleteConfirmed/{id}",
               defaults: new
               {
                   Controller = "Ingredients",
                   action = "DeleteConfirmed", 
                   id = 1
               }
               );

            routes.MapRoute(
             name: "IngredientsPostEdit(ingredientsVM)",
             url: "Ingredients/PostEdit/{ingredientsVM}",
              defaults: new
              {
                  Controller = "Ingredients",
                  action = "PostEdit" 
              }
              );

            routes.MapRoute(
           name: "IngredientsFoo",
           url: "Ingredients/Foo",
            defaults: new
            {
                Controller = "Ingredients",
                action = "Foo" 
            }
            );

            routes.MapRoute("SecondRoute",
                "Page{page}",
                new
                {
                    controller = "Ingredients",
                    action = "ListAsc",
                    maker = (string)null
                },
                new { page = @"\d+" }
                );

    //        routes.MapRoute("IngredientListMaker",
    //            "{category}",
    //            new {controller="Ingredients",action="ListAsc",page=1}
    //);

            routes.MapRoute("IngredientListPage",
                "{category}/ Page{page}",
                new { controller = "Ingredients", action = "ListAsc" },
                new { page = @"\d+" });

            routes.MapRoute("IngredientListPage2", "{controller}/{action}");

            routes.MapRoute(
               name: "RecipesIndex",
               url: "Recipes/{action}/{id}",
               defaults: new { controller = "Recipes", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "RecipesAlone",
               url: "Recipes",
               defaults: new { controller = "Recipes", action = "Index",  }
           ); 

            routes.MapRoute(
              name: "MenusIndex",
              url: "Menus/{action}/{id}",
              defaults: new { controller = "Menus", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "PersonsIndex",
             url: "Persons/{action}/{id}",
             defaults: new { controller = "Persons", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "PersonsAlone",
             url: "Persons",
             defaults: new { controller = "Persons", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "PlansIndex",
            url: "Plans/{action}/{id}",
            defaults: new { controller = "Plans", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
             name: "PlansAlone",
             url: "Plans",
             defaults: new { controller = "Plans", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "ShoppingListsIndex",
            url: "ShoppingLists/{action}/{id}",
            defaults: new { controller = "Menus", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
           name: "ShoppingListAlone",
           url: "ShoppingList",
           defaults: new { controller = "ShoppingList", action = "Index", id = UrlParameter.Optional }
       );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "TwoData",
               url: "{controller}/{action}/{id}/{id1}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, id1 = UrlParameter.Optional }
           );
        }
    }
}
