using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System; 
using System.Collections.Generic; 

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : IngredientsGenericController<Ingredient>
    {
        static HttpClient Client { get; set; }
        static string key   = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";

        public IngredientsController(IRepository<Ingredient> repository) : base(repository)
        {
            Repo = repository;
            Client = new HttpClient();

        }

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
              //  HttpClient Client1 = new HttpClient();
                string http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
                string apiKey = "&type=f&format=json&" + ds + "&api_key="; 
                string foodsUrl = String.Concat(http, ndbno, apiKey, key);
             

                string ingredientsList = null;
                HttpResponseMessage response  = await Client.GetAsync(foodsUrl);
                if (response.IsSuccessStatusCode)
                {
                    UsdaFood food = await response.Content.ReadAsAsync<UsdaFood>();
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
            string foodsUrl = String.Concat(http, ndbno, apiKey, key);
            Client.BaseAddress = new Uri(foodsUrl);
           

            string ingredientName = null;
            HttpResponseMessage response = await Client.GetAsync(foodsUrl);
            if (response.IsSuccessStatusCode)
            {
                UsdaFood webFood = await response.Content.ReadAsAsync<UsdaFood>();
                ingredientName = webFood.foods[0].food.desc.Name;
            }
            return ingredientName;

        }

        public async Task<int?> GetNdbnoFromSearchStringAsync(string searchString, string dataSource = "")
        {  // TODO: sanitize searchString
           //  "https://api.nal.usda.gov/ndb/search/?format=json&q=aardvark&sort=n&max=1&offset=0&api_key=DEMO_KEY"


            searchString = ReduceStringLengthToWhatWillWorkOnUSDA(searchString); 
            try
            {

                string http = "https://api.nal.usda.gov/ndb/search/?format=json&q=";
                string apiKey = "&max=1&offset=0&" + dataSource + "&api_key="; 
                string foodsUrl = String.Concat(http, searchString, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl); 

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
            searchString = ReduceStringLengthToWhatWillWorkOnUSDA(searchString);

            try
            {

                string http = "https://api.nal.usda.gov/ndb/search/?format=json&q=";
                string apiKey = "&+max=" + max + "offset=" + offset + "&" + dataSource + "&api_key=";
              
                string foodsUrl = String.Concat(http, searchString, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl);
               
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

        private static string ReduceStringLengthToWhatWillWorkOnUSDA(string searchString)
        {
            const int MaxStringLengthThatWillWork = 43;
            if (searchString.Length > MaxStringLengthThatWillWork)
            {
                searchString = searchString.Substring(searchString.Length - MaxStringLengthThatWillWork);
            }

            return searchString;
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
                string foodsUrl = String.Concat(http, searchString, apiKey, key);
                Client.BaseAddress = new Uri(foodsUrl);
               

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
    }
}