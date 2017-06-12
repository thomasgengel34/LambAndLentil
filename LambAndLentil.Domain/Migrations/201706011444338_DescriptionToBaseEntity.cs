namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionToBaseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("MENU.Menu", "Description", c => c.String());
            AddColumn("PERSON.Person", "Description", c => c.String());
            AddColumn("PLAN.Plan", "Description", c => c.String());
            AddColumn("dbo.RecipeIngredients", "Description", c => c.String());
            AddColumn("SHOPPINGLIST.ShoppingList", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("SHOPPINGLIST.ShoppingList", "Description");
            DropColumn("dbo.RecipeIngredients", "Description");
            DropColumn("PLAN.Plan", "Description");
            DropColumn("PERSON.Person", "Description");
            DropColumn("MENU.Menu", "Description");
        }
    }
}
