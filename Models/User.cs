using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsAPI.Models
{

[Table("Users")]
    public class User
    {

        [Key]
        [Required]
        
        public long ID { get; set; }
        public string Uname { get; set; }

        public string Pass { get; set; }

        public User()
        {

        }
        public User(long id, string uname, string pass)
        {
            this.ID = id;
            this.Uname = uname;
            this.Pass = pass;
        }
    }

}