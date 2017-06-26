using System;
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
    }
}
