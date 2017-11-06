using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{ 
      public class BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        public static IRepository<T> Repo { get; set; } 
        public static ListEntity<T> ListEntity;
        public static T item;
        internal static string ClassName {get; private set;}

        public BaseControllerTest()
        { 
            Repo = new TestRepository<T>();
            ListEntity = new ListEntity<T>
            {
                ListT = new List<T>()
            };
            item = new T
            {
                ID = 1,
                Name="Name from BasicController_Test",
                Description = "BasicController_Test",
                CreationDate= new DateTime(2001,2,2)  ,
                 
            };
            Repo.Save(item);
            ListEntity.ListT = SetUpRepository();
            ClassName = typeof(T).ToString().Split('.').Last();  
        }

      public List<T> SetUpRepository()
        {
            string t = typeof(T).ToString();
            List<T> list = new List<T> {
                new T {ID = int.MaxValue, Name =t+ " ControllerTest1" ,
                    Description="test TsController.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-1, Name = t+ " ControllerTest2",
                    Description="test TsController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Mordecai", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new T {ID = int.MaxValue-2, Name = t+ " ControllerTest3",
                    Description="test TsController.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Milton", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new T {ID = int.MaxValue-3, Name = t+ " ControllerTest4",
                    Description="test TsController.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Michaelangelo", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-4, Name = t+ " ControllerTest5",
                    Description="test TsController.Setup",  AddedByUser="Buck Doe",  ModifiedByUser="Maurice", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

            foreach (T item in list)
            {
                Repo.Add(item);
            }
            return list;
        }

         

        protected void  BaseShouldAddIngredientToIngredientsList(IRepository<T> Repo,IEntity Entity, IEntity ReturnedEntity, BaseController<T> controller, UIControllerType uIControllerType)
        {
            // Arrange
            string addedIngredient = "added ingredient";
            string originalIngredientList = Entity.IngredientsList; 

            // Act
            controller.BaseAddIngredientToIngredientsList(Repo, uIControllerType, Entity.ID, addedIngredient);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            string listWithAddedIngredient = String.Concat(originalIngredientList, ", ", addedIngredient);
            Assert.AreEqual(listWithAddedIngredient, ReturnedEntity.IngredientsList);
        }

        protected void BaseCanSendPaginationViewModel(IRepository<T> Repo, IGenericController<T> Controller, UIControllerType uIControllerType)
        {

            // Arrange
            int count = Repo.Count();

            // Act 
            ListEntity<T> resultT1 = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            ListEntity<T> resultT2 = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;
            int repoCount =Repo.Count();
            int resultT1Count = resultT1.ListT.Count(); 
            int resultT2Count = resultT2.ListT.Count();
            PagingInfo pageInfoT = resultT1.PagingInfo;

            // Assert  
            Assert.AreEqual(1, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(count, resultT1.ListT.Count());
            Assert.AreEqual(0, resultT2.ListT.Count());
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

        protected void BasePagingInfoIsCorrect(IRepository<ShoppingList> Repo, IGenericController<ShoppingList> Controller, UIControllerType uIControllerType)
        {
            // Arrange 
            int repoCount = Repo.Count();

            // Action
            int totalItems = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;

            // Assert
            Assert.AreEqual(repoCount, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        protected void BaseCanSendPaginationViewModel_TotalItemsCorrect(IRepository<T> Repo, IGenericController<T> Controller, UIControllerType uIControllerType)
        {
            // Arrange 
            int count = Repo.Count();

            // Act 
            ListEntity<T> result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            PagingInfo pageInfoT = result.PagingInfo; 

            // Assert 
            Assert.AreEqual(count, result.ListT.Count());
        }

        private void BaseShouldAddRecipeToList(IRepository<Ingredient> repo, Ingredient entity, Ingredient returnedEntity, IngredientsController controller, UIControllerType ingredients)
        {
            throw new NotImplementedException();
        }



        protected void BaseFirstPageIsCorrect(IRepository<T> Repo, IGenericController<T> Controller, UIControllerType uIControllerType)
        {
            // Arrange
            int repoCount = Repo.Count();
        ListEntity<T> ilListEntity = new ListEntity<T>();
        Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
        int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

        // Assert
        Assert.IsNotNull(view1);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual(typeof(T).ToString()+" ControllerTest1", ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual(typeof(T).ToString() + " ControllerTest2", ((ListEntity<T>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual(typeof(T).ToString() + " ControllerTest3", ((ListEntity<T>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }


        protected void BasePagingInfoIsCorrect(IRepository<T> Repo, IGenericController<T> Controller, UIControllerType uIControllerType)
        {
            // Arrange 
            int repoCount = Repo.Count();

            // Action
            int totalItems = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(repoCount, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        protected void BaseSuccessfullyDetachIngredientChild(IRepository<T> ParentRepo, IGenericController<T> AttachController, IGenericController<T> DetachController, UIControllerType uIControllerType, int orderNumber=0) 
        {
            // Arrange
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachhIngredientChild" };
            ParentRepo.Save(parent);
            Ingredient child = new Ingredient() { ID = 3500, Name = "SuccessfullyAttachAndDetachhIngredientChild" };
            IRepository<Ingredient> childRepo = new TestRepository<Ingredient>();
            childRepo.Save(child);
            AttachController.AttachIngredient(parent.ID, child,0);

            // Act
            
            DetachController.DetachIngredient(parent.ID, child, orderNumber);
            T ReturnedItem = ParentRepo.GetById(parent.ID);
            // Assert 
            Assert.AreEqual(0, parent.Ingredients.Count());
            Assert.AreEqual(0, ReturnedItem.Ingredients.Count());
        }

        protected void BaseSuccessfullyDetachFirstRecipeChild(IRepository<T> ParentRepo, IGenericController<T> AttachController, IGenericController<T> DetachController, UIControllerType uIControllerType, int orderNumber = 0)
        {
            // Arrange
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachFirstRecipeChild" };
            ParentRepo.Save(parent);
            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachhRecipeChild" };
            IRepository<Recipe> childRepo = new TestRepository<Recipe>();
            childRepo.Save(child);
            AttachController.AttachRecipe(parent.ID, child, 0);

            // Act

            DetachController.DetachIngredient(parent.ID, child, orderNumber);
            T ReturnedItem = ParentRepo.GetById(parent.ID);
            // Assert 
            Assert.AreEqual(0, parent.Ingredients.Count());
            Assert.AreEqual(0, ReturnedItem.Ingredients.Count());
        }
        protected void BaseDeleteAFoundEntity(IGenericController<T> Controller)
        {
            // Arrange
            int repoCount = Repo.Count();

            // Act  
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string viewName = ((ViewResult)adr.InnerResult).ViewName;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(repoCount, Repo.Count());  // shows this does not actually delete anything.  That is done in delete-confirm
        }

        protected void BaseCanPaginateArrayLengthIsCorrect(IRepository<T> ParentRepo, IGenericController<T> Controller)
        {
            // Arrange
            int repoCount = Repo.Count();

            // Act
            var result = (ListEntity<T>)(Controller.Index(1)).Model;

            // Assert 
            Assert.AreEqual(repoCount, result.ListT.Count());
        }

        protected void BaseReturnDetailsWhenIDIsFound(IGenericController<T> Controller)
        {
            // Arrange

            // Act
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string viewName = ((ViewResult)adr.InnerResult).ViewName;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }

        protected void BaseReturnDeleteWithActionMethodDeleteWithEmptyResult(IGenericController<T> Controller)
        {
            // Details view with success  "Here it is!"

            // Arrange

            // Act
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;


            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message); // no message
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.IsInstanceOfType(vr.Model, typeof(T));

        }

        protected void BaseUpdateTheModificationDateBetweenPostedEdits(IRepository<T> Repo, IGenericController<T> Controller, T entity)
        {
            // Arrange  
            DateTime CreationDate = entity.CreationDate;
            DateTime modDate = entity.ModifiedDate;

            // Act
            Controller.PostEdit(entity);
            IEntity returnedEntity = Repo.GetById(entity.ID);
            DateTime newCreationDate = entity.CreationDate;
            DateTime newModDate = entity.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, newCreationDate);
            Assert.AreNotEqual(modDate, newModDate); 
        }

        [TestCleanup]
       public void TestCleanup()
        {
            ClassCleanup();
        }

        [ClassCleanup()]
       public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\"+ClassName+@"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
