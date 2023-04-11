namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormMstData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormMsts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        NavigateURL = c.String(),
                        ParentFormId = c.Guid(nullable: false),
                        FormAccessCode = c.String(),
                        DisplayIndex = c.String(),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormMsts");
        }
    }
}