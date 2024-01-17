using CarSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public ContactsController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        [Route("GetContact")]

        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var ardi = await _context.Contacts.ToListAsync();
            return ardi;
        }


        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var con = await _context.Contacts.FindAsync(id);

            if (con == null)
            {
                return NotFound();
            }

            return con;
        }

        // PUT: api/Cars/5

        [HttpPut]
        public IActionResult UpdateContacts(Contact con)
        {
            try
            {
                if (con == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                else
                {
                    _context.Contacts.Update(con);
                    _context.SaveChanges();
                }
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Cars



        [HttpPost]
        [Route("PostContact")]
        public async Task<ActionResult<Contact>> PostContact(Contact con)
        {
            _context.Contacts.Add(con);

            // Check if the user has made a multiple of 5 purchases


            await _context.SaveChangesAsync();



            //return CreatedAtAction("GetCar", new { id = car.Id }, new { Car = car, Message = message, DiscountApplied = discountApplied });
            return Ok(con);

            //return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }



        // DELETE: api/Cars/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var con = await _context.Contacts.FindAsync(id);
            if (con == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(con);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContatsExists(int id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
