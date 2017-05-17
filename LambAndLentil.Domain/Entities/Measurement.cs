using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LambAndLentil.Domain.Entities
{
    public enum Measurement
    {

        Each = 1,
        Tablespoon = 2,
        Teaspoon = 3,
        Cup = 4,
        [Display(Name = "Quarter Cup")]
        QuarterCup = 7,
        Ounce = 8,
        Pound = 9,
        [Display(Name = "1/4 tspn")]
        teaspn14 = 13,
        link = 14,
        [Display(Name = "12 OZ can")]
        can12oz = 18,
        [Display(Name = "Fluid OZ")]
        FluidOz = 20,
        [Display(Name = "150gm")]
        _150g = 22,
        Meal = 23,
        Package = 24,
        [Display(Name = "NLEA Serving")]
        NLEAServing = 27,
        Head = 28,
        Leaf = 29,
        Slice = 30,
        Cube = 31,
        [Display(Name = "6 OZ container")]
        _6ozCcontainer = 33,
        [Display(Name = "Sheet (50gm)")]
        Sheet50g=34,
        Box = 35,
        Gram=36

    }
}