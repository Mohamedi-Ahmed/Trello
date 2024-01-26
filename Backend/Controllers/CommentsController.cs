using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TrelloMVC.Models;
using System;
using System.Linq;

namespace TrelloMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly DbTrelloContext _context;

        public CommentsController(ILogger<CommentsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display all comments");
            Console.WriteLine("----------------------------------------------");

            var myComments = _context.Comments;
            return Json(myComments);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetComment(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display one specific comment by id");
            Console.WriteLine("----------------------------------------------");
            if (id == null) { return BadRequest("Id not provided !"); }
            var myComment = _context.Comments.Find(id);
            if (myComment == null) { return BadRequest("Comment not found !"); }

            return Json(myComment);
        }

        [HttpGet("bycard/{cardId:int}")]
        public IActionResult GetCommentsByCardId(int cardId)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"To display comments for card id: {cardId}");
            Console.WriteLine("----------------------------------------------");

            var comments = _context.Comments
                        .Where(comment => comment.IdCard == cardId)
                        .Select(comment => new {
                            id = comment.Id,
                            content = comment.Content,
                            user = comment.User,
                            dateCreation = comment.DateCreation,
                            userId = comment.UserId
                        })
                        .ToList();

            if (!comments.Any()) { return NotFound($"No comments found for list id: {cardId}"); }

            return Json(comments);
        }

        public class CommentCreateDto
        {
            public required string Content { get; set; }
            public DateTime DateCreation { get; set; }
            public int IdCard { get; set; }
            public int UserId { get; set; }
        }
        
        [HttpPost]
        public IActionResult CreateComment([FromBody] CommentCreateDto commentDto)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To add a new comment");
            Console.WriteLine("----------------------------------------------");

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var comment = new Comment
            {
                Content = commentDto.Content,
                DateCreation = commentDto.DateCreation,
                IdCard = commentDto.IdCard,
                UserId = commentDto.UserId
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromBody] Comment commentRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To update an existing comment");
            Console.WriteLine("----------------------------------------------");

            var commentToUpdate = _context.Comments.Find(id);
            if (commentToUpdate == null) { return BadRequest("Comment not found !"); }

            // Je ne modifie pas l'id ici
            commentRequest.Id = commentToUpdate.Id;

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Je récupère les propriétés de l'objet Comment
            PropertyInfo[] props = commentToUpdate.GetType().GetProperties();
            // Itération dessus
            foreach (PropertyInfo prop in props)
            {
                // Je récupère la valeur associée à chaque propriété dans commentRequest
                var myPropertyValue = prop.GetValue(commentRequest, null);
                Console.WriteLine(myPropertyValue);
                if (myPropertyValue != null)
                {
                    // Si valeur non nulle => Modification dans commentToUpdate
                    prop.SetValue(commentToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To delete a comment");
            Console.WriteLine("----------------------------------------------");

            var commentToDelete = _context.Comments.Find(id);
            if (commentToDelete == null) { return BadRequest("Comment not found !"); }

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
