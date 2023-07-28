using Microsoft.EntityFrameworkCore;
using PracticeManagement.CLI.Models;

    namespace PracticeManagement.API.Database
    {
        public class EfContext : DbContext
        {
            public EfContext(DbContextOptions<EfContext> options)
                   : base(options) { }

         public DbSet<Client> Clients { get; set; }

    }
}

