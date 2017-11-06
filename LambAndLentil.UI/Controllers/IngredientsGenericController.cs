using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{  
    public class IngredientsGenericController<T> : BaseController<Ingredient>, IGenericController<T> where T : Ingredient
    {
        static HttpClient Client { get; set; }

        public IngredientsGenericController(IRepository<Ingredient> repository) : base(repository)
        {
            Repo = repository;
            Client = new HttpClient();
        }

        // GET: Ingredients  
        public ViewResult Index(int page = 1) => BaseIndex(Repo, page);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="foodGroup">default value of empty string</param>
        /// <param name="usdaWebApiDataSource">default value of BrandedFoodProducts</param>
        /// <returns></returns>
        public async Task<string> GetIngredientsFromDescription(string searchString, string foodGroup = "", UsdaWebApiDataSource usdaWebApiDataSource = UsdaWebApiDataSource.BrandedFoodProducts)
        {
            try
            {
                string ds = usdaWebApiDataSource == UsdaWebApiDataSource.BrandedFoodProducts ? "" : "ds=Standard+Reference";
                HttpClient Client2 = new HttpClient();
                string http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
                string apiKey = String.Concat("&type=f&format=json&", ds, "&fgcd=", foodGroup, "&api_key=");
                int? ndbno = await GetNdbnoFromSearchStringAsync(searchString, ds);
                string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                string foodsUrl = String.Concat(http, ndbno, apiKey, key);
                Client2.BaseAddress = new Uri(foodsUrl);
                Client2.DefaultRequestHeaders.Accept.Clear();
                Client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string description = "nothing returned";

                HttpResponseMessage response2 = await Client2.GetAsync(foodsUrl);
                if (response2.IsSuccessStatusCode)
                {
                    UsdaFood food = await response2.Content.ReadAsAsync<UsdaFood>();
                 
                    if (ds == "ds=Standard+Reference")
                    {
                        description = food.foods[0].food.desc.Name;
                    }
                    else
                    {
                        description = food.foods[0].food.ing != null ? food.foods[0].food.ing.desc : food.foods[0].food.footnotes[0].desc;
                    }


               

                    // do I need to check what happens if ndbno does not have a value??
                    //{
                    //    description = await GetIngredientsByNdbno((int)ndbno, usdaWebApiDataSource);
                    //}
                }
                return description;

            }
            catch (Exception ex)
            {
                return "error in getting ingredient from web:" + ex.Message;
            }
        }

        public async Task<string> GetIngredientsByNdbno(int ndbno, UsdaWebApiDataSource usdaWebApiDataSource = UsdaWebApiDataSource.BrandedFoodProducts)
        {
            try
            {
                string ds = usdaWebApiDataSource == UsdaWebApiDataSource.BrandedFoodProducts ? "" : "ds=Standard+Reference";
                HttpClient Client1 = new HttpClient();
                string http1 = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
                string apiKey1 = "&type=f&format=json&" + ds + "&api_key=";
                string key1 = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                string foodsUrl1 = String.Concat(http1, ndbno, apiKey1, key1);
                Client1.BaseAddress = new Uri(foodsUrl1);
                Client1.DefaultRequestHeaders.Accept.Clear();
                Client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string ingredientsList = null;
                HttpResponseMessage response1 = await Client.GetAsync(foodsUrl1);
                if (response1.IsSuccessStatusCode)
                {
                    UsdaFood food = await response1.Content.ReadAsAsync<UsdaFood>();
                    if (ds == "ds=Standard + Reference")
                    {
                        ingredientsList = food.foods[0].food.desc.Name;
                    }
                    else
                    {
                        ingredientsList = food.foods[0].food.ing != null ? food.foods[0].food.ing.desc : food.foods[0].food.footnotes[0].desc;
                    }
                }
                return ingredientsList;

            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public async Task<string> GetIngredientNameFromNdbno(int ndbno)
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
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")]  T t)
        {
            return BasePostEdit(Repo, t);
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





        public ActionResult AttachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber=0) => BaseAttach<Ingredient>(Repo, ingredientID, ingredient, 0);

        public ActionResult DetachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber=0)
        {
            return BaseAttach<Ingredient>(Repo,  ingredientID, ingredient, AttachOrDetach.Detach);
        }

        public async Task<int?> GetNdbnoFromSearchStringAsync(string searchString, string dataSource = "")
        {  // TODO: sanitize searchString
            //  "https://api.nal.usda.gov/ndb/search/?format=json&q=aardvark&sort=n&max=1&offset=0&api_key=DEMO_KEY"


            if (searchString.Length > 43)
            {
                searchString = searchString.Substring(searchString.Length - 43);
            }


            try
            {

                string http = "https://api.nal.usda.gov/ndb/search/?format=json&q=";
                string apiKey = "&max=1&offset=0&" + dataSource + "&api_key=";
                string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                string foodsUrl = String.Concat(http, searchString, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                int? ndbno = null;
                HttpResponseMessage response = await Client.GetAsync(foodsUrl);
                if (response.IsSuccessStatusCode)
                {
                    UsdaSingleItemSearch item = await response.Content.ReadAsAsync<UsdaSingleItemSearch>();
                    ndbno = item.list.item[0].ndbno;
                }
                return ndbno;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<string>> GetIngredientNamesAsync(string searchString, string dataSource = "", int max = 100, int offset = 0)
        {
            if (searchString.Length > 43)
            {
                searchString = searchString.Substring(searchString.Length - 43);
            }


            try
            {

                string http = "https://api.nal.usda.gov/ndb/search/?format=json&q=";
                string apiKey = "&+max=" + max + "offset=" + offset + "&" + dataSource + "&api_key=";
                string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                string foodsUrl = String.Concat(http, searchString, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                UsdaSingleItemSearch item = new UsdaSingleItemSearch();
                List<string> list = new List<string>();

                HttpResponseMessage response = await Client.GetAsync(foodsUrl);
                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadAsAsync<UsdaSingleItemSearch>();
                    foreach (var x in item.list.item)
                    {
                        list.Add(x.name);
                    }


                }
                return list;

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return new List<string>();
            }
        }


        public async Task<long> GetIngredientCountAsync(string searchString, string dataSource = "", long max = Int64.MaxValue, int offset = 0, string foodGroup = "")
        {
            if (searchString.Length > 43)
            {
                searchString = searchString.Substring(searchString.Length - 43);
            }


            try
            {

                string http = "https://api.nal.usda.gov/ndb/search/?format=json&q=";

                string apiKey = String.Concat("&+max=", max, "offset=", offset, "&ds=", dataSource, "&fg=", foodGroup, "&api_key=");
                string key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
                string foodsUrl = String.Concat(http, searchString, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                UsdaSingleItemSearch item = new UsdaSingleItemSearch();
                long total = 0;
                HttpResponseMessage response = await Client.GetAsync(foodsUrl);
                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadAsAsync<UsdaSingleItemSearch>();
                    total = item.list.total;


                }
                return total; 
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                return -1;
            }
        }

        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "")
        {
            BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);
        }

        
    }
}
