﻿using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Menu;
using TChild = LambAndLentil.Domain.Entities.Recipe;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfRecipesAndReturn1 : BaseControllerShouldDetachXAndReturn<TParent, TChild>
    {


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients() => BaseDetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients() =>
            BaseDetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients();


        [TestMethod]
        public void DetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients() =>
            BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients() =>
            BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients();


        [TestMethod]
        public void DetailWithWarningWhenIDisNotForAFoundParentWhenDetachASetOfIngredients() =>
            BaseDetailWithWarningWhenIDisNotForAFoundParentWhenDetachASetOfIngredients();



        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotFoundWhenDetachASetOfIngredients() =>
            BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients();


    }
}
