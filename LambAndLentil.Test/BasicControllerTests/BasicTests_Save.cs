using System;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    public class BasicTests_Save<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            ClassCleanup(); 
          
            SaveTheCreationDateBetweenPostedEdits();
            SaveEditedTWithDescriptionChange();
            SaveOneItem(); 
           
            ClassCleanup();
        }
         

        private static void SaveEditedTWithDescriptionChange()
        {
            SetUpForTests(out repo, out controller, out item);
            item = new T() { ID = 40000 };
            item.Description = "Pre-test";
            repo.Save(item, repo.FullPath);

            item.Description = "Post-test";

            ActionResult ar = controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual("Post-test", returnedItem.Description);

        }

        private static void SaveTheCreationDateBetweenPostedEdits()
        {

            SetUpForTests(out repo, out controller, out item);
            DateTime creationDate = new DateTime(2010, 1, 1);
            item = new T()
            {
                ID = 290,
                CreationDate = creationDate
            };
            repo.Save(item, repo.FullPath);
            ActionResult ar = controller.PostEdit(item);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(creationDate, item.CreationDate);
            Assert.AreEqual(item.CreationDate, returnedItem.CreationDate);
        }
         

        private static void SaveOneItem()
        { 
            IRepository<T> repo = new TestRepository<T>();

            T item = new T()
            {
                ID = 90000,
                Name = "test SaveOneItem"
            }; 
            repo.Save(item);

            T returnedItem= repo.GetById(item.ID);


            Assert.AreEqual(item.ID, returnedItem.ID);
            Assert.AreEqual(item.Name, returnedItem.Name);

        } 
    }
}
