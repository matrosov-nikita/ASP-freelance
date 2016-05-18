namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hangeComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "DateAdd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "DateAdd");
        }
    }
}
