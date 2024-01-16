using CarSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public BlogsController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        [Route("GetBlogs")]

        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var ardi = await _context.Blogs.ToListAsync();
            return ardi;
        }


        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlogById(int id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // PUT: api/Cars/5

        [HttpPut]
        public IActionResult UpdateBlog(Blog product)
        {
            try
            {
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                else
                {
                    _context.Blogs.Update(product);
                    _context.SaveChanges();
                }
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        [HttpPost]
        [Route("PostBlog")]
        public async Task<ActionResult<Car>> PostBlog(Blog blog)
        {
            _context.Blogs.Add(blog);




            await _context.SaveChangesAsync();


            return Ok(blog);

        }



        // DELETE: api/Cars/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
