namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormRoleMapping_IsMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormRoleMappings", "IsMenu", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormRoleMappings", "IsMenu");
        }
    }
}
