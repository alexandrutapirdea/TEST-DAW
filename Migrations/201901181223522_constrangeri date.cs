namespace TestLaborator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class constrangeridate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carte", "Titlu", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Carte", "Descriere", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Carte", "Editura", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Carte", "Autor", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Librarie", "Denumire", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Librarie", "Denumire", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Carte", "Autor", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Carte", "Editura", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Carte", "Descriere", c => c.String(nullable: false, maxLength: 3000));
            AlterColumn("dbo.Carte", "Titlu", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
