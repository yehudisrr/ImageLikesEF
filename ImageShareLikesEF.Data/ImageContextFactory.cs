using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageShareLikesEF.Data
{
    public class ImageContextFactory : IDesignTimeDbContextFactory<ImageDbContext>
    {
        public ImageDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}ImageShareLikesEF.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageDbContext(config.GetConnectionString("ConStr"));
        }
    }
}