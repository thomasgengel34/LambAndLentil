using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using LambAndLentil.Domain.Abstract;

namespace LambAndLentil.Domain.Entities
{
    public class BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; } 
        public string ClassName { get; set; }
        public string DisplayName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AddedByUser { get; set; }
        public string ModifiedByUser { get; set; }
        public string IngredientsList { get; set; }
        public bool ChildCanBeAttached { get; set; }

 

        public BaseEntity()
        {
            Name = "Newly Created";
            DisplayName = Name;
            Description = "not yet described";
            CreationDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            AddedByUser = WindowsIdentity.GetCurrent().Name;
            ModifiedByUser = WindowsIdentity.GetCurrent().Name; 
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
       

        public List<T> GetIndexedModel<T>(IRepository<T> repository, int PageSize, int page = 1)
             where T : BaseEntity, IEntity
        { 
            var result = repository.GetAll()
                      .OrderBy(p => p.Name)
                      .Skip((page - 1) * PageSize)
                      .Take(PageSize);
            List<T> list = new List<T>();
            foreach (var item in result)
            {

                list.Add(item);
            }

            return list;
        }



        public PagingInfo PagingFunction<T>(IRepository<T> repository, int? page, int PageSize)
             where T : BaseEntity, IEntity
        {
            PagingInfo PagingInfo = new PagingInfo
            {
                CurrentPage = page ?? 1,
                ItemsPerPage = PageSize
            };
            int totalItems = repository.Count();
            PagingInfo.TotalItems = totalItems;

            return PagingInfo;
        }

        public virtual bool CanHaveChild(IEntity child) { return false; }


        public static List<IEntity> GetIEntityListFromIngredientsList(IEntity parent)
        {
            var ingredients = parent.Ingredients;
            List<IEntity> selection = new List<IEntity>();
            selection.AddRange(ingredients);
            return selection;
        }

        public static List<Ingredient> GetIngredientsListFromIEntityList(List<IEntity> list)
        {
            List<Ingredient> selection = new List<Ingredient>();
            foreach (var item in list)
            {
                selection.Add((Ingredient)item);
            }
            return selection;
        }
    }
}
