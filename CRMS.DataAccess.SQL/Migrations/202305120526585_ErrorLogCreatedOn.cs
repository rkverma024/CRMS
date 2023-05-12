namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorLogCreatedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLogs", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditLogs", "CreatedOn");
        }
    }
}
