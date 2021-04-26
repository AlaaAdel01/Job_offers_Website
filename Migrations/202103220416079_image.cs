namespace JopOffere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "JopImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jops", "JopImage");
        }
    }
}
