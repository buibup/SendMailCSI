namespace SendEmailCSI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableSettingMailAndTableLanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguagesId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.LanguagesId);
            
            CreateTable(
                "dbo.SettingMails",
                c => new
                    {
                        LanguagesId = c.Int(nullable: false),
                        Subject = c.String(maxLength: 250),
                        MailFrom = c.String(maxLength: 250),
                        MailBody = c.String(),
                        SmtpClient = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.LanguagesId)
                .ForeignKey("dbo.Languages", t => t.LanguagesId)
                .Index(t => t.LanguagesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SettingMails", "LanguagesId", "dbo.Languages");
            DropIndex("dbo.SettingMails", new[] { "LanguagesId" });
            DropTable("dbo.SettingMails");
            DropTable("dbo.Languages");
        }
    }
}
