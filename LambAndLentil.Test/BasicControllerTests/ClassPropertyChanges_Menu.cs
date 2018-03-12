using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{ 
    }
internal class ClassPropertyChanges_Menu<T> : BaseControllerTest<T>
     where T : BaseEntity, IEntity, new()
{

    private static T returnedItem { get; set; }

    internal static void TestRunner()
    {
        if (typeof(T) == typeof(Menu))
        {
            SaveEditedMenuWithNameAndDayOfWeekChange();
        }
    }


    private static void SaveEditedMenuWithNameAndDayOfWeekChange()
    { 
        Menu menu = new Menu() { ID = 20020, DayOfWeek = DayOfWeek.Sunday };
        repo.Save(menu as T);
        menu.DayOfWeek = DayOfWeek.Friday;
        ActionResult ar2 = controller.PostEdit(menu as T);
        Menu returnedMenu = repo.GetById(menu.ID) as Menu;
        Assert.AreEqual(DayOfWeek.Friday, returnedMenu.DayOfWeek);
    } 
}
