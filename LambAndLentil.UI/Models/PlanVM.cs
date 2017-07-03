using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LambAndLentil.UI.Models
{
    public class PlanVM : BaseVM, IBaseVM
    {
        public PlanVM()
        {
        }

        public PlanVM(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public ICollection<Menu> Menus { get; set; }

    }
}
