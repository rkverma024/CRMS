namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormRoleMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormRoleMappings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FormId = c.Guid(),
                        RoleId = c.Guid(),
                        AllowInsert = c.Boolean(nullable: false),
                        AllowEdit = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        AllowView = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormRoleMappings");
        }
    }
}
