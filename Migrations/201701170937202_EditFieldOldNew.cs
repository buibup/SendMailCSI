namespace SendEmailCSI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditFieldOldNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendMails", "OldNew", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendMails", "OldNew");
        }
    }
}
