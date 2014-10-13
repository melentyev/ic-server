namespace Events.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscriptions1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscribtionId = c.Int(nullable: false, identity: true),
                        Subscriber = c.Int(nullable: false),
                        SubscribedTo = c.Int(nullable: false),
                        Relationship = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubscribtionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscriptions");
        }
    }
}
