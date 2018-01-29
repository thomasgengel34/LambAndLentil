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
      where T : BaseEntity, IEntity,  new()
    {
        internal static IGenericController<T> Controller { get; set; }   // TODO: convert these to private fields if possible
        internal static IRepository<T> Repo { get; set; }
        internal static ListEntity<T> ListEntity;
        internal static T item;
        internal static string ClassName { get; private set; }

        public BaseControllerTest()
        {
            ClassCleanup();
            Repo = new TestRepository<T>();
            ListEntity = new ListEntity<T>
            {
                ListT = new List<T>()
            };
            item = new T
            {
                ID = 1,
                Name = "Name from BasicController_Test",
                Description = "BasicController_Test",
                CreationDate = new DateTime(2001, 2, 2),
                Ingredients = new List<IEntity>()
            };
            Repo.Save(item);
            ListEntity.ListT = SetUpRepository();
            GetClassName();

            Controller = BaseControllerTestFactory(typeof(T));
            Controller.PageSize = 3;
        }

        private static void GetClassName()
        {
            ClassName = typeof(T).ToString().Split('.').Last(); 
        }
        private IGenericController<T> BaseControllerTestFactory( Type T)
        {
            if (typeof(T) == typeof(Ingredient))
            {
                return (IGenericController<T>)(new IngredientsController(new TestRepository<Ingredient>()));
            }
            else if (typeof(T) == typeof(Recipe))
            {
                return (IGenericController<T>)(new RecipesController(new TestRepository<Recipe>()));
            }
            else if (typeof(T) == typeof(Menu))
            {
                return (IGenericController<T>)(new MenusController(new TestRepository<Menu>()));
            }
            else if (typeof(T) == typeof(Plan))
            {
                return (IGenericController<T>)(new PlansController(new TestRepository<Plan>()));
            }
            else if (typeof(T) == typeof(Person))
            {
                return (IGenericController<T>)(new PersonsController(new TestRepository<Person>()));
            }
            else if (typeof(T) == typeof(ShoppingList))
            {
                return (IGenericController<T>)(new ShoppingListsController(new TestRepository<ShoppingList>()));
            }
            else throw new Exception();
        }

        [TestMethod]
        public void IsPublic()
        {
            Type type =  Controller.GetType();
            bool isPublic = type.IsPublic;
             
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {
            Controller.PageSize = 4;

            var type = typeof(Controller);
            var DoesDisposeExist = type.GetMethod("Dispose");

            Assert.AreEqual(4, Controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
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
                Repo.Save(item);
            }
            return list;
        }

        public void BaseShouldCreate()
        {
            ActionResult ar = Controller.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            T t = (T)vr.Model;
            string modelName = t.Name;

            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
            // TODO:  Menu as child exclusive test
            //Assert.AreEqual(DayOfWeek.Sunday, Menu.DayOfWeek);
        }
         



        protected void BaseShouldAddIngredientToIngredientsList()
        {
            IRepository<Ingredient> repo = new TestRepository<Ingredient>();
            IGenericController<Ingredient> controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient() { ID = 3500, IngredientsList = "start" };
            string addedIngredient = "added ingredient";
             repo.Save(ingredient);
             controller.AddIngredientToIngredientsList(ingredient.ID, addedIngredient);
            Ingredient returnedIngredient =  repo.GetById(ingredient.ID);
             
            string listWithAddedIngredient = String.Concat(ingredient.IngredientsList, ", ", addedIngredient);
            Assert.AreEqual(listWithAddedIngredient, returnedIngredient.IngredientsList);
        }

        protected void BaseCanSendPaginationViewModel(IRepository<T> Repo, IGenericController<T> Controller)
        { 
            int count = Repo.Count();
             
            ListEntity<T> resultT1 = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            ListEntity<T> resultT2 = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;
            int repoCount = Repo.Count();
            int resultT1Count = resultT1.ListT.Count();
            int resultT2Count = resultT2.ListT.Count();
            PagingInfo pageInfoT = resultT1.PagingInfo;
             
            Assert.AreEqual(1, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(count, resultT1.ListT.Count());
            Assert.AreEqual(0, resultT2.ListT.Count());
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

        protected void BasePagingInfoIsCorrect(IRepository<ShoppingList> Repo, IGenericController<ShoppingList> Controller )
        { 
            int repoCount = Repo.Count();
             
            int totalItems = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;
             
            Assert.AreEqual(repoCount, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        protected void BaseCanSendPaginationViewModel_TotalItemsCorrect(IRepository<T> Repo, IGenericController<T> Controller)
        { 
            int count = Repo.Count();
             
            ListEntity<T> result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            PagingInfo pageInfoT = result.PagingInfo;
             
            Assert.AreEqual(count, result.ListT.Count());
        }

        private void BaseShouldAddRecipeToList(IRepository<Ingredient> repo, Ingredient entity, Ingredient returnedEntity, IngredientsController controller) => throw new NotImplementedException();



        protected void BaseFirstPageIsCorrect(IRepository<T> Repo, IGenericController<T> Controller)
        { 
            int repoCount = Repo.Count();
            ListEntity<T> ilListEntity = new ListEntity<T>();
            Controller.PageSize = 8;
             
            ViewResult view1 = (ViewResult)Controller.Index(1);
            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();
             
            Assert.IsNotNull(view1);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual(typeof(T).ToString() + " ControllerTest1", ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual(typeof(T).ToString() + " ControllerTest2", ((ListEntity<T>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual(typeof(T).ToString() + " ControllerTest3", ((ListEntity<T>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }


        protected void BasePagingInfoIsCorrect(IRepository<T> Repo, IGenericController<T> Controller)
        { 
            int repoCount = Repo.Count();
             
            int totalItems = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<T>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;
             
            Assert.AreEqual(repoCount, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        protected void BaseSuccessfullyDetachChild<TChild>(IRepository<T> ParentRepo, IGenericController<T> attachController, IGenericController<T> detachController)
            where TChild: BaseEntity,IEntity, new()
        { 
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachChild" };
            ParentRepo.Save(parent);
            TChild child = new TChild();

            attachController.Attach( parent, child );
            IEntity parentM = parent;
             
            detachController.Detach( parent, child);
            IEntity ReturnedItem = ParentRepo.GetById(parent.ID);
             
            Assert.AreEqual(0, parentM.Menus.Count());
            Assert.AreEqual(0, ReturnedItem.Menus.Count());
        }

      

        protected void BaseSuccessfullyDetachRecipeChild(IGenericController<T> AttachController, IGenericController<T> DetachController )
        { 
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachRecipeChild" };
             Repo.Save(parent);
            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachRecipeChild" };
            IRepository<Recipe> childRepo = new TestRepository<Recipe>();
            childRepo.Save(child);
            AttachController.Attach(parent, child ); 

            DetachController.Detach(parent, child);
            IEntity ReturnedItem =  Repo.GetById(parent.ID);
            IEntity parentR = parent;
          
            Assert.AreEqual(0, parentR.Recipes.Count());
            Assert.AreEqual(0, ReturnedItem.Recipes.Count());
        }

        protected void BaseSuccessfullyDetachFirstRecipeChild(IGenericController<T> AttachController, IGenericController<T> DetachController, int orderNumber = 0)
        {
             
            T parent = new T() { ID = 4000, Name = "ParentForSuccessfullyAttachAndDetachFirstRecipeChild" };
            Repo.Save(parent);
            Recipe child = new Recipe() { ID = 3500, Name = "SuccessfullyAttachAndDetachhRecipeChild" };
            IRepository<Recipe> childRepo = new TestRepository<Recipe>();
            childRepo.Save(child);
            AttachController.Attach(parent, child );
            IEntity parentR = (IEntity)parent;

        

            DetachController.Detach(parent, child);
            IEntity ReturnedItem = Repo.GetById(parent.ID);

          
            Assert.AreEqual(0, parentR.Recipes.Count());
            Assert.AreEqual(0, ReturnedItem.Recipes.Count());
        }
        protected void BaseDeleteAFoundEntity(IGenericController<T> Controller)
        { 
            int repoCount = Repo.Count();
             
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string viewName = ((ViewResult)adr.InnerResult).ViewName;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(repoCount, Repo.Count());  // shows this does not actually delete anything.  That is done in delete-confirm
        }

        protected void BaseCanPaginateArrayLengthIsCorrect(IGenericController<T> Controller)
        { 
            int repoCount = Repo.Count();
             
            var result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
             
            Assert.AreEqual(repoCount, result.ListT.Count());
        }

        internal void BaseReturnDetailsWhenIDIsFound()
        { 
            ActionResult ar = Controller.Details(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;
            int returnedID = ((T)(vr.Model)).ID;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(int.MaxValue - 1, returnedID);
        }

        protected void BaseReturnDeleteWithActionMethodDeleteWithEmptyResult(IGenericController<T> Controller)
        { 
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);  
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.IsInstanceOfType(vr.Model, typeof(T));

        }

        protected void BaseUpdateTheModificationDateBetweenPostedEdits(T entity)
        { 
            DateTime CreationDate = entity.CreationDate;
            DateTime modDate = entity.ModifiedDate;
             
            Controller.PostEdit(entity);
            IEntity returnedEntity = Repo.GetById(entity.ID);
            DateTime newCreationDate = entity.CreationDate;
            DateTime newModDate = entity.ModifiedDate;
             
            Assert.AreEqual(CreationDate, newCreationDate);
            Assert.AreNotEqual(modDate, newModDate);
        }


        

        public void BaseDetachAllIngredientChildren()
        {    
           T parent = Generate_T_WithFiveIngredientChildren();
            Repo.Save(parent);
            List<IEntity> selection   = parent.Ingredients; 
           
            Controller.DetachASetOf(parent,selection);
           var returnedEntity = Repo.GetById(parent.ID);
            var trueOrFalse = (returnedEntity.Ingredients == null) || ( returnedEntity.Ingredients.Count()==0);
            Assert.IsTrue(trueOrFalse);
        }


        private T Generate_T_WithFiveIngredientChildren()
        {
           IRepository<Ingredient> repo = new TestRepository<Ingredient>();
           T  item= new T{ ID = 31425926 };

            Ingredient ingredient0 = new Ingredient() { ID = 1000 };
            Ingredient ingredient1 = new Ingredient() { ID = 1001 };
            Ingredient ingredient2 = new Ingredient() { ID = 1002 };
            Ingredient ingredient3 = new Ingredient() { ID = 1003 };
            Ingredient ingredient4 = new Ingredient() { ID = 1004 };

           item.Ingredients = new List<IEntity>();
           
            List<Ingredient> list= new List<Ingredient>() { ingredient0, ingredient1, ingredient2, ingredient3, ingredient4 };
            item.Ingredients.AddRange(list);  
            return  item as T;

        }

        public void BaseDetachAllMenuChildren(IRepository<T> Repo, IGenericController<T> controller, IEntity entity)
        {  
            entity.Menus.Add(new Menu() { ID = 1000 });
            entity.Menus.Add(new Menu() { ID = 1001 });
            entity.Menus.Add(new Menu() { ID = 1002 });
            entity.Menus.Add(new Menu() { ID = 1003 });
            entity.Menus.Add(new Menu() { ID = 1004 });
            Repo.Save((T)entity);

 
            controller.DetachAll(entity,typeof(Menu));
            IEntity returnedEntity = Repo.GetById(entity.ID);
           
            Assert.AreEqual(0, returnedEntity.Menus.Count());
        }


        public void BaseDetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet<TParent>(IGenericController<T> Controller)
           where TParent : BaseEntity, IEntity, new()
        {
            IEntity entity = (IEntity)(new TParent() { ID = 7654321 });
            entity.Ingredients.Clear();
            entity.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            entity.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            entity.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            entity.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            int initialIngredientCount = entity.Ingredients.Count();
            Repo.Save(entity as T);
          
            List<IEntity> selected = new List<IEntity>() { new Ingredient { ID = 6000 } };
            Controller.DetachASetOf(entity , selected);
            IEntity returnedEntity = (IEntity)Repo.GetById(entity.ID);
             
            Assert.AreEqual(initialIngredientCount, returnedEntity.Ingredients.Count());
        }

        protected void BaseReturnsIndexWithWarningWithUnknownParentID(IGenericController<T> controller)
        {  
            Repo.Remove(item);
             
            ActionResult ar = controller.Attach(item, new Ingredient() );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;
             
            Assert.AreEqual(ClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        }

        protected void BaseReturnsIndexWithWarningWithNullParent(IGenericController<T> controller)
        {  
            ActionResult ar = controller.Attach(null, new Ingredient() );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;
             
            Assert.AreEqual(ClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        public void BaseDetachTheLastIngredientChild(IGenericController<T> controller, IEntity notusing)
        {
            IEntity Entity = new T();
            Entity.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Entity.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Entity.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Entity.Ingredients.Add(new Ingredient { ID = 54008, Name = "Chopped Green Pepper" });
            Repo.Save((T)Entity);
            int initialIngredientCount = Entity.Ingredients.Count();
             
            Ingredient LastIngredient = (Ingredient)Entity.Ingredients.LastOrDefault();
            Entity.Ingredients.RemoveAt(initialIngredientCount - 1);
            bool IsLastIngredientStillThere = Entity.Ingredients.Contains(LastIngredient);
             
            Assert.AreEqual(initialIngredientCount - 1, Entity.Ingredients.Count());
            Assert.IsFalse(IsLastIngredientStillThere);
        }

        protected void BaseReturnsDetailWithWarningIfAttachingNullChild(IEntity entity,IGenericController<T> controller)
        {
            var ar = controller.Attach(entity , (Ingredient)null );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(ClassName + " was not found", adr.Message);
        }


        protected void BaseReturnsDetailWithWarningWithUnknownChildID(IEntity entity, IGenericController<T> controller)
        {
            var ar = controller.Attach(entity, (Ingredient)null );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(ClassName + " was not found", adr.Message);
        }


        protected void BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(IGenericController<T> controller, int iD)
        {
            IEntity entity = Repo.GetById(iD);

            Ingredient ingredient = new Ingredient() { ID = 8000, Name = "BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild" };

            entity.Ingredients.Add(ingredient);
            Repo.Update((T)entity, entity.ID);

            var ar = controller.Detach(entity , ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);

        }

        protected void BaseSuccessfullyAttachRecipeChild(IEntity Entity, IGenericController<T> controller)
        {
            Recipe recipe = new Recipe() { ID = 91, Name = "SuccessfullyAttachRecipeChild" };

            ActionResult ar = controller.Attach(Entity , recipe );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            IEntity returnedEntity =  Repo.GetById(Entity.ID);
            Recipe returnedRecipe = (Recipe)returnedEntity.Recipes.First();


            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Recipe was Successfully Attached!", adr.Message);
            Assert.AreEqual(Entity.ID, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());

            Assert.IsNotNull(returnedRecipe);
            Assert.AreEqual("SuccessfullyAttachRecipeChild", returnedRecipe.Name);
        }
       
      


        protected void BaseSuccessfullyAttachPlanChild(IEntity Entity, IGenericController<T> controller)
        {
            Plan plan = new Plan() { ID = 91, Name = "SuccessfullyAttachPlanChild" };

            ActionResult ar = controller.Attach(Entity , plan );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            IEntity returnedEntity = Repo.GetById(Entity.ID);
            Plan returnedPlan = (Plan)returnedEntity.Plans.First();


            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Plan was Successfully Attached!", adr.Message);
            Assert.AreEqual(Entity.ID, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());

            Assert.IsNotNull(returnedPlan);
            Assert.AreEqual("SuccessfullyAttachPlanChild", returnedPlan.Name);

        }

        protected void BaseSuccessfullyAttachChild(IEntity Entity, IGenericController<T> controller)
        {
            Menu menu = new Menu() { ID = 91, Name = "SuccessfullyAttachChild" };

            ActionResult ar = controller.Attach(Entity , menu );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            IEntity returnedEntity = Repo.GetById(Entity.ID);
            Menu returnedMenu = (Menu)returnedEntity.Menus.First();


            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Menu was Successfully Attached!", adr.Message);
            Assert.AreEqual(Entity.ID, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());

            Assert.IsNotNull(returnedMenu);
            Assert.AreEqual("SuccessfullyAttachChild", returnedMenu.Name);

        }

        protected void BaseDetachTheLastMenuChild(IRepository<T> repo, IGenericController<T> controller, IEntity Entity)
        {  
            Entity.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            Entity.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            Entity.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            Entity.Menus.Add(new Menu { ID = 54008, Name = "Chopped Green Pepper" });
            Repo.Save((T)Entity);
            int initialMenuCount = Entity.Menus.Count();
              
            Menu LastMenu = (Menu)Entity.Menus.LastOrDefault();
            Entity.Menus.RemoveAt(initialMenuCount - 1);
            bool IsLastMenuStillThere = Entity.Menus.Contains(LastMenu);
             
            Assert.AreEqual(initialMenuCount - 1, Entity.Menus.Count());
            Assert.IsFalse(IsLastMenuStillThere);
        }

        protected void BaseDetachTheLastRecipeChild(IRepository<T> repo, IGenericController<T> controller, IEntity Entity)
        {  
            Entity.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            Entity.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            Entity.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            Entity.Recipes.Add(new Recipe { ID = 54008, Name = "Chopped Green Pepper" });
            Repo.Save((T)Entity);
            int initialRecipeCount = Entity.Recipes.Count();
             
            Recipe LastRecipe = (Recipe)Entity.Recipes.LastOrDefault();
            Entity.Recipes.RemoveAt(initialRecipeCount - 1);
            bool IsLastRecipeStillThere = Entity.Recipes.Contains(LastRecipe);
             
            Assert.AreEqual(initialRecipeCount - 1, Entity.Recipes.Count());
            Assert.IsFalse(IsLastRecipeStillThere);
        }

        protected void BaseDetachTheLastShoppingListChild(IRepository<T> repo, IGenericController<T> controller, IEntity Entity)
        {  
            Entity.ShoppingLists.Add(new ShoppingList { ID = 4005, Name = "Butter" });
            Entity.ShoppingLists.Add(new ShoppingList { ID = 4006, Name = "Cayenne Pepper" });
            Entity.ShoppingLists.Add(new ShoppingList { ID = 4007, Name = "Cheese" });
            Entity.ShoppingLists.Add(new ShoppingList { ID = 54008, Name = "Chopped Green Pepper" });
            Repo.Save((T)Entity);
            int initialShoppingListCount = Entity.ShoppingLists.Count();
             
            ShoppingList LastShoppingList = (ShoppingList)Entity.ShoppingLists.LastOrDefault();
            Entity.ShoppingLists.RemoveAt(initialShoppingListCount - 1);
            bool IsLastShoppingListStillThere = Entity.ShoppingLists.Contains(LastShoppingList);
             
            Assert.AreEqual(initialShoppingListCount - 1, Entity.ShoppingLists.Count());
            Assert.IsFalse(IsLastShoppingListStillThere);
        }

        protected static void BaseDetachASetOfIngredientChildren()
        { 
            item.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            item.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            item.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            item.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((T)item);
            int initialIngredientCount = item.Ingredients.Count();

            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<IEntity> selected =item.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf(item, selected);
            IEntity returnedEntity = (IEntity)Repo.GetById(item.ID);

            Assert.AreEqual(initialIngredientCount - 2, returnedEntity.Ingredients.Count());
        }
         

        [TestCleanup]
        public void TestCleanup() => ClassCleanup();

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + ClassName + @"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

         
    }
}
