using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelTango.Models;
using System;
using System.Numerics;

namespace HotelTango.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasKey(e => e.Id);
            //modelBuilder.Entity<Reservation>().Property(p => p.WIFI_Passcode).HasComputedColumnSql("LOWER(REPLACE(LEFT(NEWID(),10), '-',''))");
            modelBuilder.Entity<Reservation>().Property(p => p.WIFI_Passcode).HasDefaultValueSql("LOWER(REPLACE(LEFT(NEWID(),10), '-',''))");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<HotelTango.Models.RoomType> RoomType { get; set; }
        public DbSet<HotelTango.Models.Room> Room { get; set; }
        public DbSet<HotelTango.Models.Customer> Customer { get; set; }
        public DbSet<HotelTango.Models.testClass> testClass { get; set; }
        public DbSet<HotelTango.Models.Reservation> Reservation { get; set; }


    }
}
