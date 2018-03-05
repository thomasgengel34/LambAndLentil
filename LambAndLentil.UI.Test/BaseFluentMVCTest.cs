using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.FluentMVCTesting;
using System.Web.Mvc;

namespace LambAndLentil.FluentMVC.Test
{
    internal class BaseFluentMVCTest<T>
        where T : BaseEntity, IEntity, new()
    {
        //    private static IGenericController<T> controller;
        //    private static IRepository<T> repository;
        //    private static T t;

        //   public BaseFluentMVCTest()
        //    {
        //        IRepository<T> repository = new TestRepository<T>();
        //        controller = GetController();
        //        t = new T() { ID = 7000 };
        //    }

        internal static IAttachDetachController<T> GetAttachDetachController()
        {
            if (typeof(T) == typeof(Ingredient))
            {
                return (IAttachDetachController<T>)(new IngredientsController(new TestRepository<Ingredient>()));
            }
            else if (typeof(T) == typeof(Recipe))
            {
                return (IAttachDetachController<T>)(new RecipesController(new TestRepository<Recipe>()));
            }
            else if (typeof(T) == typeof(Menu))
            {
                return (IAttachDetachController<T>)(new MenusController(new TestRepository<Menu>()));
            }
            else if (typeof(T) == typeof(Plan))
            {
                return (IAttachDetachController<T>)(new PlansController(new TestRepository<Plan>()));
            }
            else if (typeof(T) == typeof(Person))
            {
                return (IAttachDetachController<T>)(new PersonsController(new TestRepository<Person>()));
            }
            else if (typeof(T) == typeof(ShoppingList))
            {
                return (IAttachDetachController<T>)(new ShoppingListsController(new TestRepository<ShoppingList>()));
            }
            else throw new Exception();


        }

        internal static void BaseRenderIndexDefaultView()
        {
            ClassCleanup();

            // TODO: figure out how to refactor this
            if (typeof(T) == typeof(Ingredient))
            {
               IngredientsController controller = new IngredientsController(new TestRepository<Ingredient>());  
                controller.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Menu))
            {
                MenusController controller = new MenusController(new TestRepository<Menu>());
                controller.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Person))
            {
                PersonsController controller = new PersonsController(new TestRepository<Person>());
                controller.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Plan))
            {
                PlansController controller = new PlansController(new TestRepository<Plan>());
                controller.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Recipe))
            {
                RecipesController controller = new RecipesController(new TestRepository<Recipe>());
                controller.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingListsController controller = new ShoppingListsController(new TestRepository<ShoppingList>());
               controller.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
            }
 
        }


        internal static void BaseRenderDetailsDefaultView()
        {
            T t2 = new T() { ID = 7000 };
         

            if (typeof(T) == typeof(Ingredient))
            {
                IngredientsController controller = new IngredientsController(new TestRepository<Ingredient>());
                controller.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Menu))
            {
                MenusController controller = new MenusController(new TestRepository<Menu>());
                controller.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Person))
            {
                PersonsController controller = new PersonsController(new TestRepository<Person>());
                controller.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Plan))
            {
                PlansController controller = new PlansController(new TestRepository<Plan>());
                controller.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Recipe))
            {
                RecipesController controller = new RecipesController(new TestRepository<Recipe>());
                controller.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingListsController controller = new ShoppingListsController(new TestRepository<ShoppingList>());
                controller.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
            }

        }


        internal static void BaseRendeDeleteDefaultView()
        { 

            T item = new T() { ID = 7000 }; 

            if (typeof(T) == typeof(Ingredient))
            {
                IngredientsController controller = new IngredientsController(new TestRepository<Ingredient>());
        controller.WithCallTo(c => c.Delete(item.ID)).ShouldRenderDefaultView();
    }
            if (typeof(T) == typeof(Menu))
            {
                MenusController controller = new MenusController(new TestRepository<Menu>());
    controller.WithCallTo(c => c.Delete(item.ID)).ShouldRenderDefaultView();
}
            if (typeof(T) == typeof(Person))
            {
                PersonsController controller = new PersonsController(new TestRepository<Person>());
controller.WithCallTo(c => c.Delete(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Plan))
            {
                PlansController controller = new PlansController(new TestRepository<Plan>());
controller.WithCallTo(c => c.Delete(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Recipe))
            {
                RecipesController controller = new RecipesController(new TestRepository<Recipe>());
controller.WithCallTo(c => c.Delete(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingListsController controller = new ShoppingListsController(new TestRepository<ShoppingList>());
controller.WithCallTo(c => c.Delete(item.ID)).ShouldRenderDefaultView();
            }
         }


        internal static void BaseRendeDeleteConfirmedDefaultView()
        {
            T item = new T() { ID = 70001 }; 
            if (typeof(T) == typeof(Ingredient))
            {
                IngredientsController controller = new IngredientsController(new TestRepository<Ingredient>());
                controller.WithCallTo(c => c.DeleteConfirmed(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Menu))
            {
                MenusController controller = new MenusController(new TestRepository<Menu>());
                controller.WithCallTo(c => c.DeleteConfirmed(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Person))
            {
                PersonsController controller = new PersonsController(new TestRepository<Person>());
                controller.WithCallTo(c => c.DeleteConfirmed(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Plan))
            {
                PlansController controller = new PlansController(new TestRepository<Plan>());
                controller.WithCallTo(c => c.DeleteConfirmed(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Recipe))
            {
                RecipesController controller = new RecipesController(new TestRepository<Recipe>());
                controller.WithCallTo(c => c.DeleteConfirmed(item.ID)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingListsController controller = new ShoppingListsController(new TestRepository<ShoppingList>());
                controller.WithCallTo(c => c.DeleteConfirmed(item.ID)).ShouldRenderDefaultView();
            }
        }


        internal static void BaseRendePostEditDefaultView()
        {
            T item = new T();


            if (typeof(T) == typeof(Ingredient))
            {
                IngredientsController controller = new IngredientsController(new TestRepository<Ingredient>());
                controller.WithCallTo(c => c.PostEdit(item as Ingredient)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Menu))
            {
                MenusController controller = new MenusController(new TestRepository<Menu>());
                controller.WithCallTo(c => c.PostEdit(item as Menu)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Person))
            {
                PersonsController controller = new PersonsController(new TestRepository<Person>());
                controller.WithCallTo(c => c.PostEdit(item as Person)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Plan))
            {
                PlansController controller = new PlansController(new TestRepository<Plan>());
                controller.WithCallTo(c => c.PostEdit(item as Plan)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Recipe))
            {
                RecipesController controller = new RecipesController(new TestRepository<Recipe>());
                controller.WithCallTo(c => c.PostEdit(item as Recipe)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingListsController controller = new ShoppingListsController(new TestRepository<ShoppingList>());
                controller.WithCallTo(c => c.PostEdit(item as ShoppingList)).ShouldRenderDefaultView(); 
            }
        }

        internal static void BaseDetachDefaultView()
        {
            ClassCleanup();
            T parent = new T() { ID = 1 };
            Ingredient ingredient= new Ingredient() { ID = 7003 };
            parent.Ingredients.Add(ingredient);
             
            if (typeof(T) == typeof(Ingredient))
            {
                IngredientsController controller = new IngredientsController(new TestRepository<Ingredient>());
                controller.WithCallTo(c => c.Detach(parent, ingredient)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Menu))
            {
                MenusController controller = new MenusController(new TestRepository<Menu>());
                controller.WithCallTo(c => c.Detach(parent, ingredient)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Person))
            {
                PersonsController controller = new PersonsController(new TestRepository<Person>());
                controller.WithCallTo(c => c.Detach(parent, ingredient)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Plan))
            {
                PlansController controller = new PlansController(new TestRepository<Plan>());
                controller.WithCallTo(c => c.Detach(parent, ingredient)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(Recipe))
            {
                RecipesController controller = new RecipesController(new TestRepository<Recipe>());
                controller.WithCallTo(c => c.Detach(parent, ingredient)).ShouldRenderDefaultView();
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingListsController controller = new ShoppingListsController(new TestRepository<ShoppingList>());
                controller.WithCallTo(c => c.Detach(parent, ingredient)).ShouldRenderDefaultView();
            }
        }

        [ClassCleanup()]
        internal static void ClassCleanup()
        {
            Type type = typeof(T);
            string className = type.Name.Split('.').Last();
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + className + @"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        //internal static void BaseDetachAllDefaultView()
        //{
        //    IEntity t = new T() { ID = 4000 };
        //    IEntity ingredient = new Ingredient() { ID = 4000 };

        //    IAttachDetachController<T> controller = GetAttachDetachController();

        //    controller.WithCallTo(c => c.DetachAll(t, ingredient)).ShouldRenderDefaultView();
        //}

        //    internal static void BaseDetachASetOfDefaultView()
        //    {
        //        List<IEntity> selected = new List<IEntity>();
        //        controller.WithCallTo(c => c.DetachASetOf(t, selected)).ShouldRenderDefaultView();
        //    }
        private IGenericController<T> controller;
        private IRepository<T> repo;

        public BaseFluentMVCTest(IGenericController<T> controller, IRepository<T> repo)
        {
            this.controller = controller;
            this.repo = repo;
        }
    }

    [TestClass]
    public class TryOutFluentMVCTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new HomeController();
        }

        [TestMethod]
        public void RenderDefaultView()
        {
            _controller.WithCallTo(c => c.Index())
               .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void BaseRenderDetailsDefaultView()
        {
            IRepository<Ingredient> repository = new TestRepository<Ingredient>();
            IngredientsController ic = new IngredientsController(repository);
            Ingredient t2 = new Ingredient() { ID = 7000 };
            repository.Save(t2);
            ic.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
        }
  

        internal static void BaseRenderDetailsDefaultView2<T>()
        {
            IRepository<Ingredient> repository = new TestRepository<Ingredient>();
            IngredientsController ic = new IngredientsController(repository);
            Ingredient t2 = new Ingredient() { ID = 7000 };
            repository.Save(t2);
            ic.WithCallTo(c => c.Details(7000)).ShouldRenderDefaultView();
        }



    //    [TestMethod]
    //    public void SuperTest2()
    //    {
    //        BaseRenderDetailsDefaultView2<Ingredient>();
    //    }

    //    internal static void BaseRenderDetailsDefaultView3<T>()
    //        where T : BaseEntity, IEntity, new()
    //    {
    //        IRepository<T> repository = new TestRepository<T>();
    //        IGenericController<T> ic = BaseFluentMVCTest<T>.GetController();
    //        T item = new T() { ID = 7000 };
    //        repository.Save(item);
    //        if (typeof(T) == typeof(Ingredient))
    //        {
    //            IngredientsController ix = new IngredientsController(new TestRepository<Ingredient>());
    //            ix.WithCallTo(c => c.Index(1)).ShouldRenderDefaultView();
    //        }

    //    }



    //    [TestMethod]
    //    public void SuperTest3()
    //    {
    //        BaseRenderDetailsDefaultView3<Ingredient>();
    //    }
     }
}
