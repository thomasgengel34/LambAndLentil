using System;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public class RecipesController : BaseAttachDetachController<Recipe>
    {
        public RecipesController(IRepository<Recipe> repository) : base(repository) => Repo = repository; 
    }
}