using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public interface IGenericController<T> : IAttachDetachController 
    {
        void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "");
      
        ActionResult Create(UIViewType actionMethod);
        ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete);
        ActionResult DeleteConfirmed(int id); 
        ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details);
        ActionResult Edit(int id = 1);
     
        ActionResult Index(int? page=1 );

        ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")] T t);

         int PageSize { get; set; }

    }
}