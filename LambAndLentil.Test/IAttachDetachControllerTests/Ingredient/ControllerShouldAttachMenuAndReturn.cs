using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachMenuAndReturn: BaseControllerShouldAttachXAndReturn<IngredientType,MenuType>
    { 
        // ingredient cannot attach a menu 
       
        [TestMethod]
        public void  IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();

       
        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();

       
    }
}
