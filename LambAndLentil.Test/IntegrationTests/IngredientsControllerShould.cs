using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    // also using WebApi methods - something will be needed for additional ingredients, such as user entered
    [TestCategory("Integration")]
    [TestCategory("IngredientsController")]
    public class IngredientsControllerShould : IngredientsController_Test_Should
    {
        public IngredientsControllerShould()
        {
        }

        [TestMethod]
        [TestCategory("Create")]
        public void CreateAnIngredient()
        {
            // Arrange

            // Act
            ViewResult vr = (ViewResult)Controller.Create(UIViewType.Create);
            string modelName = ((Ingredient)vr.Model).Name;
            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        [TestCategory("Save")]
        public void SaveAValidIngredientAndReturnIndexView()
        {
            // Arrange
            Ingredient vm = new Ingredient
            {
                ID = 3000,
                Name = "SaveAValidIngredientAndReturnIndexView"
            };

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            // Assert 

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedIngredient()
        {
            // Arrange
            IGenericController<Ingredient> indexController = new IngredientsController(Repo);
            IGenericController<Ingredient> Controller2 = new IngredientsController(Repo);
            IGenericController<Ingredient> Controller3 = new IngredientsController(Repo);


            Ingredient = new Ingredient
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test IngredientsControllerShould.SaveEditedIngredient"
            };

            // Act 
            ActionResult ar1 =  Controller.PostEdit((Ingredient)Ingredient);


            // now edit it
            Ingredient.Name = "0000 test Edited";
            Ingredient.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit((Ingredient)Ingredient);
            ViewResult view2 = (ViewResult)Controller3.Index();
            List<Ingredient> ListEntity2 = (List<Ingredient>)((ListEntity<Ingredient>)view2.Model).ListT;
            ReturnedIngredient = (from m in ListEntity2
                                  where m.Name == "0000 test Edited"
                                  select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", ReturnedIngredient.Name);
            Assert.AreEqual(7777, ReturnedIngredient.ID);

        }

        //[TestMethod]
        //[TestCategory("DeleteConfirmed")]
        //public void ActuallyDeleteAnIngredientFromTheDatabase()
        //{
        //    // Arrange   
        //    Controller = new IngredientsController_Index_Test().SetUpIngredientsController(Repo);
        //    Ingredient item = Repo.GetById(int.MaxValue);
        //    int countInRepo = Repo.Count();
        //    //Act
        //    Controller.DeleteConfirmed(item.ID);
        //    int count = Repo.Count();

        //    //Assert
        //    Assert.AreEqual(countInRepo - 1, count);
        //}

        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Ingredient ingredient = new Ingredient(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Ingredient ingredient = new Ingredient();

            // Assert
            Assert.AreEqual(CreationDate.Date, ingredient.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Ingredient ingredient = new Ingredient(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Ingredient ingredient = new Ingredient(CreationDate)
            {
                ID = int.MaxValue - 200,
                Name = "test IngredientsControllerShould.SaveTheCreationDateBetweenPostedEdits"
            };


            IGenericController<Ingredient> ControllerEdit = new IngredientsController(Repo);
            IGenericController<Ingredient> ControllerView = new IngredientsController(Repo);
            IGenericController<Ingredient> ControllerDelete = new IngredientsController(Repo);

            // Act
            ControllerEdit.PostEdit(ingredient);
            ViewResult view = (ViewResult)ControllerView.Index();
            List<Ingredient> ListEntity = (List<Ingredient>)((ListEntity<Ingredient>)view.Model).ListT;
            IIngredient returnedListEntity = Repo.GetById(ingredient.ID);
            DateTime shouldBeSameDate = returnedListEntity.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);


        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            Ingredient.Name = "Test UpdateTheModificationDateBetweenPostedEdits";
            Ingredient.ID = 6000;
            Repo.Save((Ingredient)Ingredient);
            BaseUpdateTheModificationDateBetweenPostedEdits(Repo,  Controller,(Ingredient)Ingredient);
        }


        [TestMethod]
        public void NotCreateASecondElementOnEditingOneElement()
        {
            // Arrange
            int initialCount = Repo.Count();

            // Act 
            Ingredient.Name = "Changed";
             Controller.Edit(Ingredient.ID);

            // Assert
            Assert.AreEqual(initialCount, Repo.Count());
        }


        [TestMethod]
        public void NotCreateASecondElementOnPostEditingOneElement()
        {
            // Arrange
            int initialCount = Repo.Count();

            // Act 
            Ingredient.Name = "Changed";
             Controller.PostEdit((Ingredient)Ingredient);

            // Assert
            Assert.AreEqual(initialCount, Repo.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void ReturnIndexWithWarningForNonexistentIngredient()
        {
            // Arrange

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Edit(1000);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert  
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }


        [TestMethod]
        public void NotChangeIDInPostEditActionMethod()
        {
            //Arrange
            int originalID = Ingredient.ID;
            Ingredient.ID = 7000;

            //Act
             Controller.PostEdit((Ingredient)Ingredient);
            ReturnedIngredient = Repo.GetById(7000);
            Ingredient OriginalIngredient = Repo.GetById(originalID);

            // Assert
            Assert.AreEqual(originalID, OriginalIngredient.ID);
            Assert.AreEqual(7000, ReturnedIngredient.ID);
        }


        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveNameBoundInPostEditActionMethod()
        {
            //Arrange 
            Ingredient.Name ="Changed";

            //Act
             Controller.PostEdit((Ingredient)Ingredient);
            ReturnedIngredient = Repo.GetById(Ingredient.ID); 

            // Assert 
            Assert.AreEqual(Ingredient.Name, ReturnedIngredient.Name);
        }
         
         
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveDescriptionBoundInPostEditActionMethod()
        {
            //Arrange 
            Ingredient.Description = "Changed";

            //Act
             Controller.PostEdit((Ingredient)Ingredient);
            ReturnedIngredient = Repo.GetById(Ingredient.ID);

            // Assert 
            Assert.AreEqual(Ingredient.Description, ReturnedIngredient.Description);
        }
          
        
               
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveIngredientsBoundInPostEditActionMethod()
        {
            //Arrange 
            Ingredient.Ingredients = new List<Ingredient> {
                new Ingredient { Name = "Changed" },
                new Ingredient { Name = "Changed 2" },
                new Ingredient { Name = "Changed Up" }
            };

            //Act
             Controller.PostEdit((Ingredient)Ingredient);
            ReturnedIngredient = Repo.GetById(Ingredient.ID);

            // Assert 
            Assert.AreEqual("Changed", ReturnedIngredient.Ingredients.First().Name);
            Assert.AreEqual("Changed Up", ReturnedIngredient.Ingredients.Last().Name);
        }
         
         
        [TestMethod]
        public void HaveIngredientsListBoundInPostEditActionMethod()
        {
            //Arrange 
            Ingredient.IngredientsList =  "Changed"  ;

            //Act
             Controller.PostEdit((Ingredient)Ingredient);
            ReturnedIngredient = Repo.GetById(Ingredient.ID);

            // Assert 
            Assert.AreEqual("Changed", ReturnedIngredient.IngredientsList);
        } 
    }
}
