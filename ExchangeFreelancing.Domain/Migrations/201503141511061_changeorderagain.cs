namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeorderagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Executer_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Executer_Id");
        }
    }
}
