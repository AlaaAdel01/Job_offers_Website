namespace JopOffere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyJobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jops", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.JobId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jops");
            DropIndex("dbo.ApplyForJobs", new[] { "userId" });
            DropIndex("dbo.ApplyForJobs", new[] { "JobId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
