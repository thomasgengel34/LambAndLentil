using LambAndLentil.Domain.Entities; 
using System.Linq;

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

    //public interface IRepository<T>
    //{
    //    IQueryable  item { get; } 
    //    void Save(T t);  
    //    void Delete(int ID);  
    //    IQueryable<T> items { get; }  
    //}
}
