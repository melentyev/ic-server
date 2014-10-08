namespace Events.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "UserId", c => c.String());
        }
    }
}
