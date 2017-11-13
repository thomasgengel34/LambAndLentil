using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public class ShoppingListsController : ShoppingListsGenericController<ShoppingList>
    {
        public ShoppingListsController(IRepository<ShoppingList> repository) : base(repository)
        {
            Repo = repository;
        }
    } 
} 
