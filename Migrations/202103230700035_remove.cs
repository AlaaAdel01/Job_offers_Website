namespace JopOffere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jops", "JopImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jops", "JopImage", c => c.String(nullable: false));
        }
    }
}
