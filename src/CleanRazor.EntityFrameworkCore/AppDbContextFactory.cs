﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanRazor.EntityFrameworkCore
{
    public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .Build();

            var connectionString = configuration.GetConnectionString("Default");

            var options = new DbContextOptionsBuilder<AppDbContext>();
#if (UseSqlServer)
            options.UseSqlServer(connectionString);
#elif (UsePostgreSQL)
            options.UseNpgsql(connectionString);
#elif (UseLocalDB)
            options.UseSqlite(connectionString);
#endif
            return new AppDbContext(options.Options);
        }
    }
}
