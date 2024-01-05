using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrelloMVC.Models;

namespace TrelloMVC.Controllers
{
    public class ListsController : Controller
    {
        private readonly ILogger<ListsController> _logger;
	    private readonly DbTrelloContext _context;

        public ListsController(ILogger<ListsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

// Requête pour l'affichage de toutes les listes (READ)
        [HttpGet]
        [Route("lists")]
        public IActionResult GetList()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display all lists");
            Console.WriteLine("----------------------------------------------");

            var myLists = _context.Lists;
            return Json(myLists);
        }

// Requête pour l'affichage d'une liste spécifique par Id (READ by id)
        [HttpGet]
        [Route("/lists/{id:int}")]
        public IActionResult GetList(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display one specific list by id");
            Console.WriteLine("----------------------------------------------");
            if(id == null) { return BadRequest("Id not provided !");}
            var myList = _context.Lists.Find(id);
            if (myList == null) {return BadRequest("List not found !");}
            return Json(myList);
            
        }
        
// Requête pour la création d'une nouvelle liste (CREATE)
        [HttpPost]
        [Route("lists")]
        public IActionResult CreateList([FromBody] List list)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To create a new list");
            Console.WriteLine("----------------------------------------------");
            
            if (!ModelState.IsValid) {return BadRequest(ModelState);}
            
            _context.Lists.Add(list);
            _context.SaveChanges();

                return Ok();
        }

// Requête pour la mise à jour d'une liste existante (UPDATE)
        [HttpPut]
        [Route("/lists/{id}")]
        public IActionResult UpdateList(int id, [FromBody] List listRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To update an existing list");
            Console.WriteLine("----------------------------------------------");

            var listToUpdate = _context.Lists.Find(id);
            if(listToUpdate == null) {return BadRequest("List not found !");}
            
            // Je ne modifie pas l'id ici
            listRequest.Id = listToUpdate.Id;

            if(!ModelState.IsValid){ return BadRequest(ModelState);}
            
            // Je recupere les proprietes de l'objet List
            PropertyInfo[] props = listToUpdate.GetType().GetProperties();
            // Iteration dessus
            foreach (PropertyInfo prop in props)
            {
                // Je recupere la valeur associee a chaque propriete dans listRequest
                var myPropertyValue = prop.GetValue(listRequest, null);
                Console.WriteLine(myPropertyValue);
                if( myPropertyValue!= null)
                {
                    //si valeur non nulle => Modification dans listToUpdate
                    prop.SetValue(listToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

// Requête pour la suppression d'une liste (DELETE)
        [HttpDelete]
        [Route("/lists/{id}")]
        public IActionResult DeleteList(string id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To delete a list");
            Console.WriteLine("----------------------------------------------");
                        
            int myIntId = int.Parse(id);
            var listToDelete = _context.Lists.Find(myIntId);
            if(listToDelete == null) {return BadRequest("List not found !");}

            _context.Lists.Remove(listToDelete);
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