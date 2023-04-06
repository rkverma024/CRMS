namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormMstDataPINUll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FormMsts", "ParentFormId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FormMsts", "ParentFormId", c => c.Guid(nullable: false));
        }
    }
}
