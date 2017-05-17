using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LambAndLentil.Domain.Entities
{
    public enum ContainerSizeUnit
    {
        OZ,
        LB,

        [Display(Name = "FL OZ")]
        FLOZ,
        Cup,
        Quart,
        Gallon,
        Gram,
        Liter,

        [Display(Name = "Not Applicable")]
        NA,
        unknown
    }
}