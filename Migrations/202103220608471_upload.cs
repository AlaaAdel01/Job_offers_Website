namespace JopOffere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upload : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jops", "JopImage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jops", "JopImage", c => c.String());
        }
    }
}
