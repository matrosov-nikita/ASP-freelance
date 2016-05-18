namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdasd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "mark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "mark", c => c.String());
        }
    }
}
