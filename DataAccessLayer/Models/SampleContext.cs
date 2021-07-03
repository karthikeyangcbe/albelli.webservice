using System;
using albelli.configuration.library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Albelli.DataAccessLayer.Model
{
    public partial class AlbilleDBContext : DbContext, IAlbilleDBContext
    {
        public AlbilleDBContext()
        {
        }

        public AlbilleDBContext(DbContextOptions<AlbilleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customeraddress> Customeraddresses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Productdetails> Productnames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //ConfigurationManager.Instance.ConnectionString
                 optionsBuilder.UseSqlServer("Server=KGENGARAM-5410;User ID=sa;Password=Aspect123$;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;database=albelli");
                //optionsBuilder.UseSqlServer(ConfigurationManager.Instance.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Customerid).ValueGeneratedNever();
            });

            modelBuilder.Entity<Customeraddress>(entity =>
            {
                entity.Property(e => e.Customeraddressid).ValueGeneratedNever();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Orderid).ValueGeneratedNever();
            });

            modelBuilder.Entity<Productdetails>(entity =>
            {
                entity.Property(e => e.Productid).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
