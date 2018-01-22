using System.Web.Mvc;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Controllers
{
    public interface IGenericController<T> : IAttachDetachController<T>
        where T : BaseEntity, IEntity, new()
    {
        void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "");
      
        ActionResult Create(UIViewType actionMethod);
        ActionResult Delete(int id = 1 );
        ActionResult DeleteConfirmed(int id); 
        ActionResult Details(int id = 1 );
        ActionResult Edit(int id = 1);
     
        ActionResult Index(int? page=1 );

        ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")] T t);

         int PageSize { get; set; }

    }
}