namespace SendEmailCSI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendMails", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendMails", "Link");
        }
    }
}
