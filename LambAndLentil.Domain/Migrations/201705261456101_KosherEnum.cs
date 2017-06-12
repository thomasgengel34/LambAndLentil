namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KosherEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("INGREDIENT.Ingredient", "Kosher", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("INGREDIENT.Ingredient", "Kosher", c => c.Boolean(nullable: false));
        }
    }
}
