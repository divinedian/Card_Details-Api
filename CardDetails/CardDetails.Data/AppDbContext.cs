using CardDetails.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CardDetails.Data
{
    public class AppDbContext: DbContext
    {
        private readonly DbContextOptions _options;
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<CardDetail>(e => { e.HasOne(s => s.Country); e.HasOne(s => s.Number); });

            //builder.Entity<Bank>(e => e.HasNoKey().ToView(null));
            //builder.Entity<Bank>(e => e.HasKey(s => s.Name));
            //builder.Entity<CardDetail>(e => e.HasKey(s => s.Bin));
            //builder.Entity<Country>(e => e.HasKey(s => s.Name));
        }
        
        public DbSet<CardDetail> CardDetails { get; set; }

        public DbSet<Number> Numbers { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Bank> Banks { get; set; }

    }
}
