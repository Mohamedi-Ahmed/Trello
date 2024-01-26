using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TrelloMVC.Models;
using System;
using System.Linq;

namespace TrelloMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : Controller
    {
        private readonly ILogger<CardsController> _logger;
        private readonly DbTrelloContext _context;

        public CardsController(ILogger<CardsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCards()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display all cards");
            Console.WriteLine("----------------------------------------------");

            var myCards = _context.Cards;
            return Json(myCards);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCard(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display one specific card by id");
            Console.WriteLine("----------------------------------------------");
            if (id == null) { return BadRequest("Id not provided !"); }
            var myCard = _context.Cards.Find(id);
            if (myCard == null) { return BadRequest("Card not found !"); }

            return Json(myCard);
        }

        [HttpGet("bylist/{listId:int}")]
        public IActionResult GetCardsByListId(int listId)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"To display cards for list id: {listId}");
            Console.WriteLine("----------------------------------------------");

            var cards = _context.Cards
                        .Where(card => card.IdList == listId)
                        .Select(card => new {
                            id = card.Id,
                            title = card.Title,
                            description = card.Description,
                            dateCreation = card.DateCreation,
                            idList = card.IdList,
                            creatorId = card.CreatorId,
                        })
                        .ToList();

            if (!cards.Any()) { return NotFound($"No cards found for list id: {listId}"); }

            return Json(cards);
        }

        public class CardCreateDto
        {
            public required string Title { get; set; }
            public string? Description { get; set; }
            public string? BackgroundColor { get; set; }
            public int CreatorId { get; set; }
            public int IdList {get; set; }
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] CardCreateDto cardDto)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To add a new card");
                        Console.WriteLine(cardDto.CreatorId);

            Console.WriteLine("----------------------------------------------");

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var card = new Card 
            {
                Title = cardDto.Title,
                Description = cardDto.Description,
                Background = cardDto.BackgroundColor,
                CreatorId = cardDto.CreatorId,
                DateCreation = DateTime.Now,
                IdList = cardDto.IdList
            };

            _context.Cards.Add(card);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCard(int id, [FromBody] Card cardRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To update an existing card");
            Console.WriteLine("----------------------------------------------");

            var cardToUpdate = _context.Cards.Find(id);
            if (cardToUpdate == null) { return BadRequest("Card not found !"); }

            // Je ne modifie pas l'id ici
            cardRequest.Id = cardToUpdate.Id;

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Je récupère les propriétés de l'objet Card
            PropertyInfo[] props = cardToUpdate.GetType().GetProperties();
            // Itération dessus
            foreach (PropertyInfo prop in props)
            {
                // Je récupère la valeur associée à chaque propriété dans CardRequest
                var myPropertyValue = prop.GetValue(cardRequest, null);
                Console.WriteLine(myPropertyValue);
                if (myPropertyValue != null)
                {
                    // Si valeur non nulle => Modification dans CardToUpdate
                    prop.SetValue(cardToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(string id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To delete a card");
            Console.WriteLine("----------------------------------------------");

            int myIntId = int.Parse(id);
            var cardToDelete = _context.Cards.Find(myIntId);
            if (cardToDelete == null) { return BadRequest("Card not found !"); }

            _context.Cards.Remove(cardToDelete);
            _context.SaveChanges();

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
