namespace sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zmiana_nazwy_kolumny : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zamowienie", "StanZamowienia", c => c.Int(nullable: false));
            DropColumn("dbo.Zamowienie", "StenZamowienia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zamowienie", "StenZamowienia", c => c.Int(nullable: false));
            DropColumn("dbo.Zamowienie", "StanZamowienia");
        }
    }
}
