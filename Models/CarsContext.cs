using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Models
{
    public class CarsContext : DbContext
    { private static bool _created = false;
    public CarsContext()
    {
        if (!_created)
        {
            _created = true;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
       

        public CarsContext(DbContextOptions<CarsContext> options)
         : base(options)
        {
        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Cars.db");
        public DbSet<Car> MyCars { get; set; }


    }
}