namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Description", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Description", c => c.String(nullable: false, maxLength: 400));
        }
    }
}
