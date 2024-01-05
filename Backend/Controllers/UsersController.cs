using Microsoft.AspNetCore.Mvc;
using TrelloMVC.Models;
using System.Linq;

namespace TrelloMVC.Controllers
{
    public class UsersController : ControllerBase
    {
            private readonly ILogger<UsersController> _logger;
            private readonly DbTrelloContext _context;

            public UsersController(ILogger<UsersController> logger, DbTrelloContext context)
            {
                _logger = logger;
                _context = context;
            }

            public class LoginDto
            {
                public required string Username { get; set; }
            }

            [HttpPost]
            [Route("users")]
            public IActionResult Login([FromBody] LoginDto loginDto)
            {
                if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username))
                {
                    return BadRequest("Le nom d'utilisateur est requis.");
                }

                var userName = loginDto.Username;
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("Je suis dans user");
                Console.WriteLine(userName);
                Console.WriteLine("------------------------------------------------------------------------------");

                // Rechercher l'utilisateur par son nom
                var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    // Utilisateur trouvé
                    return Ok(user);
                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else{
                    // Créer un nouvel utilisateur si non trouvé (selon votre logique métier)
                    var newUser = new User {UserName = userName};
                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    return Ok(newUser);
                }
            }

            [HttpGet]
            [Route("users/{id}")]
            public IActionResult GetUserNameByID(int id)
            {
                var myUser = _context.Users.Find(id);
                if (myUser == null)
                {
                    return NotFound(); // Retourne un code d'état 404 si l'utilisateur n'est pas trouvé
                }
                return Ok(new {name = myUser.UserName }); // Retourne le nom de l'utilisateur avec un code d'état 200
            }
    }
}