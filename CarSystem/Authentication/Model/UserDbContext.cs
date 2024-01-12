﻿using Microsoft.EntityFrameworkCore;
namespace Authentication.Model
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
           
        }
         public DbSet<User> Users { get; set; }
      
    }
}
