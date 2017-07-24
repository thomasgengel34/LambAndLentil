using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LambAndLentil.Domain.Concrete
{
    public class EFDbContext : DbContext //, ILambAndLentilContext
    {
        public EFDbContext() : base("name=EFDbContext")
        {

        }

        public DbSet<Ingredient> Ingredient { get; set; } 
        public DbSet<Recipe> Recipe { get; set; } 
        public DbSet<Menu> Menu { get; set; } 
        public DbSet<Person> Person  { get; set; } 
        public DbSet<Plan> Plan { get; set; } 
        public DbSet<ShoppingList> ShoppingList  { get; set; } 


        public void Update<T>(T entity, int key) where T : BaseEntity, IEntity
        {
            using (var context = new EFDbContext())
            {
                T existing = context.Set<T>().Find(key);
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(entity);

                    // later search for collections on entity using reflection
                    foreach (Ingredient ingredient in existing.Ingredients)
                    {
                        var ingredientEntry = context.Entry(existing.Ingredients.Where(s => s.ID == ingredient.ID).FirstOrDefault());
                        ingredientEntry.State = EntityState.Deleted;
                    }
                    context.SaveChanges();
                    {

                        // later search for collections on entity using reflection
                        foreach (Ingredient ingredient in entity.Ingredients)
                        {
                            var ingredientEntry = context.Entry(entity.Ingredients.Where(s => s.ID == ingredient.ID).FirstOrDefault());
                            ingredientEntry.State = EntityState.Added;
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        public void Update<T>(EFDbContext context, T entity, int key) where T : BaseEntity, IEntity
        {
            using (var _context = context)
            {
                T existing = context.Set<T>().Find(key);
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(entity);

                    // later search for collections on entity using reflection
                    foreach (Ingredient ingredient in existing.Ingredients)
                    {
                        var ingredientEntry = context.Entry(existing.Ingredients.Where(s => s.ID == ingredient.ID).FirstOrDefault());
                        ingredientEntry.State = EntityState.Deleted;
                    }
                    context.SaveChanges();
                    {

                        // later search for collections on entity using reflection
                        foreach (Ingredient ingredient in entity.Ingredients)
                        {
                            var ingredientEntry = context.Entry(entity.Ingredients.Where(s => s.ID == ingredient.ID).FirstOrDefault());
                            ingredientEntry.State = EntityState.Added;
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        //public class EFDbContext<T> : DbContext where T : BaseEntity
        //{
        //    public EFDbContext() : base("name=EFDbContext")
        //    {

        //    }
            //public DbSet<Ingredient> Ingredient { get; set; }
            //public DbSet<Ingredients> Ingredients { get; set; }
            //public DbSet<Recipe> Recipe { get; set; }
            //public DbSet<Recipes> Recipes { get; set; }
            //public DbSet<Menu> Menus { get; set; }
            //public DbSet<Person> Persons { get; set; }
            //public DbSet<Plan> Plans { get; set; }
            //public DbSet<ShoppingList> ShoppingLists { get; set; }


        //}
    }
}

