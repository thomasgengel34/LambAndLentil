using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{ 
    }
internal class ClassPropertyChanges_Person<T> : BaseControllerTest<T>
     where T : BaseEntity, IEntity, new()
{

    private static T returnedItem { get; set; }

    internal static void TestRunner()
    {
        if (typeof(T) == typeof(Person))
        {
            ShouldEditFirstAndLastNamesAndFullNameIsChanged();
            ShouldEditFullNameAndFirstNameAndLastNameChangeToEmptyStrings();
            ShouldEditLastNameOnlyAndFullNameIsChangedBuFirstNameIsNot();
            ShouldEditFirstNameOnlyAndFullNameIsChangedButLastNameIsNot();
        }
    }


    private static void ShouldEditFirstAndLastNamesAndFullNameIsChanged()
    { 
            Person item = new Person() { ID = 500 };

            repo.Save(item as T);
            item.FirstName = "Changed";
            item.LastName = "Altered";

            controller.PostEdit(item as T);
            Person returnedItem = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Changed Altered", returnedItem.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Altered", returnedItem.FullName);
            Assert.AreEqual("Changed", returnedItem.FirstName);
            Assert.AreEqual("Altered", returnedItem.LastName); 
    }



    private static void ShouldEditFullNameAndFirstNameAndLastNameChangeToEmptyStrings()
    { 
            Person item = new Person() { ID = 501 };
            item.FullName = "Changed";
            repo.Save(item as T);

            controller.PostEdit(item as T);
            Person returnedItem = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Changed", returnedItem.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", returnedItem.FullName);
            Assert.AreEqual("", returnedItem.FirstName);
            Assert.AreEqual("", returnedItem.LastName);
        
    }


    private static void ShouldEditFirstNameOnlyAndFullNameIsChangedButLastNameIsNot()
    { 
            Person item = new Person() { ID = 502 };
            item.FirstName = "Changed";

            repo.Save(item as T);

            controller.PostEdit(item as T);
            Person Returneditem = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Changed Created", Returneditem.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Created", Returneditem.FullName);
            Assert.AreEqual("Changed", Returneditem.FirstName);
            Assert.AreEqual("Created", Returneditem.LastName);
        
    }

    private static void ShouldEditLastNameOnlyAndFullNameIsChangedBuFirstNameIsNot()
    { 
            Person item = new Person() { ID = 503 };
            item.LastName = "Altered";

            repo.Save(item as T);

            controller.PostEdit(item as T);

            Person returnedItem = repo.GetById(item.ID) as Person;

            Assert.AreEqual("Newly Altered", returnedItem.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Newly Altered", returnedItem.FullName);
            Assert.AreEqual("Newly", returnedItem.FirstName);
            Assert.AreEqual("Altered", returnedItem.LastName);
        
    }
}
