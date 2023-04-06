namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormMstDataBase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FormMsts", "DisplayIndex", c => c.Int());
            DropColumn("dbo.FormMsts", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormMsts", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.FormMsts", "DisplayIndex", c => c.String());
        }
    }
}
