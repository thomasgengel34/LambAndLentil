using System;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

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
            string controllerName = String.Concat(ParentClassName, "sController");
            string controllerPath = "Controllers";
            string assemblyName = " LambAndLentil.UI";
            Type returnController = Type.GetType(controllerPath+"."+controllerName+ ", "+assemblyName,true );
            return ( IGenericController<TParent>)returnController;
        }
        
        private PropertyInfo ChildPropertyFactory()
        {
            string ChildClassName = typeof(TChild).ToString().Split('.').Last();
           var propertyInfos = typeof(TParent).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            PropertyInfo property =  (from p in propertyInfos where p.Name == ChildClassName select p).FirstOrDefault();

            return property;
        }

        internal static void BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied() {

            Assert.Fail();
        } 
    }
}
