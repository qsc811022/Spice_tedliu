using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Spice_tedliu.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace Spice_tedliu.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categroy { get; set; }

        public DbSet<SubCategory> SubCategroy { get; set; }
        public DbSet<MenuItem> MenuItem { get;set;}

        public DbSet<Coupon> Coupon { get;set;}
    }
}
