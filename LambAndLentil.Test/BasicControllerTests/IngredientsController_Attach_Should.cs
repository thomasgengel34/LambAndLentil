using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using System.Linq;
using LambAndLentil.UI;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class IngredientsController_Attach_Should : IngredientsController_Test_Should
    {
        [TestMethod] 
        public void SuccessfullyAttachChild()
        {
            
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            Repo.Save(child);
 
            Controller.Attach(Repo,Ingredient.ID, child );
            ReturnedIngredient = Repo.GetById(Ingredient.ID); 
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedIngredient.Ingredients.Last().Name);
        }

     

        [TestMethod] 
        public void DetachASetOfIngredientChildren()
        { 
            BaseDetachASetOfIngredientChildren();
        }

       

        [TestMethod]
        public void DetachTheLastIngredientChild() => BaseDetachTheLastIngredientChild(Repo, Controller, (Ingredient)Ingredient);

        [TestMethod] 
        public void DetachAllIngredientChildren() => BaseDetachAllIngredientChildren(Repo, Controller );


        

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() =>
            BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller);

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild(Ingredient, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Ingredient, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller,Ingredient.ID); 
    }
}
