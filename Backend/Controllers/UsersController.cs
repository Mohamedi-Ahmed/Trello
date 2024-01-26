using Microsoft.AspNetCore.Mvc;
using TrelloMVC.Models;
using System.Linq;
using System;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace TrelloMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DbTrelloContext _context;

        public UsersController(ILogger<UsersController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("checknickname/{nickname}")]
        public IActionResult CheckNicknameAvailability(string nickname)
        {
            var userWithSameNickname = _context.Users.FirstOrDefault(u => u.UserName == nickname);
            return Ok(new { isTaken = userWithSameNickname != null });
        }

        [HttpGet("checkemail/{email}")]
        public IActionResult CheckEmailAvailability(string email)
        {
            var userWithSameEmail = _context.Users.FirstOrDefault(u => u.Email == email);
            return Ok(new { isTaken = userWithSameEmail != null });
        }

        public class RegistrationDto
        {
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationDto registrationDto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == registrationDto.Username);
            if (existingUser != null)
            {
                return Conflict("Un utilisateur avec le même nom d'utilisateur existe déjà.");
            }

            var existingEmail = _context.Users.FirstOrDefault(u => u.Email == registrationDto.Email);
            if (existingEmail != null)
            {
                return Conflict("L'adresse e-mail est déjà enregistrée.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password);

            var newUser = new User
            {
                UserName = registrationDto.Username,
                Email = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Password = hashedPassword
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }

        public class LoginDto
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);
                
                if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    return Ok(new { Success = true, Message = "Connexion réussie", UserId = user.Id });                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Identifiant ou mot de passe incorrect." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la tentative de connexion.");
                return StatusCode(500, "Une erreur interne est survenue. Veuillez réessayer plus tard.");
            }
        }
        /*
        [HttpGet("updatedb")]
        public IActionResult MiseAJourMotsDePasse()
        {
            var utilisateurs = _context.Users.ToList(); // Récupérez tous les utilisateurs

            foreach (var utilisateur in utilisateurs)
            {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(utilisateur.Password);

                    // Mettez à jour le mot de passe dans la base de données
                    utilisateur.Password = hashedPassword;

                    // Enregistrez les modifications dans la base de données
                    _context.SaveChanges();
            }
            return Ok();
        }
        */


        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (users == null || !users.Any())
            {
                return NotFound("Aucun utilisateur trouvé.");
            }
            return Ok(users);
        }

        [HttpGet("username/{id}")]
        public IActionResult GetUserNameByID(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new { name = user.UserName });
        }




    }
}
