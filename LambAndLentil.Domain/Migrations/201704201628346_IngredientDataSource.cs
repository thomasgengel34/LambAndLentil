namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientDataSource : DbMigration
    {
        public override void Up()
        {
            AddColumn("INGREDIENT.Ingredient", "DataSource", c => c.String());
            AddColumn("MENU.Menu", "DayOfWeek", c => c.Int(nullable: false));
            AddColumn("PLAN.Plan", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("PLAN.Plan", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("PLAN.Plan", "AddedByUser", c => c.String());
            AddColumn("PLAN.Plan", "ModifiedByUser", c => c.String());
            AlterColumn("PLAN.Plan", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("PLAN.Plan", "Name", c => c.String());
            DropColumn("PLAN.Plan", "ModifiedByUser");
            DropColumn("PLAN.Plan", "AddedByUser");
            DropColumn("PLAN.Plan", "ModifiedDate");
            DropColumn("PLAN.Plan", "CreationDate");
            DropColumn("MENU.Menu", "DayOfWeek");
            DropColumn("INGREDIENT.Ingredient", "DataSource");
        }
    }
}
