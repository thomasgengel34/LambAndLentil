using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.UI.Models;
using AutoMapper.Mappers;
using AutoMapper;
using Microsoft.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : BaseController
    {
        public IngredientsController(IRepository repo) : base(repo)
        {
            List<Ingredient> ingredients = repository.Ingredients.ToList<Ingredient>();
            Ingredients = new Ingredients(ingredients);
        }


        // GET: Ingredients 
        //[OutputCache(CacheProfile = "IngredientIndexAsc")]
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(UIControllerType.Ingredients, page);
            return View(view.ViewName, view.Model);
        }


        // GET: Ingredients/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title =actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            { 
                return BaseDelete<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
            }
            else if (actionMethod== UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<Ingredient, IngredientsController>(UIControllerType.Ingredients, id);
            }
            return BaseDetails<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
        }

        // GET: Ingredients/Create

        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate<IngredientVM>(actionMethod);
        }


        // GET: Ingredients/Edit/5
        [HttpGet]
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Maker, Brand, Name,Description,ServingSize,ServingSizeUnit,ServingsPerContainer, ContainerSize, ContainerSizeUnit, ContainerSizeInGrams,Calories,CalFromFat,Kosher, FoodGroup, CreationDate,ModifiedDate,DataSource")]  IngredientVM ingredientVM)
        {
            return BasePostEdit<Ingredient, IngredientsController, IngredientVM>(ingredientVM);
        }

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1,UIViewType actionMethod=UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) 
        { 
            return BaseDeleteConfirmed<Ingredient, IngredientsController>(UIControllerType.Ingredients, id);
        }
         
    }
}
