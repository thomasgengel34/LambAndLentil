namespace LambAndLentil.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removePersonMustHaveGarlic : DbMigration
    {
        public override void Up()
        {
            DropColumn("PERSON.Person", "MustHaveGarlic");
        }
        
        public override void Down()
        {
            AddColumn("PERSON.Person", "MustHaveGarlic", c => c.Boolean(nullable: false));
        }
    }
}
