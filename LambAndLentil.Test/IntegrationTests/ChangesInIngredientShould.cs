using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using System.IO;
using Newtonsoft.Json;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Abstract;

namespace LambAndLentil.Test.IntegrationTests
{
    [Ignore]
    [TestClass]
    public class ChangesInIngredientVMShould
    {
        static IRepository<IngredientVM> repo;

        public ChangesInIngredientVMShould()
        {
            repo = new TestRepository<IngredientVM>();
        }


        /// <summary>
        /// Recipe/IngredientVM: ingredientVM added: when an ingredientVM is added to a recipe, the recipe should change
        /// </summary>
        [TestMethod]
        public void ChangeParentRecipeWhenIngredientVMChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/IngredientVM: ingredientVM modified
        /// </summary>
        [TestMethod]
        public void ChangeParentRecipeWhenIngredientVMChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/IngredientVM: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeRecipeWhenIngredientVMChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/IngredientVM: ingredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangeParentRecipeWhenIngredientVMChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/IngredientVM: IngredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientVMChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/IngredientVM: IngredientVM modified
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientVMChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/IngredientVM: IngredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientVMChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/IngredientVM: IngredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientVMChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/IngredientVM: IngredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientVMChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/IngredientVM: IngredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientVMChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/IngredientVM: IngredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientVMChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/IngredientVM: IngredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientVMChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/IngredientVM: IngredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientVMChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/IngredientVM: IngredientVM modified
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientVMChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/IngredientVM: IngredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientVMChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/IngredientVM: IngredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientVMChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Menu/Recipe/IngredientVM: IngredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenGrandchildIngredientVMInChildRecipeIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Menu/Recipe/IngredientVM: IngredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenGrandchildIngredientVMInChildRecipeIsModified()
        {
            Assert.Fail();
        }

        ///// <summary>
        ///// Menu/Recipe/IngredientVM: IngredientVM deleted
        ///// </summary>
        //[TestMethod]
        //public void ChangeMenuRecipeWhenIngredientVMDeleted()
        //{
        //    // Arrange 
        //    Menu menu = new Menu();
        //    menu.ID = int.MaxValue;
        //    Recipe recipe = new Recipe();
        //    IngredientVM ingredientVM = new IngredientVM();
        //    recipe.IngredientVMs.Add(ingredientVM);
        //    menu.Recipes.Add(recipe);
        //    IngredientVM ingredientVM1 = new IngredientVM();
        //    recipe.IngredientVMs.Add(ingredientVM1);
        //    IRepository<MenuVM> repoMenu = new JSONRepository< MenuVM>(); 
        //    repoMenu.Add(menu);

        //    //Act
        //    // repo.RemoveT(ingredientVM1); never the correct approach. Keeping it here because this is important.
        //    menu.Recipes.First().IngredientVMs.Remove(ingredientVM1);
        //    repoMenu.Add(menu);
        //    MenuVM retrievedMenuVM = repoMenu.GetById(menu.ID);
        //    // Assert
        //    Assert.AreEqual(1, retrievedMenuVM.Recipes.First().IngredientVMs.Count());

        //    // Clean Up
        //}

        /// <summary>
        /// Menu/Recipe/IngredientVM: IngredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientVMRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/IngredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/IngredientVM: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientVMIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/IngredientVM: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/IngredientVM: ingredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientVMIsRemoved()
        {
            Assert.Fail();
        }
        /// <summary>
        ///  menu/recipe/ingredientVM: ingredientVM  added
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientVMInRecipeIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  menu/recipe/ingredientVM: ingredientVM  changed
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// menu/recipe/ingredientVM: ingredientVM deleted  
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// menu/recipe/ingredientVM: ingredientVM  removed
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientVMIsRemoved()
        {
            Assert.Fail();
        }


        /// <summary>
        ///  shoppinglist/menu/recipe/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  shoppinglist/menu/recipe/ingredientVM: ingredientVM changed
        /// </summary>
       // [TestMethod]
        //public void ChangeShoppingListMenuRecipeWhenIngredientVMIsChanged()
        //{
        //    string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\ShoppingList\";
        //    try
        //    {

        //        // Arrange
                
        //        ShoppingList shoppingList = new ShoppingList();
        //        shoppingList.ID = int.MaxValue;
        //        Menu menu = new Menu();
        //        Recipe recipe = new Recipe();
        //        Ingredient  ingredient  = new Ingredient();
        //        recipe.Ingredients.Add(ingredient);
        //        menu.Recipes.Add(recipe);
        //        shoppingList.Menus.Add(menu);
        //        ingredient.Name = "Test ChangeShoppingMenuRecipeWhenIngredientVMIsChanged";


        //        using (var fs = File.CreateText(string.Concat(path, int.MaxValue, ".txt")))
        //        {
        //            JsonSerializer serializer = new JsonSerializer();
        //            serializer.Serialize(fs, shoppingList);
        //        }

        //        // Act
        //        ingredient.Name = "Test ChangeShoppingMenuRecipeWhenIngredientVMIsChanged  Has Been Changed";
        //        repo.Save(ingredient);
        //        //  Get the ingredientVM child of shoppingList 
        //        ShoppingList returnedShoppingList = JsonConvert.DeserializeObject<ShoppingList>(File.ReadAllText(String.Concat(path, int.MaxValue, ".txt")));
        //        IEnumerable<Menu> returnedMenus = returnedShoppingList.Menus;
        //        IEnumerable<Recipe> returnedRecipes = returnedMenus.First().Recipes;
        //        IngredientVM returnedIngredientVM = returnedRecipes.First().IngredientVMs.First();

        //        //Assert
        //        Assert.AreEqual(ingredientVM.Name, returnedIngredientVM.Name);
        //        // will need to go into the repo.Save method and determine if ingredientVM is a child anywhere,then update it there 
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup
        //        File.Delete(String.Concat(path, int.MaxValue, ".txt"));
        //    }


        // }

        /// <summary>
        ///  shoppinglist/menu/recipe/ingredientVM: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  shoppinglist/menu/recipe/ingredientVM: ingredientVM removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientVMIsRemoved()
        {
            Assert.Fail();
        }
        /// <summary>
        /// plan/menu/recipe/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuRecipeWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// plan/menu/recipe/ingredientVM: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuRecipeWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// plan/menu/recipe/ingredientVM: ingredientVM deleted or removed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/menu/recipe/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuRecipeWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/menu/recipe/ingredientVM: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuRecipeWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }


        /// <summary>
        /// ShoppingList/Plan/menu/recipe/ingredientVM: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/recipe/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListRecipeWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/recipe/ingredientVM: ingredientVM modifed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListRecipeWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/recipe/ingredientVM: ingredientVM deleted or removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/recipe/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangePlanRecipeWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/recipe/ingredientVM: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangePlanRecipeWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/recipe/ingredientVM: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangePlanRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }


        /// <summary>
        /// ShoppingList/Plan/recipe/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanRecipeWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/recipe/ingredientVM: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanRecipeWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/recipe/ingredientVM: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanRecipeWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Menu/IngredientVM: IngredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Menu/IngredientVM: IngredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Menu/IngredientVM: IngredientVM deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredientVM: ingredientVM modified
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientVMIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredientVM: ingredientVM deleted  
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredientVM: ingredientVM  detached
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientVMIsDetached()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/IngredientVM: ingredientVM added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/IngredientVM: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/IngredientVM: ingredientVM Deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientVMIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/IngredientVM: ingredientVM Removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientVMIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/multiple ingredientVMs: ingredientVM added
        /// </summary>
        [TestMethod]
        public void NotChangeRecipeIngredientVMWhenSecondIngredientVMIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/multiple ingredientVMs: ingredientVM changed
        /// </summary>
        [TestMethod]
        public void NotChangeRecipeIngredientVMWhenSecondIngredientVMIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/multiple ingredientVMs: ingredientVM deleted
        /// </summary>
        [TestMethod]
        public void NotChangeRecipeIngredientVMWhenSecondIngredientVMeIsDeleted()
        {
            Assert.Fail();
        }

        //  TODO:          

        // also some adds: shopping list changes because an ingredientVM was added to a recipe, etc.
        // there should be two separate methods for when an ingredientVM is deleted, and when it is detached. Deleted means it is no longer in the database. Detached means it was deliberately removed in a child. The word 'removed' should be changed to 'detached'.   I am not sure that when I start coding that there will be any difference. 

        [TestMethod]
        public void IngredientVMUpdatedOnUSDASiteShouldBeFlaggedForUserOptionToUpdateParent()
        {
            // if an ingredientVM is updated on the USDA site, it should be flagged with a user option to update the recipe, menu, etc. to the new information. 
            Assert.Fail();
        }


    }
}
