using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        internal static IGenericController<TParent> Controller { get; set; }
       protected internal static IEntity Parent { get; set; }
        internal static IEntity Child { get; set; }
        internal static IRepository<TParent> ParentRepo = new TestRepository<TParent>();
        internal static IRepository<TChild> ChildRepo = new TestRepository<TChild>();
        internal static string ParentClassName { get; private set; }
        internal static string ChildClassName { get; private set; }
        internal static PropertyInfo ChildProperty { get; private set; } 

        public BaseTest()
        {
            ParentClassName = typeof(TParent).ToString().Split('.').Last();
            Controller = ControllerFactory();
            ChildProperty = ChildPropertyFactory();
            Parent = new TParent()
            {
                ID = 1,
                Name = "Name from BaseTest",
                Description = "BasicTest",
                CreationDate = new DateTime(2011, 2, 2),

            };
            ParentRepo.Save((TParent)Parent);
            Child = new TChild()
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

      

        [TestCleanup]
        public void TestCleanup() => ClassCleanup();

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + ParentClassName + @"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

    }
}
