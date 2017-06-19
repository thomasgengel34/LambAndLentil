using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Models
{
    public class RecipeIndexViewModel
    {
        public int ID { get; set; }
        public Recipe Recipe { get; set; }
        public string ReturnUrl { get; set; }

       
    }
}