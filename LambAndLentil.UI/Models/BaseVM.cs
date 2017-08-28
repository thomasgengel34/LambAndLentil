using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{
    public class BaseVM : BaseEntity, IEntity
    {
        public BaseVM() : base()
        {
            IngredientVMs = new List<IngredientVM>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
            Plans = new List<Plan>();
            ShoppingLists = new List<ShoppingList>();
            Persons = new List<Person>();
        }

        public int ID { get; set; }
        public List<IngredientVM> IngredientVMs { get; set; }
        public List<RecipeVM> RecipeVMs { get; set; }
        public List<MenuVM> MenuVMs { get; set; }
        public List<PlanVM> PlanVMs { get; set; }
        public List<ShoppingListVM> ShoppingListVMs { get; set; }
        public List<PersonVM> PersonVMs { get; set; }

        internal   List<T> GetIndexedModel<T>(IRepository<T > repository, int PageSize, int page = 1)
            where T : BaseVM, IEntity
        {
           

            var result = repository.GetAll()
                      .OrderBy(p => p.Name)
                      .Skip((page - 1) * PageSize)
                      .Take(PageSize);
            List<T> listVM = new List<T>();
            foreach (var item in result)
            {
                
                listVM.Add(item);
            }

            return   listVM;
        }



        internal   PagingInfo PagingFunction<T >(IRepository<T> repository, int page, int PageSize)
            where T :  BaseVM, IEntity
        {
            PagingInfo PagingInfo = new PagingInfo();
            PagingInfo.CurrentPage = page;
            PagingInfo.ItemsPerPage = PageSize;
            int totalItems = repository.Count();
            PagingInfo.TotalItems = totalItems;

            return PagingInfo;
        }


    }
}
