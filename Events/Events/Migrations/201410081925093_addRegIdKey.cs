namespace Events.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRegIdKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GcmRegistrationIds",
                c => new
                    {
                        RegId = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GcmRegistrationIds", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GcmRegistrationIds", new[] { "UserId" });
            DropTable("dbo.GcmRegistrationIds");
        }
    }
}
