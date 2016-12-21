namespace LambAndLentil.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "RECIPE.Recipe",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Servings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FoodGroup = c.Int(nullable: false),
                        Calories = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId);
            
            AddColumn("dbo.Ingredients", "Recipe_RecipeId", c => c.Int());
            CreateIndex("dbo.Ingredients", "Recipe_RecipeId");
            AddForeignKey("dbo.Ingredients", "Recipe_RecipeId", "RECIPE.Recipe", "RecipeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Recipe_RecipeId", "RECIPE.Recipe");
            DropIndex("dbo.Ingredients", new[] { "Recipe_RecipeId" });
            DropColumn("dbo.Ingredients", "Recipe_RecipeId");
            DropTable("RECIPE.Recipe");
        }
    }
}
