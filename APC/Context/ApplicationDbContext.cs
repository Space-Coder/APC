using Microsoft.EntityFrameworkCore;
using APC.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace APC.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Repair> Repair { get; set; }

        public ApplicationDbContext()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=acpdata;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cars>()
                .Property(c => c.ID)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            modelBuilder.Entity<Cars>()
                .HasIndex(c => c.Number).IsUnique();
            modelBuilder.Entity<Departure>()
                .Property(d => d.ID).UseIdentityColumn();
            modelBuilder.Entity<Repair>()
                .Property(r => r.ID).UseIdentityColumn();
           
        }
    }
}
