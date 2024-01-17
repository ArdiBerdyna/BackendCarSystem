﻿using Microsoft.EntityFrameworkCore;

namespace CarSystem.Model
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
        {
        }
     
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Car> CarStock { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }



    }
}
