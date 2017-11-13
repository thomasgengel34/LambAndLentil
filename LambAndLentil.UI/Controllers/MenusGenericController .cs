using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public class MenusController : MenusGenericController<Menu>
       
    {
        public MenusController(IRepository<Menu> repository) : base(repository) => Repo = repository;
    } 
}
