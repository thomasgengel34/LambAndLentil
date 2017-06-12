namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientFish : DbMigration
    {
        public override void Up()
        {
            AddColumn("INGREDIENT.Ingredient", "Fish", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("INGREDIENT.Ingredient", "Fish");
        }
    }
}
