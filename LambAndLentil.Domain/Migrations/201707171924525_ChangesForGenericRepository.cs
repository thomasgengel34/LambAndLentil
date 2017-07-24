namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesForGenericRepository : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("INGREDIENT.Ingredient", "Ingredients_ID", "dbo.Ingredients");
            DropForeignKey("RECIPE.Recipe", "Recipes_ID", "dbo.Recipes");
            DropIndex("INGREDIENT.Ingredient", new[] { "Ingredients_ID" });
            DropIndex("RECIPE.Recipe", new[] { "Recipes_ID" });
            DropColumn("INGREDIENT.Ingredient", "Ingredients_ID");
            DropColumn("RECIPE.Recipe", "Recipes_ID");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Recipes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("RECIPE.Recipe", "Recipes_ID", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Ingredients_ID", c => c.Int());
            CreateIndex("RECIPE.Recipe", "Recipes_ID");
            CreateIndex("INGREDIENT.Ingredient", "Ingredients_ID");
            AddForeignKey("RECIPE.Recipe", "Recipes_ID", "dbo.Recipes", "ID");
            AddForeignKey("INGREDIENT.Ingredient", "Ingredients_ID", "dbo.Ingredients", "ID");
        }
    }
}
