namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddommentClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        order = c.Int(nullable: false),
                        executer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comments");
        }
    }
}
