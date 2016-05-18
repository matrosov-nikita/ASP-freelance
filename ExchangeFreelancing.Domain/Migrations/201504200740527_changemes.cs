namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        message_id = c.Int(nullable: false, identity: true),
                        author = c.String(nullable: false),
                        message = c.String(nullable: false),
                        order_number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.message_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
