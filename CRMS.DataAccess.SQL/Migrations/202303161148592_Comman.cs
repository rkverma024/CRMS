namespace CRMS.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comman : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommanLookUps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ConfigName = c.String(),
                        ConfigKey = c.String(),
                        ConfigValue = c.String(),
                        DisplayOrder = c.Int(),
                        Description = c.String(),
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
            DropTable("dbo.CommanLookUps");
        }
    }
}
