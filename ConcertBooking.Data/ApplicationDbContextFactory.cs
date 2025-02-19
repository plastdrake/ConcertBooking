using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContextFactory() { }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
            var connectionString = builder.GetConnectionString("Todo");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set.");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString).Options;
            return new ApplicationDbContext(options);
        }
    }
}
