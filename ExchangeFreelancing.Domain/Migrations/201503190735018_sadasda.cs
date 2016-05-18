namespace ExchangeFreelancing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadasda : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Files");
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        File_Id = c.Int(nullable: false, identity: true),
                        path = c.String(nullable: false),
                        order_number = c.Int(nullable: false),
                        extension = c.String(),
                    })
                .PrimaryKey(t => t.File_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Files");
        }
    }
}
