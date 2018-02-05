using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestCategory("RecipesController")]
    [TestClass]
    public class RecipesController_Attach_Should : RecipesController_Test_Should
    {

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() => BaseReturnsIndexWithWarningWithUnknownParentID();

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent();

        
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Recipe,  Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild();

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        { 
            Recipe recipe = new Recipe
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Recipe> mRepo = new TestRepository<Recipe>();
            mRepo.Save(recipe);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
             
            ActionResult ar = Controller.Attach(recipe, ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild() => 
            Assert.Fail();


        [TestMethod]
        public void SuccessfullyAttachChild()
        { 
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);
 
            Controller.Attach(Recipe, child );
            ReturnedRecipe = Repo.GetById(Recipe.ID);
           
          
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedRecipe.Ingredients.Last().Name);
        }
         

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        {  
            //Recipe.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            //Recipe.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            //Recipe.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            //Recipe.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            //Repo.Save(Recipe);
            //int initialIngredientCount = Recipe.Ingredients.Count();
 
            //var setToSelect = new HashSet<int> {  4006, 4008 };

            //List<Ingredient> selected = Recipe.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();

            //Controller.DetachASetOf(Recipe, selected);
            //Recipe returnedRecipe = Repo.GetById(Recipe.ID);
          
            //Assert.AreEqual(initialIngredientCount - 2, returnedRecipe.Ingredients.Count());
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet() => BaseDetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet<Recipe>(Controller);


        [TestMethod]
        public void DetachTheLastIngredientChild() => BaseDetachTheLastIngredientChild(Controller, Recipe);

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachAllIngredientChildren() => BaseDetachAllIngredientChildren(  );

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild();


    }
}
