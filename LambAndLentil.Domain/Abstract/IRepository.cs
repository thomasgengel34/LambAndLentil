using LambAndLentil.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambAndLentil.Domain.Abstract
{ 
    public interface IRepository 
    {
        IQueryable Ingredient { get; }
        IQueryable Recipe { get; }
        IQueryable Menu { get; }
        IQueryable Plan { get; }
        IQueryable Person { get; }
        IQueryable ShoppingList { get; }          

        int Save(Ingredient ingredient);
        int Save(Recipe recipe);
        int Save(Menu menu);
        int Save(Plan plan);
        int Save(Person person);
        int Save(ShoppingList shoppingList);
        void Save<T>(BaseEntity item);
        void Delete<T>(int ID);


        IQueryable<Ingredient> Ingredients { get; }
        IQueryable<Recipe> Recipes { get; }
        IQueryable<Menu> Menus { get; }
        IQueryable<Plan> Plans { get; }
        IQueryable<Person> Persons { get; }
        IQueryable<ShoppingList> ShoppingLists { get; }

         
    }

  
}

public interface IRepository<T> where T:class
{
    T GetById(int id);

    IEnumerable<T> GetAll();

    IEnumerable<T> Query(Expression<Func<T, bool>> filter);

    void Add(T entity);

    void Remove(T entity);

    void Update(T entity);

    void Save();
}
