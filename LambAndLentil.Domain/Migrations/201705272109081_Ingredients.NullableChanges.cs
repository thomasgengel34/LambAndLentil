namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientsNullableChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("INGREDIENT.Ingredient", "ContainerSizeUnit", c => c.Int(nullable: false));
            AlterColumn("INGREDIENT.Ingredient", "TotalFat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "SaturatedFat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "TransFat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "PolyUnSaturatedFat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "MonoUnSaturatedFat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Cholesterol", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Sodium", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "TotalCarbohydrates", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Protein", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Potassium", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "DietaryFiber", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Sugars", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "VitaminA", c => c.Int());
            AlterColumn("INGREDIENT.Ingredient", "VitaminC", c => c.Int());
            AlterColumn("INGREDIENT.Ingredient", "Calcium", c => c.Int());
            AlterColumn("INGREDIENT.Ingredient", "Iron", c => c.Int());
            AlterColumn("INGREDIENT.Ingredient", "FolicAcid", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("INGREDIENT.Ingredient", "FolicAcid", c => c.Int(nullable: false));
            AlterColumn("INGREDIENT.Ingredient", "Iron", c => c.Int(nullable: false));
            AlterColumn("INGREDIENT.Ingredient", "Calcium", c => c.Int(nullable: false));
            AlterColumn("INGREDIENT.Ingredient", "VitaminC", c => c.Int(nullable: false));
            AlterColumn("INGREDIENT.Ingredient", "VitaminA", c => c.Int(nullable: false));
            AlterColumn("INGREDIENT.Ingredient", "Sugars", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "DietaryFiber", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Potassium", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Protein", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "TotalCarbohydrates", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Sodium", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "Cholesterol", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "MonoUnSaturatedFat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "PolyUnSaturatedFat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "TransFat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "SaturatedFat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "TotalFat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("INGREDIENT.Ingredient", "ContainerSizeUnit", c => c.Int());
        }
    }
}
