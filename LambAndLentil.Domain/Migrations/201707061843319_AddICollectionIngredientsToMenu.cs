namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddICollectionIngredientsToMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("INGREDIENT.Ingredient", "Menu_ID", c => c.Int());
            CreateIndex("INGREDIENT.Ingredient", "Menu_ID");
            AddForeignKey("INGREDIENT.Ingredient", "Menu_ID", "MENU.Menu", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("INGREDIENT.Ingredient", "Menu_ID", "MENU.Menu");
            DropIndex("INGREDIENT.Ingredient", new[] { "Menu_ID" });
            DropColumn("INGREDIENT.Ingredient", "Menu_ID");
        }
    }
}
