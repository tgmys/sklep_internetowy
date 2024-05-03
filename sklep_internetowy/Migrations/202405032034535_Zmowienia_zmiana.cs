namespace sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zmowienia_zmiana : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zamowienie", "UserId", c => c.String());
            AddColumn("dbo.Zamowienie", "Adres", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy", c => c.String());
            AlterColumn("dbo.Zamowienie", "Telefon", c => c.String(nullable: false));
            AlterColumn("dbo.Zamowienie", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Zamowienie", "Ulica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zamowienie", "Ulica", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Zamowienie", "Email", c => c.String());
            AlterColumn("dbo.Zamowienie", "Telefon", c => c.String());
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy");
            DropColumn("dbo.Zamowienie", "Adres");
            DropColumn("dbo.Zamowienie", "UserId");
        }
    }
}
