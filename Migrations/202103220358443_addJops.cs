namespace JopOffere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJops : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JopTitle = c.String(nullable: false),
                        JopDescription = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jops", new[] { "CategoryId" });
            DropTable("dbo.Jops");
        }
    }
}
