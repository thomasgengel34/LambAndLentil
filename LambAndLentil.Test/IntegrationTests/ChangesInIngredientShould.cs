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
    public class ChangesInIngredientShould
    {
        static IRepository<Ingredient, IngredientVM> repo;

        public ChangesInIngredientShould()
        {
            repo = new JSONRepository<Ingredient, IngredientVM>();
        }


        /// <summary>
        /// Recipe/Ingredient: ingredient added: when an ingredient is added to a recipe, the recipe should change
        /// </summary>
        [TestMethod]
        public void ChangeParentRecipeWhenIngredientChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/Ingredient: ingredient modified
        /// </summary>
        [TestMethod]
        public void ChangeParentRecipeWhenIngredientChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/Ingredient: ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeRecipeWhenIngredientChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/Ingredient: ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangeParentRecipeWhenIngredientChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/Ingredient: Ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/Ingredient: Ingredient modified
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/Ingredient: Ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  Menu/Ingredient: Ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenIngredientChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Ingredient: Ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Ingredient: Ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Ingredient: Ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Ingredient: Ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangeParentPlanWhenIngredientChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Ingredient: Ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientChildIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Ingredient: Ingredient modified
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientChildIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Ingredient: Ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientChildIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Ingredient: Ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangeParentShoppingListWhenIngredientChildIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Menu/Recipe/Ingredient: Ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenGrandchildIngredientInChildRecipeIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Menu/Recipe/Ingredient: Ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeParentMenuWhenGrandchildIngredientInChildRecipeIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Menu/Recipe/Ingredient: Ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientDeleted()
        {
            // Arrange 
            Menu menu = new Menu();
            menu.ID = int.MaxValue;
            Recipe recipe = new Recipe();
            Ingredient ingredient = new Ingredient();
            recipe.Ingredients.Add(ingredient);
            menu.Recipes.Add(recipe);
            Ingredient ingredient1 = new Ingredient();
            recipe.Ingredients.Add(ingredient1);
            IRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>(); 
            repoMenu.AddT(menu);

            //Act
            // repo.RemoveT(ingredient1); never the correct approach. Keeping it here because this is important.
            menu.Recipes.First().Ingredients.Remove(ingredient1);
            repoMenu.AddT(menu);
            MenuVM retrievedMenuVM = repoMenu.GetTVMById(menu.ID);
            // Assert
            Assert.AreEqual(1, retrievedMenuVM.Recipes.First().Ingredients.Count());

            // Clean Up
        }

        /// <summary>
        /// Menu/Recipe/Ingredient: Ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/Ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/Ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/Ingredient: ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/Menu/Ingredient: ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuWhenIngredientIsRemoved()
        {
            Assert.Fail();
        }
        /// <summary>
        ///  menu/recipe/ingredient: ingredient  added
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientInRecipeIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  menu/recipe/ingredient: ingredient  changed
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// menu/recipe/ingredient: ingredient deleted  
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// menu/recipe/ingredient: ingredient  removed
        /// </summary>
        [TestMethod]
        public void ChangeMenuRecipeWhenIngredientIsRemoved()
        {
            Assert.Fail();
        }
        /// <summary>
        ///  shoppinglist/menu/recipe/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  shoppinglist/menu/recipe/ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientIsChanged()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\ShoppingList\";
            try
            {

                // Arrange
                JSONRepository<Ingredient, IngredientVM> repo = new JSONRepository<Ingredient, IngredientVM>();
                ShoppingList shoppingList = new ShoppingList();
                shoppingList.ID = int.MaxValue;
                Menu menu = new Menu();
                Recipe recipe = new Recipe();
                Ingredient ingredient = new Ingredient();
                recipe.Ingredients.Add(ingredient);
                menu.Recipes.Add(recipe);
                shoppingList.Menus.Add(menu);
                ingredient.Name = "Test ChangeShoppingMenuRecipeWhenIngredientIsChanged";


                using (var fs = File.CreateText(string.Concat(path, int.MaxValue, ".txt")))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(fs, shoppingList);
                }

                // Act
                ingredient.Name = "Test ChangeShoppingMenuRecipeWhenIngredientIsChanged  Has Been Changed";
                repo.SaveT(ingredient);
                //  Get the ingredient child of shoppingList 
                ShoppingList returnedShoppingList = JsonConvert.DeserializeObject<ShoppingList>(File.ReadAllText(String.Concat(path, int.MaxValue, ".txt")));
                IEnumerable<Menu> returnedMenus = returnedShoppingList.Menus;
                IEnumerable<Recipe> returnedRecipes = returnedMenus.First().Recipes;
                Ingredient returnedIngredient = returnedRecipes.First().Ingredients.First();

                //Assert
                Assert.AreEqual(ingredient.Name, returnedIngredient.Name);
                // will need to go into the repo.Save method and determine if ingredient is a child anywhere,then update it there 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup
                File.Delete(String.Concat(path, int.MaxValue, ".txt"));
            }


        }

        /// <summary>
        ///  shoppinglist/menu/recipe/ingredient: ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  shoppinglist/menu/recipe/ingredient: ingredient removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuRecipeWhenIngredientIsRemoved()
        {
            Assert.Fail();
        }
        /// <summary>
        /// plan/menu/recipe/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuRecipeWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// plan/menu/recipe/ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuRecipeWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// plan/menu/recipe/ingredient: ingredient deleted or removed
        /// </summary>
        [TestMethod]
        public void ChangePlanMenuRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/menu/recipe/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuRecipeWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/menu/recipe/ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuRecipeWhenIngredientIsChanged()
        {
            Assert.Fail();
        }


        /// <summary>
        /// ShoppingList/Plan/menu/recipe/ingredient: ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/recipe/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListRecipeWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/recipe/ingredient: ingredient modifed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListRecipeWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/recipe/ingredient: ingredient deleted or removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/recipe/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangePlanRecipeWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/recipe/ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangePlanRecipeWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Plan/recipe/ingredient: ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangePlanRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }


        /// <summary>
        /// ShoppingList/Plan/recipe/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanRecipeWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/recipe/ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanRecipeWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Plan/recipe/ingredient: ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanRecipeWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Menu/Ingredient: Ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Menu/Ingredient: Ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/Menu/Ingredient: Ingredient deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredient: ingredient modified
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientIsModified()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredient: ingredient deleted  
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        /// ShoppingList/plan/menu/ingredient: ingredient  detached
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanMenuWhenIngredientIsDetached()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/Ingredient: ingredient added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/Ingredient: ingredient changed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/Ingredient: ingredient Deleted
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientIsDeleted()
        {
            Assert.Fail();
        }

        /// <summary>
        ///  ShoppingList/Plan/Ingredient: ingredient Removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenIngredientIsRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/multiple ingredients: ingredient added
        /// </summary>
        [TestMethod]
        public void NotChangeRecipeIngredientWhenSecondIngredientIsAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/multiple ingredients: ingredient changed
        /// </summary>
        [TestMethod]
        public void NotChangeRecipeIngredientWhenSecondIngredientIsChanged()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Recipe/multiple ingredients: ingredient deleted
        /// </summary>
        [TestMethod]
        public void NotChangeRecipeIngredientWhenSecondIngredienteIsDeleted()
        {
            Assert.Fail();
        }

        //  TODO:          

        // also some adds: shopping list changes because an ingredient was added to a recipe, etc.
        // there should be two separate methods for when an ingredient is deleted, and when it is detached. Deleted means it is no longer in the database. Detached means it was deliberately removed in a child. The word 'removed' should be changed to 'detached'.   I am not sure that when I start coding that there will be any difference. 

        [TestMethod]
        public void IngredientUpdatedOnUSDASiteShouldBeFlaggedForUserOptionToUpdateParent()
        {
            // if an ingredient is updated on the USDA site, it should be flagged with a user option to update the recipe, menu, etc. to the new information. 
            Assert.Fail();
        }


    }
}
