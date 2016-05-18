namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRequest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "Excecuter_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "Excecuter_Id", c => c.Int(nullable: false));
        }
    }
}
