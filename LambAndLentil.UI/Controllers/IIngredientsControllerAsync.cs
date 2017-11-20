using System.Collections.Generic;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public interface IIngredientsControllerAsync
    {
        Task<long> GetIngredientCountAsync(string searchString, string dataSource = "", long max = long.MaxValue, int offset = 0, string foodGroup = "");

        Task<string> GetIngredientNameFromNdbno(int ndbno);

        Task<IEnumerable<string>> GetIngredientNamesAsync(string searchString, string dataSource = "", int max = 100, int offset = 0);

        Task<string> GetIngredientsByNdbno(int ndbno, UsdaWebApiDataSource usdaWebApiDataSource = UsdaWebApiDataSource.BrandedFoodProducts);

        Task<string> GetIngredientsFromDescription(string searchString, string foodGroup = "", UsdaWebApiDataSource usdaWebApiDataSource = UsdaWebApiDataSource.BrandedFoodProducts);

        Task<int?> GetNdbnoFromSearchStringAsync(string searchString, string dataSource = "");
    }
}