namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredForOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Description", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Description", c => c.String());
            AlterColumn("dbo.Orders", "Category", c => c.String());
        }
    }
}
