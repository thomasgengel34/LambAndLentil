using System;
using System.Collections.Generic;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    public class ClassPropertyChanges<T>:BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        public T  Entity{ get; set; }
        public T ReturnedEntity{ get; set; }

        public  ClassPropertyChanges()
        {
            Entity = new T { ID = 1000, Name = "Original Name",Ingredients=new List<Ingredient>() };
            Repo.Save(Entity); 
        }

      
        public void ShouldEditName()
        { 
            Entity.Name = "Name is changed";
             Controller.PostEdit(Entity);
            ReturnedEntity= Repo.GetById(1000);
         
            Assert.AreEqual("Name is changed", ReturnedEntity.Name);
        }

      
        public void Copy()
        {  
            
            Entity.ID = 42;
            Controller.PostEdit(Entity);
             ReturnedEntity = Repo.GetById(42);
              Entity = Repo.GetById(1000);
             
            Assert.AreEqual(42, ReturnedEntity.ID);
            Assert.IsNotNull( Entity);
        }

        [TestMethod]
        public void ShouldEditDescription()
        {
           
            string changedDescription = "Description has changed";
 
            Entity.Description = changedDescription;
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);
             
            Assert.AreNotEqual(changedDescription, ReturnedEntity.AddedByUser);
        }

        [TestMethod]
        public void DoesNotEditCreationDate()
        { 
            DateTime dateTime = new DateTime(1776, 7, 4);
            Controller.PostEdit(Entity);
             
            Entity.CreationDate = dateTime;
             Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            
            Assert.AreNotEqual(dateTime.Year, ReturnedEntity.CreationDate.Year);
        }

        [TestMethod]
        public void DoesNotEditAddedByUser()
        { 
            string user = "Abraham Lincoln";
             
            Entity.AddedByUser = user;

             Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);
             
            Assert.AreNotEqual(user, ReturnedEntity.AddedByUser);
        }

        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        { 
            string user = "Abraham Lincoln";
            
            Entity.ModifiedByUser = user ;
             Controller.PostEdit(Entity);
            ReturnedEntity= Repo.GetById(Entity.ID);
 
            Assert.AreNotEqual(user, ReturnedEntity.ModifiedByUser);
        }

        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            
            DateTime dateTime = new DateTime(1776, 7, 4);
            Entity.ModifiedDate = dateTime;

           
            Controller.PostEdit(Entity);
            ReturnedEntity= Repo.GetById(Entity.ID);

             
            Assert.AreNotEqual(dateTime.Year, ReturnedEntity.ModifiedDate.Year);
        }
         
        [TestMethod]
        public void ShouldEditUserGeneratedIngredientsList()
        { 
            Entity.IngredientsList = "Edited";
             
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);
             
            Assert.AreEqual("Edited", ReturnedEntity.IngredientsList);
        }

      
        public void ShouldNotEditWebAPIGeneratedIngredientsList() =>
             
            Assert.Fail();

       
        public void ShouldAddIngredientToIngredients()
        { 
            int initialCount = Entity.Ingredients.Count;
             
            Entity.Ingredients.Add(new Ingredient() { ID = 134, Name = "ShouldAddIngredientToIngredients" });
             Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);
             
            Assert.AreEqual(initialCount + 1, Entity.Ingredients.Count);
            Assert.AreEqual("ShouldAddIngredientToIngredients", Entity.Ingredients[initialCount].Name);
        }

        
        public void ShouldAddIngredientToIngredientsList() => BaseShouldAddIngredientToIngredientsList();
    }
}
