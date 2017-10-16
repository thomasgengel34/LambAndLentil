using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : IngredientsGenericController<Ingredient>
    {
        public IngredientsController(IRepository<Ingredient> repository) : base(repository)
        {
            Repo = repository;
        }


    }

    public class IngredientsGenericController<T> : BaseController<Ingredient>
          where T : Ingredient
    {
        static HttpClient Client { get; set; }

        public IngredientsGenericController(IRepository<Ingredient> repository) : base(repository)
        {
            Repo = repository;
            Client = new HttpClient();
        }

        // GET: Ingredients  
        public ViewResult Index(int page = 1)
        {
            return BaseIndex(Repo, page);
        }

        public async Task<int?> GetIngredients(string searchString)
        {
            try
            {
                //string http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
                //string apiKey = "&type=f&format=json&api_key=";
                //string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                //string foodsUrl = String.Concat(http, ndbno, apiKey, key);
                //Client.BaseAddress = new Uri(foodsUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string foo = "https://api.nal.usda.gov/ndb/search/?format=json&q=aardvark&sort=n&max=1&offset=0&api_key=DEMO_KEY";
                int? ndbno = null;
                HttpResponseMessage response = await Client.GetAsync(foo);
                if (response.IsSuccessStatusCode)
                {
                    UsdaSingleItemSearch item = await response.Content.ReadAsAsync<UsdaSingleItemSearch>();
                    ndbno = item.list.item[0].ndbno;
                }
                return ndbno;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<string> GetIngredientsByNdbno(int ndbno)
        {
            try
            {
                string http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
                string apiKey = "&type=f&format=json&api_key=";
                string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                string foodsUrl = String.Concat(http, ndbno, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string ingredientsList = null;
                HttpResponseMessage response = await Client.GetAsync(foodsUrl);
                if (response.IsSuccessStatusCode)
                {
                    UsdaFood food = await response.Content.ReadAsAsync<UsdaFood>();
                    ingredientsList = food.foods[0].food.ing.desc;
                }
                return ingredientsList;

            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public async Task<string> GetIngredientName(int ndbno)
        {
            string http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
            string apiKey = "&type=f&format=json&api_key=";
            string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
            string foodsUrl = String.Concat(http, ndbno, apiKey, key);
            Client.BaseAddress = new Uri(foodsUrl);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string ingredientName = null;
            HttpResponseMessage response = await Client.GetAsync(foodsUrl);
            if (response.IsSuccessStatusCode)
            {
                UsdaFood webFood = await response.Content.ReadAsAsync<UsdaFood>();
                ingredientName = webFood.foods[0].food.desc.Name;
            }
            return ingredientName;

        }

        public int GetNdbno(string searchString)
        {
            return 45078606;
        }

        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo, UIControllerType.Ingredients, id, actionMethod);
        }

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        }

        // GET: Ingredients/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo, UIControllerType.Ingredients, id, UIViewType.Edit);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")]  Ingredient ingredient)
        {
            return BasePostEdit(Repo, ingredient);
        }

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete(Repo, UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Ingredients, id);
        }

        // cannot attach or detach an ingredient

        public async Task<int?> GetNdbnoFromSearchStringAsync(string searchString)
        {
            try
            {
                //string http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
                //string apiKey = "&type=f&format=json&api_key=";
                //string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                //string foodsUrl = String.Concat(http, ndbno, apiKey, key);
                //Client.BaseAddress = new Uri(foodsUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string foo = "https://api.nal.usda.gov/ndb/search/?format=json&q=aardvark&sort=n&max=1&offset=0&api_key=DEMO_KEY";
                int? ndbno = null;
                HttpResponseMessage response = await Client.GetAsync(foo);
                if (response.IsSuccessStatusCode)
                {
                    UsdaSingleItemSearch item = await response.Content.ReadAsAsync<UsdaSingleItemSearch>();
                    ndbno = item.list.item[0].ndbno;
                }
                return ndbno;

            }
            catch (Exception ex)
            {
                return -1;
            }
        } 
    }
}
