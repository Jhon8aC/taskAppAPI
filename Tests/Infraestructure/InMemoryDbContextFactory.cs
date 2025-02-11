using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infraestructure
{
    public static class InMemoryDbContextFactory
    {
        public static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //Each test will use a unique database
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
