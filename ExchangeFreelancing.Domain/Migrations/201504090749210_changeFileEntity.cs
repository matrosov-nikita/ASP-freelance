namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFileEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "order_number", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "order_number", c => c.Int(nullable: false));
        }
    }
}
