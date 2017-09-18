using AutoMapper;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Tests.Infrastructure
{
    public static class AutoMapperConfigForTests
    {
        public static void InitializeMap()
        { 
            Mapper.Initialize(cfg =>
            {
                cfg.RecognizeDestinationPostfixes("VM");
                cfg.RecognizePostfixes("VM");
                cfg.CreateMap<Ingredient, Ingredient>();
                cfg.CreateMap<Ingredient, Ingredient>();
                cfg.CreateMap<Menu, Menu>();
                cfg.CreateMap<Menu, Menu>();
                cfg.CreateMap<Person, Person>();
                cfg.CreateMap<Person, Person>();
                cfg.CreateMap<Plan, Plan>();
                cfg.CreateMap<Plan, Plan>();
                cfg.CreateMap<Recipe, Recipe>();
                cfg.CreateMap<Recipe, Recipe>();
                cfg.CreateMap<ShoppingList, ShoppingList>();
                cfg.CreateMap<ShoppingList, ShoppingList>();
                //   cfg.ShouldMapProperty = pi =>
                //             pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);
                //cfg.AddProfile<FooProfile>(); not currently needed
                
            });
         }


        public static MapperConfiguration AMConfigForTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.RecognizeDestinationPostfixes("VM");
                cfg.RecognizePostfixes("VM");
                cfg.CreateMap<Ingredient, Ingredient>();
                cfg.CreateMap<Ingredient, Ingredient>();
                cfg.CreateMap<Menu, Menu>();
                cfg.CreateMap<Menu, Menu>();
                cfg.CreateMap<Person, Person>();
                cfg.CreateMap<Person, Person>();
                cfg.CreateMap<Plan, Plan>();
                cfg.CreateMap<Plan, Plan>();
                cfg.CreateMap<Recipe, Recipe>();
                cfg.CreateMap<Recipe, Recipe>();
                cfg.CreateMap<ShoppingList, ShoppingList>();
                cfg.CreateMap<ShoppingList, ShoppingList>();
                //cfg.AddProfile<FooProfile>(); not currently needed
            });
            return config;
        }
    }
}
