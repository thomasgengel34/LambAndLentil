namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PLAN.Meal", "Menu_ID", "MENU.Menu");
            DropForeignKey("PLAN.Meal", "Plan_ID", "PLAN.Plan");
            DropIndex("PLAN.Meal", new[] { "Menu_ID" });
            DropIndex("PLAN.Meal", new[] { "Plan_ID" });
            AddColumn("MENU.Menu", "Plan_ID", c => c.Int());
            CreateIndex("MENU.Menu", "Plan_ID");
            AddForeignKey("MENU.Menu", "Plan_ID", "PLAN.Plan", "ID");
            DropTable("PLAN.Meal");
        }
        
        public override void Down()
        {
            CreateTable(
                "PLAN.Meal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MealType = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreationDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        AddedByUser = c.String(),
                        ModifiedByUser = c.String(),
                        Menu_ID = c.Int(),
                        Plan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("MENU.Menu", "Plan_ID", "PLAN.Plan");
            DropIndex("MENU.Menu", new[] { "Plan_ID" });
            DropColumn("MENU.Menu", "Plan_ID");
            CreateIndex("PLAN.Meal", "Plan_ID");
            CreateIndex("PLAN.Meal", "Menu_ID");
            AddForeignKey("PLAN.Meal", "Plan_ID", "PLAN.Plan", "ID");
            AddForeignKey("PLAN.Meal", "Menu_ID", "MENU.Menu", "ID");
        }
    }
}
