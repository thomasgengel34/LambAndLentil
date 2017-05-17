namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("INGREDIENT.Ingredient", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("RECIPE.Recipe", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RecipeIngredients", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("MENU.Menu", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("PERSON.Person", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("PLAN.Meal", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("SHOPPINGLIST.ShoppingList", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("SHOPPINGLIST.ShoppingList", "Name", c => c.String());
            AlterColumn("PLAN.Meal", "Name", c => c.String());
            AlterColumn("PERSON.Person", "Name", c => c.String());
            AlterColumn("MENU.Menu", "Name", c => c.String());
            AlterColumn("dbo.RecipeIngredients", "Name", c => c.String());
            AlterColumn("RECIPE.Recipe", "Name", c => c.String());
            AlterColumn("INGREDIENT.Ingredient", "Name", c => c.String());
        }
    }
}
