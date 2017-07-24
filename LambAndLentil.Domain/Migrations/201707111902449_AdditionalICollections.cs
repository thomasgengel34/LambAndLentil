namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalICollections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("INGREDIENT.Ingredient", "Menu_ID", "MENU.Menu");
            DropForeignKey("RECIPE.Recipe", "Menu_ID", "MENU.Menu");
            DropForeignKey("MENU.Menu", "Plan_ID", "PLAN.Plan");
            DropForeignKey("INGREDIENT.Ingredient", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("INGREDIENT.Ingredient", "Recipe_ID", "RECIPE.Recipe");
            DropIndex("INGREDIENT.Ingredient", new[] { "Menu_ID" });
            DropIndex("INGREDIENT.Ingredient", new[] { "ShoppingList_ID" });
            DropIndex("RECIPE.Recipe", new[] { "Menu_ID" });
            DropIndex("MENU.Menu", new[] { "Plan_ID" });
            CreateTable(
                "dbo.MenuIngredients",
                c => new
                    {
                        Menu_ID = c.Int(nullable: false),
                        Ingredient_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_ID, t.Ingredient_ID })
                .ForeignKey("MENU.Menu", t => t.Menu_ID, cascadeDelete: true)
                .ForeignKey("INGREDIENT.Ingredient", t => t.Ingredient_ID, cascadeDelete: true)
                .Index(t => t.Menu_ID)
                .Index(t => t.Ingredient_ID);
            
            CreateTable(
                "dbo.PersonIngredients",
                c => new
                    {
                        Person_ID = c.Int(nullable: false),
                        Ingredient_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_ID, t.Ingredient_ID })
                .ForeignKey("PERSON.Person", t => t.Person_ID, cascadeDelete: true)
                .ForeignKey("INGREDIENT.Ingredient", t => t.Ingredient_ID, cascadeDelete: true)
                .Index(t => t.Person_ID)
                .Index(t => t.Ingredient_ID);
            
            CreateTable(
                "dbo.PersonMenus",
                c => new
                    {
                        Person_ID = c.Int(nullable: false),
                        Menu_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_ID, t.Menu_ID })
                .ForeignKey("PERSON.Person", t => t.Person_ID, cascadeDelete: true)
                .ForeignKey("MENU.Menu", t => t.Menu_ID, cascadeDelete: true)
                .Index(t => t.Person_ID)
                .Index(t => t.Menu_ID);
            
            CreateTable(
                "dbo.PlanIngredients",
                c => new
                    {
                        Plan_ID = c.Int(nullable: false),
                        Ingredient_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Plan_ID, t.Ingredient_ID })
                .ForeignKey("PLAN.Plan", t => t.Plan_ID, cascadeDelete: true)
                .ForeignKey("INGREDIENT.Ingredient", t => t.Ingredient_ID, cascadeDelete: true)
                .Index(t => t.Plan_ID)
                .Index(t => t.Ingredient_ID);
            
            CreateTable(
                "dbo.PlanMenus",
                c => new
                    {
                        Plan_ID = c.Int(nullable: false),
                        Menu_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Plan_ID, t.Menu_ID })
                .ForeignKey("PLAN.Plan", t => t.Plan_ID, cascadeDelete: true)
                .ForeignKey("MENU.Menu", t => t.Menu_ID, cascadeDelete: true)
                .Index(t => t.Plan_ID)
                .Index(t => t.Menu_ID);
            
            CreateTable(
                "dbo.PlanPersons",
                c => new
                    {
                        Plan_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Plan_ID, t.Person_ID })
                .ForeignKey("PLAN.Plan", t => t.Plan_ID, cascadeDelete: true)
                .ForeignKey("PERSON.Person", t => t.Person_ID, cascadeDelete: true)
                .Index(t => t.Plan_ID)
                .Index(t => t.Person_ID);
            
            CreateTable(
                "dbo.RecipeMenus",
                c => new
                    {
                        Recipe_ID = c.Int(nullable: false),
                        Menu_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_ID, t.Menu_ID })
                .ForeignKey("RECIPE.Recipe", t => t.Recipe_ID, cascadeDelete: true)
                .ForeignKey("MENU.Menu", t => t.Menu_ID, cascadeDelete: true)
                .Index(t => t.Recipe_ID)
                .Index(t => t.Menu_ID);
            
            CreateTable(
                "dbo.RecipePersons",
                c => new
                    {
                        Recipe_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_ID, t.Person_ID })
                .ForeignKey("RECIPE.Recipe", t => t.Recipe_ID, cascadeDelete: true)
                .ForeignKey("PERSON.Person", t => t.Person_ID, cascadeDelete: true)
                .Index(t => t.Recipe_ID)
                .Index(t => t.Person_ID);
            
            CreateTable(
                "dbo.RecipePlans",
                c => new
                    {
                        Recipe_ID = c.Int(nullable: false),
                        Plan_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_ID, t.Plan_ID })
                .ForeignKey("RECIPE.Recipe", t => t.Recipe_ID, cascadeDelete: true)
                .ForeignKey("PLAN.Plan", t => t.Plan_ID, cascadeDelete: true)
                .Index(t => t.Recipe_ID)
                .Index(t => t.Plan_ID);
            
            CreateTable(
                "dbo.ShoppingListIngredients",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        Ingredient_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.Ingredient_ID })
                .ForeignKey("SHOPPINGLIST.ShoppingList", t => t.ShoppingList_ID, cascadeDelete: true)
                .ForeignKey("INGREDIENT.Ingredient", t => t.Ingredient_ID, cascadeDelete: true)
                .Index(t => t.ShoppingList_ID)
                .Index(t => t.Ingredient_ID);
            
            CreateTable(
                "dbo.ShoppingListMenus",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        Menu_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.Menu_ID })
                .ForeignKey("SHOPPINGLIST.ShoppingList", t => t.ShoppingList_ID, cascadeDelete: true)
                .ForeignKey("MENU.Menu", t => t.Menu_ID, cascadeDelete: true)
                .Index(t => t.ShoppingList_ID)
                .Index(t => t.Menu_ID);
            
            CreateTable(
                "dbo.ShoppingListPersons",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        Person_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.Person_ID })
                .ForeignKey("SHOPPINGLIST.ShoppingList", t => t.ShoppingList_ID, cascadeDelete: true)
                .ForeignKey("PERSON.Person", t => t.Person_ID, cascadeDelete: true)
                .Index(t => t.ShoppingList_ID)
                .Index(t => t.Person_ID);
            
            CreateTable(
                "dbo.ShoppingListPlans",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        Plan_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.Plan_ID })
                .ForeignKey("SHOPPINGLIST.ShoppingList", t => t.ShoppingList_ID, cascadeDelete: true)
                .ForeignKey("PLAN.Plan", t => t.Plan_ID, cascadeDelete: true)
                .Index(t => t.ShoppingList_ID)
                .Index(t => t.Plan_ID);
            
            CreateTable(
                "dbo.ShoppingListRecipes",
                c => new
                    {
                        ShoppingList_ID = c.Int(nullable: false),
                        Recipe_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingList_ID, t.Recipe_ID })
                .ForeignKey("SHOPPINGLIST.ShoppingList", t => t.ShoppingList_ID, cascadeDelete: true)
                .ForeignKey("RECIPE.Recipe", t => t.Recipe_ID, cascadeDelete: true)
                .Index(t => t.ShoppingList_ID)
                .Index(t => t.Recipe_ID);
            
            AddColumn("INGREDIENT.Ingredient", "Ingredient_ID", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Recipe_ID1", c => c.Int());
            AddColumn("RECIPE.Recipe", "Recipe_ID", c => c.Int());
            AddColumn("RECIPE.Recipe", "Ingredient_ID", c => c.Int());
            AddColumn("MENU.Menu", "Menu_ID", c => c.Int());
            AddColumn("PERSON.Person", "Person_ID", c => c.Int());
            AddColumn("PLAN.Plan", "Plan_ID", c => c.Int());
            AddColumn("SHOPPINGLIST.ShoppingList", "ShoppingList_ID", c => c.Int());
            CreateIndex("INGREDIENT.Ingredient", "Ingredient_ID");
            CreateIndex("INGREDIENT.Ingredient", "Recipe_ID1");
            CreateIndex("MENU.Menu", "Menu_ID");
            CreateIndex("PERSON.Person", "Person_ID");
            CreateIndex("PLAN.Plan", "Plan_ID");
            CreateIndex("RECIPE.Recipe", "Recipe_ID");
            CreateIndex("RECIPE.Recipe", "Ingredient_ID");
            CreateIndex("SHOPPINGLIST.ShoppingList", "ShoppingList_ID");
            AddForeignKey("INGREDIENT.Ingredient", "Ingredient_ID", "INGREDIENT.Ingredient", "ID");
            AddForeignKey("MENU.Menu", "Menu_ID", "MENU.Menu", "ID");
            AddForeignKey("PERSON.Person", "Person_ID", "PERSON.Person", "ID");
            AddForeignKey("PLAN.Plan", "Plan_ID", "PLAN.Plan", "ID");
            AddForeignKey("RECIPE.Recipe", "Recipe_ID", "RECIPE.Recipe", "ID");
            AddForeignKey("SHOPPINGLIST.ShoppingList", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList", "ID");
            AddForeignKey("RECIPE.Recipe", "Ingredient_ID", "INGREDIENT.Ingredient", "ID");
            AddForeignKey("INGREDIENT.Ingredient", "Recipe_ID1", "RECIPE.Recipe", "ID");
            DropColumn("INGREDIENT.Ingredient", "Menu_ID");
            DropColumn("INGREDIENT.Ingredient", "ShoppingList_ID");
            DropColumn("RECIPE.Recipe", "Menu_ID");
            DropColumn("MENU.Menu", "Plan_ID");
        }
        
        public override void Down()
        {
            AddColumn("MENU.Menu", "Plan_ID", c => c.Int());
            AddColumn("RECIPE.Recipe", "Menu_ID", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "ShoppingList_ID", c => c.Int());
            AddColumn("INGREDIENT.Ingredient", "Menu_ID", c => c.Int());
            DropForeignKey("INGREDIENT.Ingredient", "Recipe_ID1", "RECIPE.Recipe");
            DropForeignKey("RECIPE.Recipe", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropForeignKey("SHOPPINGLIST.ShoppingList", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("dbo.ShoppingListRecipes", "Recipe_ID", "RECIPE.Recipe");
            DropForeignKey("dbo.ShoppingListRecipes", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("dbo.ShoppingListPlans", "Plan_ID", "PLAN.Plan");
            DropForeignKey("dbo.ShoppingListPlans", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("dbo.ShoppingListPersons", "Person_ID", "PERSON.Person");
            DropForeignKey("dbo.ShoppingListPersons", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("dbo.ShoppingListMenus", "Menu_ID", "MENU.Menu");
            DropForeignKey("dbo.ShoppingListMenus", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("dbo.ShoppingListIngredients", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropForeignKey("dbo.ShoppingListIngredients", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList");
            DropForeignKey("RECIPE.Recipe", "Recipe_ID", "RECIPE.Recipe");
            DropForeignKey("dbo.RecipePlans", "Plan_ID", "PLAN.Plan");
            DropForeignKey("dbo.RecipePlans", "Recipe_ID", "RECIPE.Recipe");
            DropForeignKey("dbo.RecipePersons", "Person_ID", "PERSON.Person");
            DropForeignKey("dbo.RecipePersons", "Recipe_ID", "RECIPE.Recipe");
            DropForeignKey("dbo.RecipeMenus", "Menu_ID", "MENU.Menu");
            DropForeignKey("dbo.RecipeMenus", "Recipe_ID", "RECIPE.Recipe");
            DropForeignKey("PLAN.Plan", "Plan_ID", "PLAN.Plan");
            DropForeignKey("dbo.PlanPersons", "Person_ID", "PERSON.Person");
            DropForeignKey("dbo.PlanPersons", "Plan_ID", "PLAN.Plan");
            DropForeignKey("dbo.PlanMenus", "Menu_ID", "MENU.Menu");
            DropForeignKey("dbo.PlanMenus", "Plan_ID", "PLAN.Plan");
            DropForeignKey("dbo.PlanIngredients", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropForeignKey("dbo.PlanIngredients", "Plan_ID", "PLAN.Plan");
            DropForeignKey("PERSON.Person", "Person_ID", "PERSON.Person");
            DropForeignKey("dbo.PersonMenus", "Menu_ID", "MENU.Menu");
            DropForeignKey("dbo.PersonMenus", "Person_ID", "PERSON.Person");
            DropForeignKey("dbo.PersonIngredients", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropForeignKey("dbo.PersonIngredients", "Person_ID", "PERSON.Person");
            DropForeignKey("MENU.Menu", "Menu_ID", "MENU.Menu");
            DropForeignKey("dbo.MenuIngredients", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropForeignKey("dbo.MenuIngredients", "Menu_ID", "MENU.Menu");
            DropForeignKey("INGREDIENT.Ingredient", "Ingredient_ID", "INGREDIENT.Ingredient");
            DropIndex("dbo.ShoppingListRecipes", new[] { "Recipe_ID" });
            DropIndex("dbo.ShoppingListRecipes", new[] { "ShoppingList_ID" });
            DropIndex("dbo.ShoppingListPlans", new[] { "Plan_ID" });
            DropIndex("dbo.ShoppingListPlans", new[] { "ShoppingList_ID" });
            DropIndex("dbo.ShoppingListPersons", new[] { "Person_ID" });
            DropIndex("dbo.ShoppingListPersons", new[] { "ShoppingList_ID" });
            DropIndex("dbo.ShoppingListMenus", new[] { "Menu_ID" });
            DropIndex("dbo.ShoppingListMenus", new[] { "ShoppingList_ID" });
            DropIndex("dbo.ShoppingListIngredients", new[] { "Ingredient_ID" });
            DropIndex("dbo.ShoppingListIngredients", new[] { "ShoppingList_ID" });
            DropIndex("dbo.RecipePlans", new[] { "Plan_ID" });
            DropIndex("dbo.RecipePlans", new[] { "Recipe_ID" });
            DropIndex("dbo.RecipePersons", new[] { "Person_ID" });
            DropIndex("dbo.RecipePersons", new[] { "Recipe_ID" });
            DropIndex("dbo.RecipeMenus", new[] { "Menu_ID" });
            DropIndex("dbo.RecipeMenus", new[] { "Recipe_ID" });
            DropIndex("dbo.PlanPersons", new[] { "Person_ID" });
            DropIndex("dbo.PlanPersons", new[] { "Plan_ID" });
            DropIndex("dbo.PlanMenus", new[] { "Menu_ID" });
            DropIndex("dbo.PlanMenus", new[] { "Plan_ID" });
            DropIndex("dbo.PlanIngredients", new[] { "Ingredient_ID" });
            DropIndex("dbo.PlanIngredients", new[] { "Plan_ID" });
            DropIndex("dbo.PersonMenus", new[] { "Menu_ID" });
            DropIndex("dbo.PersonMenus", new[] { "Person_ID" });
            DropIndex("dbo.PersonIngredients", new[] { "Ingredient_ID" });
            DropIndex("dbo.PersonIngredients", new[] { "Person_ID" });
            DropIndex("dbo.MenuIngredients", new[] { "Ingredient_ID" });
            DropIndex("dbo.MenuIngredients", new[] { "Menu_ID" });
            DropIndex("SHOPPINGLIST.ShoppingList", new[] { "ShoppingList_ID" });
            DropIndex("RECIPE.Recipe", new[] { "Ingredient_ID" });
            DropIndex("RECIPE.Recipe", new[] { "Recipe_ID" });
            DropIndex("PLAN.Plan", new[] { "Plan_ID" });
            DropIndex("PERSON.Person", new[] { "Person_ID" });
            DropIndex("MENU.Menu", new[] { "Menu_ID" });
            DropIndex("INGREDIENT.Ingredient", new[] { "Recipe_ID1" });
            DropIndex("INGREDIENT.Ingredient", new[] { "Ingredient_ID" });
            DropColumn("SHOPPINGLIST.ShoppingList", "ShoppingList_ID");
            DropColumn("PLAN.Plan", "Plan_ID");
            DropColumn("PERSON.Person", "Person_ID");
            DropColumn("MENU.Menu", "Menu_ID");
            DropColumn("RECIPE.Recipe", "Ingredient_ID");
            DropColumn("RECIPE.Recipe", "Recipe_ID");
            DropColumn("INGREDIENT.Ingredient", "Recipe_ID1");
            DropColumn("INGREDIENT.Ingredient", "Ingredient_ID");
            DropTable("dbo.ShoppingListRecipes");
            DropTable("dbo.ShoppingListPlans");
            DropTable("dbo.ShoppingListPersons");
            DropTable("dbo.ShoppingListMenus");
            DropTable("dbo.ShoppingListIngredients");
            DropTable("dbo.RecipePlans");
            DropTable("dbo.RecipePersons");
            DropTable("dbo.RecipeMenus");
            DropTable("dbo.PlanPersons");
            DropTable("dbo.PlanMenus");
            DropTable("dbo.PlanIngredients");
            DropTable("dbo.PersonMenus");
            DropTable("dbo.PersonIngredients");
            DropTable("dbo.MenuIngredients");
            CreateIndex("MENU.Menu", "Plan_ID");
            CreateIndex("RECIPE.Recipe", "Menu_ID");
            CreateIndex("INGREDIENT.Ingredient", "ShoppingList_ID");
            CreateIndex("INGREDIENT.Ingredient", "Menu_ID");
            AddForeignKey("INGREDIENT.Ingredient", "Recipe_ID", "RECIPE.Recipe", "ID");
            AddForeignKey("INGREDIENT.Ingredient", "ShoppingList_ID", "SHOPPINGLIST.ShoppingList", "ID");
            AddForeignKey("MENU.Menu", "Plan_ID", "PLAN.Plan", "ID");
            AddForeignKey("RECIPE.Recipe", "Menu_ID", "MENU.Menu", "ID");
            AddForeignKey("INGREDIENT.Ingredient", "Menu_ID", "MENU.Menu", "ID");
        }
    }
}
