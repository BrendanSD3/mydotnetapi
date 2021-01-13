using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Models
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options)
            : base(options)
        {
        }

        public DbSet<Car> MyCars { get; set; }
    }
}