using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Models
{
    public class CarsContext : DbContext
    {
        public CarsContext()
        {
        }

        public CarsContext(DbContextOptions<CarsContext> options)
         : base(options)
        {
        }
        public DbSet<Car> MyCars { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Cars.db");



    }
}