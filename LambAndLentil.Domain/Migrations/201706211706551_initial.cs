namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("INGREDIENT.Ingredient", "RecipeID", "RECIPE.Recipe");
            DropIndex("INGREDIENT.Ingredient", new[] { "RecipeID" });
            DropColumn("INGREDIENT.Ingredient", "RecipeID");
        }
        
        public override void Down()
        {
            AddColumn("INGREDIENT.Ingredient", "RecipeID", c => c.Int(nullable: false));
            CreateIndex("INGREDIENT.Ingredient", "RecipeID");
            AddForeignKey("INGREDIENT.Ingredient", "RecipeID", "RECIPE.Recipe", "ID", cascadeDelete: true);
        }
    }
}
