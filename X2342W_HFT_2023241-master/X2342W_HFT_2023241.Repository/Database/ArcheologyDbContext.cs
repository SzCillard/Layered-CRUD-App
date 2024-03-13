using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Numerics;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Repository
{
    public class ArcheologyDbContext : DbContext
    {
        public DbSet<Researcher> Researchers { get; set; }
        public DbSet<Excavation> Excavations { get; set; }
        public DbSet<ExcavationSite> ExcavationSites { get; set; }
        public DbSet<Settlement> Settlements { get; set; }

        public ArcheologyDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseInMemoryDatabase("excavation")
                    .UseLazyLoadingProxies();

			}
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Researcher>()
                .HasMany(x => x.Sites)
                .WithMany(x => x.Researchers)
                .UsingEntity<Excavation>(
                    x => x.HasOne(x => x.ExcavationSite)
                        .WithMany().HasForeignKey(x => x.SiteId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.Researcher)
                        .WithMany().HasForeignKey(x => x.ResearcherId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Excavation>()
               .HasOne(e => e.ExcavationSite)
               .WithMany(site => site.Excavations)
               .HasForeignKey(e => e.SiteId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Excavation>()
                .HasOne(e => e.Researcher)
                .WithMany(r => r.Excavations)
                .HasForeignKey(e => e.ResearcherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExcavationSite>(site => site
               .HasOne(site => site.Settlement)
               .WithMany(s => s.Sites)
               .HasForeignKey(site => site.SettlementId)
               .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Settlement>().HasData(new Settlement[]
            {
                new Settlement("1#Abaújszántó#Borsod-Abaúj-Zemplén"),
                new Settlement("2#Alsózsolca#Borsod-Abaúj-Zemplén"),
                new Settlement("3#Adács#Heves"),
				new Settlement("4#Csákvár#Fejér"),
				new Settlement("5#Szeged#Csongrád-Csanád"),


			});
            modelBuilder.Entity<ExcavationSite>().HasData(new ExcavationSite[]
            {
               new ExcavationSite("1#1#Shrine#Ancient"),
                new ExcavationSite("2#1#Tower#Early Medieval"),
                new ExcavationSite("3#3#Aqueduct#Post Medieval"),
                new ExcavationSite("4#2#Tunnel#Ancient"),
				new ExcavationSite("5#4#Fortress#Ancient"),
				new ExcavationSite("6#5#Castle#Mid Medieval"),
				new ExcavationSite("7#5#Cemetery#Ancient"),

			});
            modelBuilder.Entity<Excavation>().HasData(new Excavation[]
            {
               new Excavation("1#1#1#1966.03.17#1966.09.17"),
               new Excavation("2#2#2#1997.04.11#1997.10.11"),
               new Excavation("3#1#2#1989.09.23#1989.03.23"),
                new Excavation("4#1#3#1985.06.01#1985.12.01"),
                new Excavation("5#3#3#2000.11.23#2001.05.23"),
                new Excavation("6#4#4#2003.06.16#2003.12.16"),
                new Excavation("7#4#1#1993.12.21#1994.06.21"),
                new Excavation("8#4#2#2014.09.17#2015.03.17"),
                new Excavation("9#4#3#1978.05.24#1978.11.24"),
               new Excavation("10#2#3#1975.11.25#1976.05.25"),
			   new Excavation("11#5#5#1999.01.23#1999.07.01"),
			   new Excavation("12#5#3#1999.01.25#1999.07.01"),
			   new Excavation("13#6#4#2012.06.10#2013.01.30"),
			   new Excavation("14#7#5#2020.02.20#2020.11.17"),


			});
            modelBuilder.Entity<Researcher>().HasData(new Researcher[]
                {
                   new Researcher("1#Nagy Gábor#Archeologist"),
                    new Researcher("2#Kovács Eszter#Historian"),
                    new Researcher("3#Tóth Péter#Archeologist"),
                    new Researcher("4#Szabó Katalin#Archeologist"),
                    new Researcher("5#Macskásy Júlia#Anthropologist"),
				});
        }
    }
}
