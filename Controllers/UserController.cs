using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsAPI.Models;

namespace CarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CarsContext _context;

        public UserController(CarsContext context)
        {
            _context = context;
        }
        // GET: api/GetCars
      /*   [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context.Users
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        } */
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return ItemToDTO(user);
        }
    
   
        

       /*  [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(long id, CarDTO CarDTO)
        {
            if (id != CarDTO.Id)
            {
                return BadRequest();
            }

            var car = await _context.MyCars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            car.Make = CarDTO.Make;
            car.Model= CarDTO.Model;
            car.Price= CarDTO.Price;
            car.Year = CarDTO.Year;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CarExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
 */
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {
           string password= EncodePasswordToBase64(userDTO.Pass);
            var user = new User
            {
           ID = userDTO.ID,
           Uname = userDTO.Username,
           Pass = password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.ID },
                ItemToDTO(user));
        }

       
        private bool UserExists(long id) =>
             _context.Users.Any(e => e.ID == id);

        private static UserDTO ItemToDTO(User user) =>
            new UserDTO
            {
            ID=user.ID,
           Username=user.Uname,
           Pass=user.Pass

            };
              public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                Console.WriteLine(encodedData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
    
}