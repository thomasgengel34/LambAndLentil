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
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Name = "Newly Created";
            CreationDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            AddedByUser = WindowsIdentity.GetCurrent().Name;
            ModifiedByUser = WindowsIdentity.GetCurrent().Name;
        }
        [Key]
        [HiddenInput(DisplayValue = false)] 
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AddedByUser { get; set; }
        public string ModifiedByUser { get; set; }
    }
}
