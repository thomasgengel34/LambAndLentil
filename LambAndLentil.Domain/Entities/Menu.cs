using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity
    {
        
        public Menu():base() 
        {
            ID = base.ID;
            Name = base.Name;
            Description = base.Description;
            CreationDate = base.CreationDate; 
            ModifiedDate = base.ModifiedDate;
            AddedByUser = base.AddedByUser; ;
            ModifiedByUser = base.ModifiedByUser;
        }

        public Menu(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public int Diners { get; set; }

        ICollection<Recipe> Recipes { get; set; }

    }
}
