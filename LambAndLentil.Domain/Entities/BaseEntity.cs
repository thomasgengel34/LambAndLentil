﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public class BaseEntity  
    {
        public BaseEntity()
        {
            Name = "Newly Created";
            Description = "not yet described";
            CreationDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            AddedByUser = WindowsIdentity.GetCurrent().Name;
            ModifiedByUser = WindowsIdentity.GetCurrent().Name;
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
            Plans = new List<Plan>();
            ShoppingLists = new List<ShoppingList>();
            Persons = new List<Person>();
        }

        public BaseEntity(DateTime creationDate) : this()
        {
            if (creationDate < DateTime.Today.AddYears(-40))
            {
                CreationDate = DateTime.Today.AddYears(-40);
            }
            else
            {
                CreationDate = creationDate;
            }

        }

       
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AddedByUser { get; set; }
        public string ModifiedByUser { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
        public   ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
        public virtual ICollection<Person>Persons { get; set; }
    }
}
