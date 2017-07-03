using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{

    [Table("RECIPE.Recipe")]
    public class Recipe : BaseEntity, IEntity
    {
        public Recipe() : base()
        {
        }

        public Recipe(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public decimal Servings { get; set; }
        public MealType MealType { get; set; }
        public int? Calories { get; set; }
        public short? CalsFromFat { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

    }
}