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


        public static MapperConfiguration AMConfigForTests()
        {
            var config = new MapperConfiguration(cfg =>
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
            return config;
        } 
    }
}
