using CarSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public CarsController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        [Route("GetCar")]

        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            if (_context.CarStock == null)
            {
                return NotFound();
            }
            var ardi = await _context.CarStock.ToListAsync();
            return ardi;
        }


        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            if (_context.CarStock == null)
            {
                return NotFound();
            }
            var car = await _context.CarStock.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       


        [HttpPost]
        [Route("PostCar")]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.CarStock.Add(car);

            // Check if the user has made a multiple of 5 purchases


            await _context.SaveChangesAsync();



            //return CreatedAtAction("GetCar", new { id = car.Id }, new { Car = car, Message = message, DiscountApplied = discountApplied });
            return Ok(car);

            //return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }



        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (_context.CarStock == null)
            {
                return NotFound();
            }
            var car = await _context.CarStock.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.CarStock.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return (_context.CarStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
