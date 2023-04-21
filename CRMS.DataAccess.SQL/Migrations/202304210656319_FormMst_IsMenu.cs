namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormMst_IsMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormMsts", "IsMenu", c => c.Boolean(nullable: false));
            DropColumn("dbo.FormRoleMappings", "IsMenu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormRoleMappings", "IsMenu", c => c.Boolean(nullable: false));
            DropColumn("dbo.FormMsts", "IsMenu");
        }
    }
}
