using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.UI.Models
{
    public class IngredientVM: BaseVM,IBaseVM,IEntity
    {
        public IngredientVM():base()
        { 
        }

        public IngredientVM(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

      
        [DataType(DataType.MultilineText)]
        public string IngredientsList { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}