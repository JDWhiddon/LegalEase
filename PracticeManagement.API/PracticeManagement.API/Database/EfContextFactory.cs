﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PracticeManagement.API.Database
{
    public class EfContextFactory : IDesignTimeDbContextFactory<EfContext>
    {
        public EfContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EfContext(optionsBuilder.Options);
        }

        static string? connectionString = null;

        static EfContextFactory()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            connectionString = config["ConectionStrings:LegalEase_DB2"];
        }
    }
}
