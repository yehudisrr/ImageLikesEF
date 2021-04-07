using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageShareLikesEF.Data
{
    public class ImageDbContext : DbContext
    {
        private readonly string _connectionString;

        public ImageDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Image> Images { get; set; }
    }
}
