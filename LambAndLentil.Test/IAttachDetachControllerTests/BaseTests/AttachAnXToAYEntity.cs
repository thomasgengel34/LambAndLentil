using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PersonType = LambAndLentil.Domain.Entities.Person;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class AttachAnXToAYEntity<TParent, TChild> : BaseTest<TParent, TChild>
         where TParent : BaseEntity, IEntity, new()
         where TChild : BaseEntity, IEntity, new()
    {
        public void AttachAChildToAParent<Parent, Child>()
            where Parent : BaseEntity, IEntity, new()
        where Child : BaseEntity, IEntity, new()
        {
             Parent parent = new  Parent() { ID = 3000, Description = "test Attach " };
            IRepository<Parent> localRepo = new TestRepository<Parent>();
            localRepo.Save(parent);
            Child child = new Child { ID = 3300 };

            Controller.Attach(parent, child);
            Parent returnedParent = localRepo.GetById(parent.ID) as Parent;

            Assert.AreEqual(1, returnedParent.Ingredients.Count());
            Assert.AreEqual("test Attach ", parent.Description);
        }

        public void MakeAttachments()
        {
            AttachAChildToAParent<TParent, IngredientType>();
            AttachAChildToAParent<MenuType, RecipeType>();
            AttachAChildToAParent<PlanType, RecipeType>();
            AttachAChildToAParent<ShoppingListType, RecipeType>();
            AttachAChildToAParent<PersonType, RecipeType>();
            AttachAChildToAParent<PlanType, MenuType>();
            AttachAChildToAParent<ShoppingListType, MenuType>();
            AttachAChildToAParent<PersonType, MenuType>();
            AttachAChildToAParent<ShoppingListType, PlanType>();
            AttachAChildToAParent<PlanType, PlanType>();
            AttachAChildToAParent<PersonType, ShoppingListType>();
        }
    }
}
