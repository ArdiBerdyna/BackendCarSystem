
using CarSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public ReservationsController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
      
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            if (_context.Reservation == null)
            {
                return NotFound();
            }
            var ardi = await _context.Reservation.ToListAsync();
            return ardi;
        }

      
        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            if (_context.Reservation == null)
            {
                return NotFound();
            }
            var car = await _context.Reservation.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

       
        [HttpPost]
        [Route("CalculateDiscountedPrice")]
        public ActionResult<double> CalculateDiscountedPrice(Reservation car)
        {
            // Check if the user has made a multiple of 5 purchases
            bool discountApplied = _context.Reservation.Count(c => c.UserId == car.UserId) % 5 == 0 && _context.Reservation.Count(c => c.UserId == car.UserId) != 0;

            if (discountApplied)
            {
                // Apply a discount logic here
                double discountedPrice = car.Total * 0.8; // 20% discount (adjust as needed)
                return Ok(discountedPrice);
            }

            // If no discount applied, return the original price
            return Ok(car.Total);
        }


        [HttpPost]
        [Route("PostReservation")]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation car)
        {
            _context.Reservation.Add(car);

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
            if (_context.Reservation == null)
            {
                return NotFound();
            }
            var car = await _context.Reservation.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return (_context.Reservation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    } 
}
