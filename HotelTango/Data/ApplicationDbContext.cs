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
            RandomPasswordGenerator uniquePassword = new RandomPasswordGenerator();
            modelBuilder.Entity<Reservation>().HasKey(e => e.Id);
            modelBuilder.Entity<Reservation>().Property(p => p.WIFI_Passcode).HasComputedColumnSql("LOWER(CHAR(RAND()*24+66))+CHAR(RAND()*24+66)+CHAR(RAND()*24+66)\r\n      +CHAR(RAND()*24+66)+LOWER(CHAR(RAND()*24+66))+CHAR(RAND()*24+66)\r\n      +CHAR(RAND()*24+66)+LOWER(CHAR(RAND()*24+66))+CHAR(RAND()*24+66)\r\n");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<HotelTango.Models.RoomType> RoomType { get; set; }
        public DbSet<HotelTango.Models.Room> Room { get; set; }
        public DbSet<HotelTango.Models.Customer> Customer { get; set; }
        public DbSet<HotelTango.Models.testClass> testClass { get; set; }
        public DbSet<HotelTango.Models.Reservation> Reservation { get; set; }


    }
}
