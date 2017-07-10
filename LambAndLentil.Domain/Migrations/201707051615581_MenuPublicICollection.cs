namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuPublicICollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("RECIPE.Recipe", "Menu_ID", c => c.Int());
            CreateIndex("RECIPE.Recipe", "Menu_ID");
            AddForeignKey("RECIPE.Recipe", "Menu_ID", "MENU.Menu", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("RECIPE.Recipe", "Menu_ID", "MENU.Menu");
            DropIndex("RECIPE.Recipe", new[] { "Menu_ID" });
            DropColumn("RECIPE.Recipe", "Menu_ID");
        }
    }
}
