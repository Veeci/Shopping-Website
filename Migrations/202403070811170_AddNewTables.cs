namespace ShoppingWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.admin",
                c => new
                    {
                        adminid = c.Int(nullable: false),
                        username = c.String(maxLength: 50),
                        password = c.String(maxLength: 50),
                        full_name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.adminid);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        userid = c.Int(nullable: false),
                        username = c.String(maxLength: 50),
                        password = c.String(maxLength: 50),
                        email = c.String(maxLength: 100),
                        phone_number = c.String(maxLength: 20),
                        full_name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user");
            DropTable("dbo.admin");
        }
    }
}
