namespace TestLaborator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carte",
                c => new
                    {
                        IDCarte = c.Int(nullable: false, identity: true),
                        Titlu = c.String(nullable: false, maxLength: 255),
                        Descriere = c.String(nullable: false, maxLength: 3000),
                        Editura = c.String(nullable: false, maxLength: 255),
                        Pret = c.Int(nullable: false),
                        Autor = c.String(nullable: false, maxLength: 255),
                        IDLibrarie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDCarte)
                .ForeignKey("dbo.Librarie", t => t.IDLibrarie, cascadeDelete: true)
                .Index(t => t.IDLibrarie);
            
            CreateTable(
                "dbo.Librarie",
                c => new
                    {
                        IDLibrarie = c.Int(nullable: false, identity: true),
                        Denumire = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.IDLibrarie);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carte", "IDLibrarie", "dbo.Librarie");
            DropIndex("dbo.Carte", new[] { "IDLibrarie" });
            DropTable("dbo.Librarie");
            DropTable("dbo.Carte");
        }
    }
}
