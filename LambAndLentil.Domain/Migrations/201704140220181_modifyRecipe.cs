namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyRecipe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecipeIngredients", "Recipe_ID", "RECIPE.Recipe");
            DropIndex("dbo.RecipeIngredients", new[] { "Recipe_ID" });
            DropColumn("dbo.RecipeIngredients", "Recipe_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecipeIngredients", "Recipe_ID", c => c.Int());
            CreateIndex("dbo.RecipeIngredients", "Recipe_ID");
            AddForeignKey("dbo.RecipeIngredients", "Recipe_ID", "RECIPE.Recipe", "ID");
        }
    }
}
