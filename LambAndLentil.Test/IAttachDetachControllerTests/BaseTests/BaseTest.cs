using System;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests

{
    [TestClass]
    public class BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        internal static IGenericController<TParent> Controller { get; set; }
        internal static IEntity Parent { get; set; }

       

        internal static IEntity Child { get; set; }
        internal static IRepository<TParent> ParentRepo = new TestRepository<TParent>();
        internal static IRepository<TChild> ChildRepo = new TestRepository<TChild>();
        internal static string ParentClassName { get; private set; }
        internal static PropertyInfo ChildProperty { get; private set; }

        public BaseTest()
        {
            ParentClassName = typeof(TParent).ToString().Split('.').Last();
            Controller = ControllerFactory();
            ChildProperty = ChildPropertyFactory();
            IEntity Parent = new TParent()
            {
                ID = 1,
                Name = "Name from BaseTest",
                Description = "BasicTest",
                CreationDate = new DateTime(2011, 2, 2),

            };
            ParentRepo.Save((TParent)Parent);
            IEntity Child = new TChild()
            {
                ID = 2,
                Name = "Child Name from BaseTest",
                Description = "Child BasicTest",
                CreationDate = new DateTime(2013, 2, 3),

            };
            ChildRepo.Save((TChild)Child);

        }
         
        private IGenericController<TParent> ControllerFactory()
        {
            switch (ParentClassName)
            {
                case "Ingredient":
                    Controller = new IngredientsController(new TestRepository<Domain.Entities.Ingredient>()) as IGenericController<TParent>;
                    return Controller;
                case "Recipe":
                    Controller = new RecipesController(new TestRepository<Domain.Entities.Recipe>()) as IGenericController<TParent>;
                    return Controller;
                case "Menu":
                    Controller = new MenusController(new TestRepository<Domain.Entities.Menu>()) as IGenericController<TParent>;
                    return Controller;
                case "Plan":
                    Controller = new PlansController(new TestRepository<Domain.Entities.Plan>()) as IGenericController<TParent>;
                    return Controller;
                case "ShoppingList":
                    Controller = new ShoppingListsController(new TestRepository<Domain.Entities.ShoppingList>()) as IGenericController<TParent>;
                    return Controller;
                case "Person":
                    Controller = new PersonsController(new TestRepository<Domain.Entities.Person>()) as IGenericController<TParent>;
                    return Controller;
                default:
                    throw new NotImplementedException();
            }
        }

        private PropertyInfo ChildPropertyFactory()
        {
            string ChildClassName = typeof(TChild).ToString().Split('.').Last();
            var propertyInfos = typeof(TParent).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            PropertyInfo property = (from p in propertyInfos where p.Name == ChildClassName select p).FirstOrDefault();

            return property;
        }

        protected static void BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied()
        {
            // "Here it is!"
            // Arrange


            // Act
            ActionResult ar = Controller.Details(2);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            //   Assert.IsInstanceOfType(view.Model, typeof(Domain.Entities.Ingredient));
              Assert.IsInstanceOfType(view.Model, typeof(TChild));
            Assert.AreEqual("Here it is!", adr.Message);
        }

        protected static void BaseReturnsIndexWithWarningWithNullParent()
        {
            // Arrange 
            

            // Act 
            ActionResult ar = Controller.AttachIngredient(null, new Domain.Entities.Ingredient(), 0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected   void BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient()
        {  // Todo: add logging
            // Act 
            ActionResult ar = Controller.AttachIngredient(null, new Domain.Entities.Ingredient(), 0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid() { }

  
        protected void BaseDetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeWhenDetaching()
        { 
          // Arrange
            IIngredient ingredient = new IngredientType();
            IRepository<IngredientType> IngredientRepo = new TestRepository<IngredientType>();
            // Act 
               ActionResult ar =Controller.DetachIngredient(1,(Domain.Entities.Ingredient)ingredient,-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual("Order Number Was Negative! Nothing was detached", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        } 

    }
}
