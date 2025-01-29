//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace Infraestructure.Contexts
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            // Get variable with "set DefaultConnection= data" cmd
//            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
//            if (string.IsNullOrEmpty(connectionString))
//            {

//                throw new InvalidOperationException($"Connection string not found. string means:{connectionString}");
//            }
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseSqlServer(connectionString); // this contain the connection string

//            return new ApplicationDbContext(optionsBuilder.Options);
//        }
//    }
//}
