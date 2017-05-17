using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{

    public interface IBaseVM
    {
        int ID { get; set; }
        string Name { get; set; }
        DateTime CreationDate { get; set; }
        DateTime ModifiedDate { get; set; }
        string AddedByUser { get; set; }
        string ModifiedByUser { get; set; }
    }
}