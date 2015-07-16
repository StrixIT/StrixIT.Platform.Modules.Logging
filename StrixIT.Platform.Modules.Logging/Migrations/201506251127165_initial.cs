//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StrixIT.Platform.Modules.Logging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyticsLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        UserName = c.String(maxLength: 250),
                        LogType = c.String(nullable: false, maxLength: 50),
                        LogDateTime = c.DateTime(nullable: false),
                        LogData = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuditLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        UserName = c.String(maxLength: 250),
                        LogType = c.String(nullable: false, maxLength: 50),
                        LogDateTime = c.DateTime(nullable: false),
                        Message = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExceptionType = c.String(maxLength: 100),
                        ApplicationId = c.Guid(nullable: false),
                        UserEmail = c.String(maxLength: 250),
                        LogDateTime = c.DateTime(nullable: false),
                        Level = c.String(nullable: false, maxLength: 10),
                        Message = c.String(nullable: false, maxLength: 500),
                        Exception = c.String(maxLength: 4000),
                        IPAddress = c.String(maxLength: 40),
                        Url = c.String(maxLength: 250),
                        UserAgent = c.String(maxLength: 100),
                        Method = c.String(maxLength: 10),
                        ContentType = c.String(maxLength: 50),
                        Headers = c.String(maxLength: 1000),
                        Cookies = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorLog");
            DropTable("dbo.AuditLog");
            DropTable("dbo.AnalyticsLog");
        }
    }
}
