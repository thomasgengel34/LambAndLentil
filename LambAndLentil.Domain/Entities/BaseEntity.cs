using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;

namespace LambAndLentil.Domain.Entities
{
    public class BaseEntity:IPossibleChildren
    { 
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AddedByUser { get; set; }
        public string ModifiedByUser { get; set; }
        public string IngredientsList { get; set; }
        public bool CanHaveIngredentChild { get; set; }
        public bool CanHaveMenuChild { get; set; }
        public bool CanHavePersonChild { get; set; }
        public bool CanHavePlanChild { get; set; }
        public bool CanHaveRecipeChild { get; set; }
        public bool CanHaveShoppingListChild { get; set; }

        public BaseEntity()
        {
            Name = "Newly Created";
            Description = "not yet described";
            CreationDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            AddedByUser = WindowsIdentity.GetCurrent().Name;
            ModifiedByUser = WindowsIdentity.GetCurrent().Name;

            CanHaveIngredentChild = true;
            CanHavePersonChild = false;
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
    }
}
