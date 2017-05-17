using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{
    public class BaseVM : BaseEntity, IBaseVM
    {
       internal BaseVM() : base()
        {
        }

        internal static ListVM GetIndexedModel(string t, IRepository repository, int PageSize,int page=1)
        {
            ListVM model = new ListVM();

            // TODO: eliminate switch
            switch (t)
            {
                case "Ingredients":
                    model.Ingredients = repository.Ingredients
                        .OrderBy(p => p.Name)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize);
                    break;
                case "Recipes":
                    model.Recipes = repository.Recipes
                        .OrderBy(p => p.Name)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize);
                    break;
                case "Menus":
                    model.Menus = repository.Menus
                         .OrderBy(p => p.Name)
                         .Skip((page - 1) * PageSize)
                         .Take(PageSize);
                    break;
                case "Plans":
                    model.Plans = repository.Plans
                         .OrderBy(p => p.Name)
                         .Skip((page - 1) * PageSize)
                         .Take(PageSize);
                    break;
                case "ShoppingLists":
                    model.ShoppingLists = repository.ShoppingLists
                         .OrderBy(p => p.Name)
                         .Skip((page - 1) * PageSize)
                         .Take(PageSize);
                    break;
                case "Persons":
                    model.Persons = repository.Persons
                          .OrderBy(p => p.Name)
                          .Skip((page - 1) * PageSize)
                          .Take(PageSize);
                    break;
                default:
                    break;
            }
            return model;
        }

       internal static BaseEntity GetBaseEntity(string entity, IRepository repository, int id)
        {
            BaseEntity item = new BaseEntity();
            switch (entity.ToLower())
            {
                case "ingredient":
                    item = (Ingredient)repository.Ingredients.Where(e => e.ID == (int)id).SingleOrDefault();
                    break;
                case "recipe":
                    item = (Recipe)repository.Recipes.Where(e => e.ID == (int)id).SingleOrDefault();
                    break;
                case "menu":
                    item = (Menu)repository.Menus.Where(e => e.ID == (int)id).SingleOrDefault();
                    break;
                case "person":
                    item = (Person)repository.Persons.Where(e => e.ID == (int)id).SingleOrDefault();
                    break;
                case "shoppinglist":
                    item = (ShoppingList)repository.ShoppingLists.Where(e => e.ID == (int)id).SingleOrDefault();
                    break;
                case "plan":
                    item = (Plan)repository.Plans.Where(e => e.ID == (int)id).SingleOrDefault();
                    break;
                default:
                    throw new Exception("I never heard of that class!");
                    break;
            }
            return item;
        }


        internal static int Save(string entity, IRepository repository, BaseEntity item)
        {
            int result=-1;
            switch (entity)
            {
                case "Ingredient":
                   // Ingredient ingredient = (Ingredient)GetBaseEntity(entity, repository, id); 
                    result= repository.Save((Ingredient)item);
                  
                    break;
                case "Recipe":
                    //Recipe recipe = (Recipe)GetBaseEntity(entity, repository, id);
                    result = repository.Save((Recipe)item);
                    break;
                case "Menu":
                    //Menu menu = (Menu)GetBaseEntity(entity, repository,id);
                    result = repository.Save((Menu)item);
                    break;
                case "Plan":
                    // Plan plan = (Plan)GetBaseEntity(entity, repository, id);
                    result = repository.Save((Plan)item);
                    break;
                case "Person":
                    //  Person person = (Person)GetBaseEntity(entity, repository, id);
                    result = repository.Save((Person)item);
                    break;
                case "ShoppingList":
                    //ShoppingList shoppingList = (ShoppingList)BaseVM.GetBaseEntity(entity, repository,id);
                    result = repository.Save((ShoppingList)item);
                    break;
                default:
                    break;
            }
            return result;
        }

      internal static PagingInfo PagingFunction(string entity, IRepository repository, int page, int PageSize )
        {
            PagingInfo PagingInfo = new PagingInfo();
            PagingInfo.CurrentPage = page;
            PagingInfo.ItemsPerPage = PageSize;
            int totalItems = 0;
            switch (entity)
            {
                case "Ingredients":
                    totalItems = repository.Ingredients.Count();
                    break;
                case "Recipes":
                    totalItems = repository.Recipes.Count();
                    break;
                case "Menus":
                    totalItems = repository.Menus.Count();
                    break;
                case "Persons":
                    totalItems = repository.Persons.Count();
                    break;
                case "Plans":
                    totalItems = repository.Plans.Count();
                    break;
                case "ShoppingLists":
                    totalItems = repository.ShoppingLists.Count();
                    break;
                default:
                    break;
            }
            PagingInfo.TotalItems = totalItems;

            return PagingInfo;
        }

        
    }
}
