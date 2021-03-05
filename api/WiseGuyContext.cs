using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Linq;

namespace wiseguy {
    class WiseGuyContext : DbContext {
        public DbSet<Answer> Answers {get;set;}
        public DbSet<SheetCopy> Copies {get;set;}
        public DbSet<SheetTemplate> Templates {get;set;}
        public DbSet<SheetIssue> Issues {get;set;}
        public DbSet<Phrase> Phrases {get;set;}
        public DbSet<Maillist> Maillists {get;set;}
        public DbSet<Participant> Participants {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Startup.CurrentConfiguration.GetConnectionString("localDatabase"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>().HasKey(p => new {p.Email});
            modelBuilder.Entity<Phrase>()
                .HasOne(p=>p.SheetTemplate)
                .WithMany(s=>s.Phrases)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SheetCopy>()
                .HasOne(p=>p.SheetTemplate)
                .WithMany(s=>s.Copies)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}