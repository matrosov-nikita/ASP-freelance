namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auxOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Message", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Message");
        }
    }
}
