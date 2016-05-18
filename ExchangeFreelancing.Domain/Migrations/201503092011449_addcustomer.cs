namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Customer_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Customer_Id");
        }
    }
}
