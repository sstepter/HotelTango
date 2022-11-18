using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HotelTango.Models;

namespace HotelTango.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HotelTango.Models.Room> Room { get; set; }
        public DbSet<HotelTango.Models.RoomType> RoomType { get; set; }
        public DbSet<HotelTango.Models.Customer> Customer { get; set; }
        public DbSet<HotelTango.Models.Reservation> Reservation { get; set; }
    }
}
