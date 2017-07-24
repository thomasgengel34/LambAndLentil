namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PluralClassesID : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("INGREDIENT.Ingredient", "Ingredients_ID", c => c.Int());
            AddColumn("RECIPE.Recipe", "Recipes_ID", c => c.Int());
            CreateIndex("INGREDIENT.Ingredient", "Ingredients_ID");
            CreateIndex("RECIPE.Recipe", "Recipes_ID");
            AddForeignKey("INGREDIENT.Ingredient", "Ingredients_ID", "dbo.Ingredients", "ID");
            AddForeignKey("RECIPE.Recipe", "Recipes_ID", "dbo.Recipes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("RECIPE.Recipe", "Recipes_ID", "dbo.Recipes");
            DropForeignKey("INGREDIENT.Ingredient", "Ingredients_ID", "dbo.Ingredients");
            DropIndex("RECIPE.Recipe", new[] { "Recipes_ID" });
            DropIndex("INGREDIENT.Ingredient", new[] { "Ingredients_ID" });
            DropColumn("RECIPE.Recipe", "Recipes_ID");
            DropColumn("INGREDIENT.Ingredient", "Ingredients_ID");
            DropTable("dbo.Recipes");
            DropTable("dbo.Ingredients");
        }
    }
}
