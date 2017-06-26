namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryAgainAndAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("INGREDIENT.Ingredient", "Recipe_ID", "RECIPE.Recipe");
            DropIndex("INGREDIENT.Ingredient", new[] { "Recipe_ID" });
            DropColumn("INGREDIENT.Ingredient", "Recipe_ID");
        }
        
        public override void Down()
        {
            AddColumn("INGREDIENT.Ingredient", "Recipe_ID", c => c.Int());
            CreateIndex("INGREDIENT.Ingredient", "Recipe_ID");
            AddForeignKey("INGREDIENT.Ingredient", "Recipe_ID", "RECIPE.Recipe", "ID");
        }
    }
}
