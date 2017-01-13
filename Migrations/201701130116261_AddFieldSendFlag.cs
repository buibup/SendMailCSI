namespace SendEmailCSI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldSendFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendMails", "SendFlag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendMails", "SendFlag");
        }
    }
}
