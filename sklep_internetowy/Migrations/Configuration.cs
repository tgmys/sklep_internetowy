namespace sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using sklep_internetowy.DAL;
    public sealed class Configuration : DbMigrationsConfiguration<sklep_internetowy.DAL.KursyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "sklep_internetowy.DAL.KursyContext";
        }

        protected override void Seed(sklep_internetowy.DAL.KursyContext context)
        {
            KursyInitializer.SeedKursyData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
