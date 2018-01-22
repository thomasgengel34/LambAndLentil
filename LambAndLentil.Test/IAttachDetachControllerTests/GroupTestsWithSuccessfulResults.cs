using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PersonType = LambAndLentil.Domain.Entities.Person;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests
{
    [TestClass]
    public class GroupTestsWithSuccessfulResults
    {
        List<Type> types = new List<Type>()
        {
            typeof(IngredientType),
            typeof(MenuType),
            typeof(PersonType),
            typeof(PlanType),
            typeof(RecipeType),
            typeof(ShoppingListType)
        };



        [TestMethod]
        public void TestIterator()
        {
            TestsDealer(typeof(IngredientType), typeof(RecipeType));
            //foreach (Type parent in types)
            //{
            //    foreach (Type child in types)
            //    {
            //        TestsDealer(parent, child);     // TODO: resolve technical difficulty here. May have to use a private class. Or use these as normal parameters (TestRunner(parent, child)). 
            //    }
            //}
        }

        private void TestsDealer(Type parent, Type child)
        {
            TestsRunner<IngredientType, IngredientType>();

            TestsRunner<IngredientType, RecipeType>();

            TestsRunner<IngredientType, MenuType>();

            TestsRunner<IngredientType, PlanType>();

            TestsRunner<IngredientType, ShoppingListType>();

            TestsRunner<IngredientType, PersonType>();

        }

         
        public static void TestsRunner<TParent, TChild>()
             where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
        {
            BaseControllerShouldAttachXAndReturn<TParent, TChild> baseAttach = new BaseControllerShouldAttachXAndReturn<TParent, TChild>();

            BaseControllerShouldDetachXAndReturn<TParent, TChild> baseDetach = new BaseControllerShouldDetachXAndReturn<TParent, TChild>();

            TParent parent = new TParent();
            TChild child = new TChild();
            bool CanBeChild = parent.CanHaveChild(child);
            if (CanBeChild)
            {
                baseAttach.BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValid();
                //baseAttach.BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidWhenAttaching();
                //baseDetach.BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll();
                //baseDetach.BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients();
            }
            else
            {
                //baseAttach.BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid();   // does this really belong here?
                //baseAttach.BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild();
                baseDetach.BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
                baseDetach.BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
            }
            // TODO: add more
        }
    }
}
