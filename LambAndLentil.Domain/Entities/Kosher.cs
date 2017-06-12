using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    // see http://www.kashrut.com/agencies/ for a complete list.  This is partial per YAGNI
    public enum Kosher
    {
        [Display(Name = "Kof-K")]
        KofK,
        Not,
        OU,
        OK,
        [Display(Name = "Star-D")]
        StarD,
        [Display(Name = "Star-K")]
        StarK,
        Unknown,
        [Display(Name = "Vaad HaKashruth of Kansas City")]
        VaadHaKashruth,
        [Display(Name = "Vaad Hoeir ofSt. Louis")]
        VaadHoeir
    }
}
