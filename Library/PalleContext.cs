using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class PalleContext : DbContext
    {
        public PalleContext(DbContextOptions<PalleContext> options) : base(options)
        {
            
        }

        public DbSet<Gift> Gifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
