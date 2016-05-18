namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadasdsad : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Message", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Message", c => c.String(nullable: false, maxLength: 4000));
        }
    }
}
