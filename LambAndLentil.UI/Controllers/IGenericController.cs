using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public interface IGenericController<T>
    {
        void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "");
        ActionResult AttachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber);
        ViewResult Create(UIViewType actionMethod);
        ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete);
        ActionResult DeleteConfirmed(int id);
        ActionResult DetachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber=0);
        ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details);
        ActionResult Edit(int id = 1); 
        ViewResult Index(int page = 1);

        ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")] T t);

         int PageSize { get; set; }

        void DetachAllIngredients(int ID);
        void DetachAllIngredients(int ID, List<Ingredient> selected);
        ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0);
        ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber=0);
    }
}