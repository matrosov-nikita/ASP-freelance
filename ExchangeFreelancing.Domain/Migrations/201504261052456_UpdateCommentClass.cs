namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "mark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "mark");
        }
    }
}
