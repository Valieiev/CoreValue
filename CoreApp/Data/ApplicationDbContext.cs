using System;
using System.Collections.Generic;
using System.Text;
using CoreApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Clients> Clients { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CoreApp.Models.Court> Court { get; set; }
      
    }
}
