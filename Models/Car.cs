
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAPI.Models
{
    [Table("MyCars")]
    public class Car
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string Make { get; set; }
        public string Model{ get; set; }
        public double Price { get; set; }
        public int Year{get;set;}
        public Car()
        {
            
        }
        public Car(long id, string make, string model, double price, int year)
        {
            this.Id=id;
            this.Make=make;
            this.Model=model;
            this.Price=price;
            this.Year=year;

        }
 public Car( string make, string model, double price, int year)
        {
           
            this.Make=make;
            this.Model=model;
            this.Price=price;
            this.Year=year;

        }
    }
   
}