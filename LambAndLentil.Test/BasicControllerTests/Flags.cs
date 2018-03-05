using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class Flags<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        { 
        }
           
 
        private static void FlagAnIngredientFlaggedInAPerson() => Assert.Fail();
       
        private static void FlagAnIngredientFlaggedInTwoPersons() => Assert.Fail();
        
        private static void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()=> Assert.Fail(); 

        private static void FlagAnMenuFlaggedInAPerson() => Assert.Fail(); 

        private static void FlagAnMenuFlaggedInTwoPersons() => Assert.Fail(); 

        private static void FlagAnRecipeFlaggedInAPerson() => Assert.Fail();

        private static void FlagAnRecipeFlaggedInTwoPersons() => Assert.Fail(); 
    }
}
