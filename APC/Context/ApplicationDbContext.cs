using Microsoft.EntityFrameworkCore;
using APC.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<Senior> Seniors { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Repair> Repair { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=acpdata;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>()
                .Property(c => c.ID).UseIdentityColumn();
            modelBuilder.Entity<Drivers>()
                .Property(d => d.ID).UseIdentityColumn();
            modelBuilder.Entity<Senior>()
                .Property(s => s.ID).UseIdentityColumn();
            modelBuilder.Entity<Departure>()
                .Property(d => d.ID).UseIdentityColumn();
            modelBuilder.Entity<Repair>()
                .Property(r => r.ID).UseIdentityColumn();
            base.OnModelCreating(modelBuilder);
        }
    }
}
