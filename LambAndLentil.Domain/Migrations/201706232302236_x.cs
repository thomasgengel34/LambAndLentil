namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecipeIngredients", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropIndex("dbo.RecipeIngredients", new[] { "Ingredient_ID" });
            AddColumn("INGREDIENT.Ingredient", "Recipe_ID", c => c.Int());
            CreateIndex("INGREDIENT.Ingredient", "Recipe_ID");
            AddForeignKey("INGREDIENT.Ingredient", "Recipe_ID", "RECIPE.Recipe", "ID");
            DropTable("dbo.RecipeIngredients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Measurement = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        AddedByUser = c.String(),
                        ModifiedByUser = c.String(),
                        Ingredient_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("INGREDIENT.Ingredient", "Recipe_ID", "RECIPE.Recipe");
            DropIndex("INGREDIENT.Ingredient", new[] { "Recipe_ID" });
            DropColumn("INGREDIENT.Ingredient", "Recipe_ID");
            CreateIndex("dbo.RecipeIngredients", "Ingredient_ID");
            AddForeignKey("dbo.RecipeIngredients", "Ingredient_ID", "INGREDIENT.Ingredient", "ID");
        }
    }
}
