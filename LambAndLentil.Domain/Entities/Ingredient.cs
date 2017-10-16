using System; 
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient : BaseEntity, IEntity
    {
        public int ID { get; set; }

        public virtual Recipe Recipe { get; set; }
         

        public Ingredient() : base()
        {

        }

        public Ingredient(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }
         

    }
}