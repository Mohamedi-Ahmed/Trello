using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrelloMVC.Models;

namespace TrelloMVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ILogger<CommentsController> _logger;
	    private readonly DbTrelloContext _context;

        public CommentsController(ILogger<CommentsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

// Requête pour l'affichage de toutes les commentaires (READ)
        [HttpGet]
        [Route("comments")]
        public IActionResult GetComment()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display all comments");
            Console.WriteLine("----------------------------------------------");

            var myComments = _context.Comments;
            return Json(myComments);
        }

// Requête pour l'affichage d'un commentaire spécifique par Id (READ by id)
        [HttpGet]
        [Route("/comments/{id:int}")]
        public IActionResult GetComment(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display one specific comment by id");
            Console.WriteLine("----------------------------------------------");
            if(id == null) { return BadRequest("Id not provided !");}
            var myComment = _context.Comments.Find(id);
            if (myComment == null) {return BadRequest("Comment not found !");}

            return Json(myComment); 
        }
        
// Requête pour l'ajout d'un nouveau commentaire (CREATE)
        [HttpPost]
        [Route("comments")]
        public IActionResult CreateComment([FromBody] Comment comment)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To add a new comment");
            Console.WriteLine("----------------------------------------------");
            
            if (!ModelState.IsValid) {return BadRequest(ModelState);}
            if(comment.DateCreation == default)
            { 
                DateTime now = DateTime.Now;
                comment.DateCreation = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            }            
            _context.Comments.Add(comment);
            _context.SaveChanges();

                return Ok();
        }

// Requête pour la mise à jour d'un commentaire existant (UPDATE)
        [HttpPut]
        [Route("/comments/{id}")]
        public IActionResult UpdateComment(int id, [FromBody] Comment commentRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To update an existing comment");
            Console.WriteLine("----------------------------------------------");

            var commentToUpdate = _context.Comments.Find(id);
            if(commentToUpdate == null) {return BadRequest("Comment not found !");}
            
            // Je ne modifie pas l'id ici
            commentRequest.Id = commentToUpdate.Id;

            if(!ModelState.IsValid){ return BadRequest(ModelState);}
            
            // Je récupere les propriétés de l'objet Comment
            PropertyInfo[] props = commentToUpdate.GetType().GetProperties();
            // Iteration dessus
            foreach (PropertyInfo prop in props)
            {
                // Je récupere la valeur associée à chaque propriété dans commentRequest
                var myPropertyValue = prop.GetValue(commentRequest, null);
                Console.WriteLine(myPropertyValue);
                if( myPropertyValue!= null)
                {
                    // Si valeur non nulle => Modification dans commentToUpdate
                    prop.SetValue(commentToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

// Requête pour la suppression d'un commentaire (DELETE)
        [HttpDelete]
        [Route("/comments/{id}")]
        public IActionResult DeleteComment(string id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To delete a comment");
            Console.WriteLine("----------------------------------------------");
                        
            int myIntId = int.Parse(id);
            var commentToDelete = _context.Comments.Find(myIntId);
            if(commentToDelete == null) {return BadRequest("Comment not found !");}

            _context.Comments.Remove(commentToDelete);
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