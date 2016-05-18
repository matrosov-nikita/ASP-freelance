namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changefieldexecutingtime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ExecutingTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ExecutingTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
