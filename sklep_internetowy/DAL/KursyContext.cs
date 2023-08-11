using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using sklep_internetowy.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace sklep_internetowy.DAL
{
    public class KursyContext : DbContext
    {
        public KursyContext() : base("KursyContext")
        {

        }
        public DbSet<Kurs> Kursy { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowienia { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /* System.Data.Entity.ModelConfiguration.Conventions;
               wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabeli
              Zamiast Kategorie zostało by stworzone Kategories
             */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}