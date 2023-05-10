namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientAddress = c.String(),
                        BrowserInfo = c.String(),
                        HttpMethod = c.String(),
                        Url = c.String(),
                        Exception = c.String(),
                        HttpStatusCode = c.Int(nullable: false),
                        Comments = c.String(),
                        Parameters = c.String(),
                        Headres = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}
