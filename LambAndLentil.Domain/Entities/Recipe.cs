using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LambAndLentil.Domain.Entities
{

    [Table("RECIPE.Recipe")]
    public class Recipe:BaseEntity,IEntity
    {
        public Recipe() : base()
        { 
        }

        public Recipe(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public decimal Servings { get; set; }  
        public MealType MealType { get; set; } 
        public int? Calories { get; set; }  
        public short? CalsFromFat { get; set; }

       public virtual ICollection<Ingredient> Ingredients { get; set; }

        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Ingredient ingredient, decimal quantity = 0m)
        {
            CartLine line = lineCollection
                                     .Where(p => p.Ingredient.ID == ingredient.ID)
                                     .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Ingredient = ingredient, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Ingredient ingredient)
        {
            lineCollection.RemoveAll(l => l.Ingredient.ID == ingredient.ID);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Ingredient Ingredient { get; set; }
        public decimal Quantity { get; set; }

        public CartLine()
        {
            Quantity = 0;
        }
    }
}