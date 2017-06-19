namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceIngredient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("RECIPE.Recipe", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropIndex("RECIPE.Recipe", new[] { "Ingredient_ID" });
            DropColumn("INGREDIENT.Ingredient", "Maker");
            DropColumn("INGREDIENT.Ingredient", "Brand");
            DropColumn("INGREDIENT.Ingredient", "ServingSize");
            DropColumn("INGREDIENT.Ingredient", "ServingSizeUnit");
            DropColumn("INGREDIENT.Ingredient", "ServingsPerContainer");
            DropColumn("INGREDIENT.Ingredient", "ContainerSize");
            DropColumn("INGREDIENT.Ingredient", "ContainerSizeUnit");
            DropColumn("INGREDIENT.Ingredient", "ContainerSizeInGrams");
            DropColumn("INGREDIENT.Ingredient", "Calories");
            DropColumn("INGREDIENT.Ingredient", "CalFromFat");
            DropColumn("INGREDIENT.Ingredient", "TotalFat");
            DropColumn("INGREDIENT.Ingredient", "SaturatedFat");
            DropColumn("INGREDIENT.Ingredient", "TransFat");
            DropColumn("INGREDIENT.Ingredient", "PolyUnSaturatedFat");
            DropColumn("INGREDIENT.Ingredient", "MonoUnSaturatedFat");
            DropColumn("INGREDIENT.Ingredient", "Cholesterol");
            DropColumn("INGREDIENT.Ingredient", "Sodium");
            DropColumn("INGREDIENT.Ingredient", "TotalCarbohydrates");
            DropColumn("INGREDIENT.Ingredient", "Protein");
            DropColumn("INGREDIENT.Ingredient", "Potassium");
            DropColumn("INGREDIENT.Ingredient", "DietaryFiber");
            DropColumn("INGREDIENT.Ingredient", "Sugars");
            DropColumn("INGREDIENT.Ingredient", "VitaminA");
            DropColumn("INGREDIENT.Ingredient", "VitaminC");
            DropColumn("INGREDIENT.Ingredient", "Calcium");
            DropColumn("INGREDIENT.Ingredient", "Iron");
            DropColumn("INGREDIENT.Ingredient", "FolicAcid");
            DropColumn("INGREDIENT.Ingredient", "Egg");
            DropColumn("INGREDIENT.Ingredient", "Nuts");
            DropColumn("INGREDIENT.Ingredient", "Milk");
            DropColumn("INGREDIENT.Ingredient", "Wheat");
            DropColumn("INGREDIENT.Ingredient", "Soy");
            DropColumn("INGREDIENT.Ingredient", "Category");
            DropColumn("INGREDIENT.Ingredient", "Corn");
            DropColumn("INGREDIENT.Ingredient", "Onion");
            DropColumn("INGREDIENT.Ingredient", "Garlic");
            DropColumn("INGREDIENT.Ingredient", "SodiumNitrite");
            DropColumn("INGREDIENT.Ingredient", "UPC");
            DropColumn("INGREDIENT.Ingredient", "Caffeine");
            DropColumn("INGREDIENT.Ingredient", "FoodGroup");
            DropColumn("INGREDIENT.Ingredient", "StorageType");
            DropColumn("INGREDIENT.Ingredient", "IsGMO");
            DropColumn("INGREDIENT.Ingredient", "CountryOfOrigin");
            DropColumn("INGREDIENT.Ingredient", "Kosher");
            DropColumn("INGREDIENT.Ingredient", "DataSource");
            DropColumn("INGREDIENT.Ingredient", "Fish");
            DropColumn("RECIPE.Recipe", "Ingredient_ID");
        }
        
        public override void Down()
        {
            AddColumn("RECIPE.Recipe", "Ingredient_ID", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Fish", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "DataSource", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Kosher", c => c.Int(nullable: false));
            AddColumn("INGREDIENT.Ingredient", "CountryOfOrigin", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "IsGMO", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "StorageType", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "FoodGroup", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Caffeine", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "UPC", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "SodiumNitrite", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Garlic", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Onion", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Corn", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Category", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Soy", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Wheat", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Milk", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Nuts", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Egg", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "FolicAcid", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Iron", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Calcium", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "VitaminC", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "VitaminA", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Sugars", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "DietaryFiber", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "Potassium", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "Protein", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "TotalCarbohydrates", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "Sodium", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "Cholesterol", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "MonoUnSaturatedFat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "PolyUnSaturatedFat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "TransFat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "SaturatedFat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "TotalFat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "CalFromFat", c => c.Short());
            AddColumn("INGREDIENT.Ingredient", "Calories", c => c.Short());
            AddColumn("INGREDIENT.Ingredient", "ContainerSizeInGrams", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "ContainerSizeUnit", c => c.Int(nullable: false));
            AddColumn("INGREDIENT.Ingredient", "ContainerSize", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "ServingsPerContainer", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "ServingSizeUnit", c => c.Int(nullable: false));
            AddColumn("INGREDIENT.Ingredient", "ServingSize", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("INGREDIENT.Ingredient", "Brand", c => c.String());
            AddColumn("INGREDIENT.Ingredient", "Maker", c => c.String());
            CreateIndex("RECIPE.Recipe", "Ingredient_ID");
            AddForeignKey("RECIPE.Recipe", "Ingredient_ID", "INGREDIENT.Ingredient", "ID");
        }
    }
}
