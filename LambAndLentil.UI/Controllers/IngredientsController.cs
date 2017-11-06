using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : IngredientsGenericController<Ingredient>
    {
        public IngredientsController(IRepository<Ingredient> repository) : base(repository) => Repo = repository;
    }
}