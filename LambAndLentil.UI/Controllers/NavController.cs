using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using WebGrease.Css.Extensions;

namespace LambAndLentil.UI.Controllers
{
    public class NavController : Controller
    {
        private IRepository repository;

        public NavController()
        {
            // for testing
        }


        public NavController(IRepository repo)
        {
            repository = repo;
        }

        //public PartialViewResult MakerMenu(string maker=null, string actionMethod="Index")
        //{
        //    IEnumerable<string> makers = repository.Ingredients
        //                                                        .Select(x => x.Maker)
        //                                                        .Distinct()
        //                                                        .OrderBy(x => x);

        //    ViewBag.SelectedMaker = maker;
        //    ViewBag.actionMethod = actionMethod;
        //    return PartialView(makers);
        //}

        //public PartialViewResult BrandMenu(string brand = null, string actionMethod = "Index")
        //{
        //    IEnumerable<string> brands = repository.Ingredients
        //                                                        .Select(x => x.Brand)
        //                                                        .Distinct()
        //                                                        .OrderBy(x => x);

        //    ViewBag.SelectedBrand = brand;
        //    ViewBag.actionMethod = actionMethod;
        //    return PartialView(brands);
        //}

        //public PartialViewResult FoodGroupMenu(string foodGroup = null, string actionMethod = "Index")
        //{
        //    IEnumerable<string> foodGroups = repository.Ingredients
        //                                                        .Select(x => x.FoodGroup)
        //                                                        .Distinct()
        //                                                        .OrderBy(x => x);

        //    ViewBag.SelectedBrand = foodGroup;
        //    ViewBag.actionMethod = actionMethod;
        //    return PartialView(foodGroups);
        //}

        //public PartialViewResult CategoryMenu(string category = null, string actionMethod = "Index")
        //{
        //    IEnumerable<string> categories = repository.Ingredients
        //                                                        .Select(x => x.Category)
        //                                                        .Distinct()
        //                                                        .OrderBy(x => x);

        //    ViewBag.SelectedBrand = category;
        //    ViewBag.actionMethod = actionMethod;
        //    return PartialView(categories);
        //}
    }
}