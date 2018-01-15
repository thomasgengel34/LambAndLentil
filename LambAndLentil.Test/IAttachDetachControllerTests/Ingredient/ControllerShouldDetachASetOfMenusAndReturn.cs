﻿using System;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfMenusAndReturn : BaseControllerShouldDetachXAndReturn<IngredientType, MenuType>
    {
        // ingredient cannot attach a menu

        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild();



        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParentWhenDetaching();


        [TestMethod]
        public void IndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild() =>
            BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild();

        [TestMethod]
        public void EditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenAttaching() =>
                  BaseEditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenAttaching();

        [TestMethod]
        public void EditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenDetaching() =>
                BaseEditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenDetaching();

    }
}
