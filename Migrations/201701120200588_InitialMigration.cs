namespace SendEmailCSI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendMails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adm = c.String(maxLength: 15),
                        AdmNo = c.String(maxLength: 15),
                        PapmiNo = c.String(maxLength: 15),
                        NationCode = c.String(maxLength: 5),
                        NationDESC = c.String(),
                        EMail = c.String(),
                        PAADMDischgDate = c.DateTime(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SendMails");
        }
    }
}
