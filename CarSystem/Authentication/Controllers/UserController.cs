using Authentication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet]
        [Route("Getuser")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userDbContext.Users.ToListAsync();
        }





        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(User objUser)
        {
            var dbuser = _userDbContext.Users.Where(u => u.Email == objUser.Email).FirstOrDefault();
            if (dbuser != null)
            {
                return BadRequest("Email already exists");
            }


            // Generate salt and hash password using bcrypt algorithm
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(objUser.Password, salt);

            objUser.Password = hashedPassword; // Save hashed password in the database
            _userDbContext.Users.Add(objUser);
            await _userDbContext.SaveChangesAsync();
            return Ok("regjistrimi u bo me sukses");

        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(Login user)
        {
            var userInDb = await _userDbContext.Users.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (userInDb == null || !BCrypt.Net.BCrypt.Verify(user.Password, userInDb.Password))
            {
                return BadRequest("Invalid email or password.");
            }

            var role = userInDb.Role;
            var id = userInDb.Id.ToString();

            // Create claims for the token
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, userInDb.Email),
        new Claim(ClaimTypes.Role, role),
        new Claim(ClaimTypes.NameIdentifier, id)
    };

            // Generate symmetric security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyWithAtLeast16Characters"));

            // Generate signing credentials using the security key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1), // Set token expiration
                SigningCredentials = credentials
            };

            // Create a token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate token based on the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Convert token to a string
            var tokenString = tokenHandler.WriteToken(token);
            //return Ok(new { Token = tokenString, UserId = userInDb.Id, Role = role });
           // const { UserId, Token } = response.data;
            // Store the user ID in state
           // setUserId(UserId);
            return Ok(new { Token = tokenString });
        }
    }
}
