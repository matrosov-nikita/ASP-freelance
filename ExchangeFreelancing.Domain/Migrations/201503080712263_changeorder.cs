namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "State", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Custom_Id", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Custom_Id");
            DropColumn("dbo.Orders", "State");
        }
    }
}
