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
    public class CarsController : ControllerBase
    {
        private readonly CarsContext _context;

        public CarsController(CarsContext context)
        {
            _context = context;
        }
          /// <summary>
        /// Gets all Cars.
        /// </summary>
        // GET: api/GetCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            return await _context.MyCars
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        /// <summary>
        /// Gets a specific Car by ID.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCar(long id)
        {
            var car = await _context.MyCars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return ItemToDTO(car);
        }

          /// <summary>
        /// Updates a specific Car by ID.
        /// </summary>
        [HttpPut("{id}")]
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
          /// <summary>
        /// Creates a new Car.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CarDTO>> CreateCar(CarDTO CarDTO)
        {
            var car = new Car
            {
            Make = CarDTO.Make,
            Model= CarDTO.Model,
            Price= CarDTO.Price,
            Year = CarDTO.Year
            };

            _context.MyCars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCar),
                new { id = car.Id },
                ItemToDTO(car));
        }

        /// <summary>
        /// Deletes a specific Car.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var car = await _context.MyCars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            _context.MyCars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(long id) =>
             _context.MyCars.Any(e => e.Id == id);

        private static CarDTO ItemToDTO(Car car) =>
            new CarDTO
            {
            Id=car.Id,
            Make = car.Make,
            Model= car.Model,
            Price= car.Price,
            Year = car.Year
            };
    }
}