namespace LambAndLentil.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        ShortDescription = c.String(maxLength: 50),
                        ServingSize = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        ServingSizeUnit = c.Int(nullable: false),
                        Calories = c.Short(),
                        CalFromFat = c.Short(),
                    })
                .PrimaryKey(t => t.IngredientID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingredients");
        }
    }
}
