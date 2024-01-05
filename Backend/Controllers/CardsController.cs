using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrelloMVC.Models;

namespace TrelloMVC.Controllers
{
    public class CardsController : Controller
    {
        private readonly ILogger<CardsController> _logger;
	    private readonly DbTrelloContext _context;

        public CardsController(ILogger<CardsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

// Requête pour l'affichage de toutes les cards (READ)
        [HttpGet]
        [Route("cards")]
        public IActionResult GerCards()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display all cards");
            Console.WriteLine("----------------------------------------------");

            var myCards = _context.Cards;
            return Json(myCards);
        }

// Requête pour l'affichage d'une card spécifique par Id (READ by id)
        [HttpGet]
        [Route("/cards/{id:int}")]
        public IActionResult GerCard(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display one specific card by id");
            Console.WriteLine("----------------------------------------------");
            if(id == null) { return BadRequest("Id not provided !");}
            var myCard = _context.Cards.Find(id);
            if (myCard == null) {return BadRequest("Card not found !");}

            return Json(myCard); 
        }
        
// Requête pour l'ajout d'un nouveau card (CREATE)
        [HttpPost]
        [Route("cards")]
        public IActionResult CreateCard([FromBody] Card card)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To add a new card");
            Console.WriteLine("----------------------------------------------");
            
            if (!ModelState.IsValid) {return BadRequest(ModelState);}
            
            if(card.DateCreation == default)
            { 
                DateTime now = DateTime.Now;
                card.DateCreation = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            }
            _context.Cards.Add(card);
            _context.SaveChanges();

                return Ok();
        }

// Requête pour la mise à jour d'un card existant (UPDATE)
        [HttpPut]
        [Route("/cards/{id}")]
        public IActionResult UpdateCard(int id, [FromBody] Card cardRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To update an existing card");
            Console.WriteLine("----------------------------------------------");

            var cardToUpdate = _context.Cards.Find(id);
            if(cardToUpdate == null) {return BadRequest("Card not found !");}
            
            // Je ne modifie pas l'id ici
            cardRequest.Id = cardToUpdate.Id;

            if(!ModelState.IsValid){ return BadRequest(ModelState);}
            
            // Je récupere les propriétés de l'objet Card
            PropertyInfo[] props = cardToUpdate.GetType().GetProperties();
            // Iteration dessus
            foreach (PropertyInfo prop in props)
            {
                // Je récupere la valeur associée à chaque propriété dans CardRequest
                var myPropertyValue = prop.GetValue(cardRequest, null);
                Console.WriteLine(myPropertyValue);
                if( myPropertyValue!= null)
                {
                    // Si valeur non nulle => Modification dans CardToUpdate
                    prop.SetValue(cardToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

// Requête pour la suppression d'une card (DELETE)
        [HttpDelete]
        [Route("/cards/{id}")]
        public IActionResult DeleteCard(string id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To delete a card");
            Console.WriteLine("----------------------------------------------");
                        
            int myIntId = int.Parse(id);
            var cardToDelete = _context.Cards.Find(myIntId);
            if(cardToDelete == null) {return BadRequest("Card not found !");}

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