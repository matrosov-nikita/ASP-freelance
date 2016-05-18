namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcutomername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Custom_name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Custom_name");
        }
    }
}
