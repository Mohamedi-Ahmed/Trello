using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrelloMVC.Models;
using System;
using System.Linq;

namespace TrelloMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListsController : Controller
    {
        private readonly ILogger<ListsController> _logger;
        private readonly DbTrelloContext _context;

        public ListsController(ILogger<ListsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display all lists");
            Console.WriteLine("----------------------------------------------");

            var myLists = _context.Lists;
            return Json(myLists);
        }

        [HttpGet("project/{projectId}")]
        public IActionResult GetListsByProjectId(int projectId)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To display lists by project id");
            Console.WriteLine("----------------------------------------------");

            var lists = _context.Lists
            // Attention IdProject ici
                .Where(l => l.IdProject == projectId) 
                .ToList();

            //if (!lists.Any()) { return NotFound("Lists not found for the project !"); }

            return Json(lists);
        }
        public class CreateListDto
        {
            
            public required string Name { get; set; }

            
            public required int ProjectId { get; set; }
        }

        [HttpPost]
        public IActionResult CreateList([FromBody] CreateListDto listDto)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Création d'une nouvelle liste");
            Console.WriteLine("Nom de la liste : " + listDto.Name + " | Id du projet : " + listDto.ProjectId);
            Console.WriteLine("----------------------------------------------");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifiez si le projet auquel la liste est associée existe
            var project = _context.Projects.FirstOrDefault(p => p.Id == listDto.ProjectId);
            if (project == null)
            {
                return NotFound("Le projet spécifié n'a pas été trouvé.");
            }

            // Créez la liste
            var list = new List
            {
                Name = listDto.Name,
                IdProject = listDto.ProjectId // Utilisez l'Id du projet à partir du DTO
            };

            _context.Lists.Add(list);
            _context.SaveChanges();

            return Ok(list);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateList(int id, [FromBody] List listRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To update an existing list");
            Console.WriteLine("----------------------------------------------");

            var listToUpdate = _context.Lists.Find(id);
            if (listToUpdate == null) { return BadRequest("List not found !"); }

            // Je ne modifie pas l'id ici
            listRequest.Id = listToUpdate.Id;

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Je recupere les proprietes de l'objet List
            PropertyInfo[] props = listToUpdate.GetType().GetProperties();
            // Iteration dessus
            foreach (PropertyInfo prop in props)
            {
                // Je recupere la valeur associee a chaque propriete dans listRequest
                var myPropertyValue = prop.GetValue(listRequest, null);
                Console.WriteLine(myPropertyValue);
                if (myPropertyValue != null)
                {
                    //si valeur non nulle => Modification dans listToUpdate
                    prop.SetValue(listToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteList(string id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("To delete a list");
            Console.WriteLine("----------------------------------------------");

            int myIntId = int.Parse(id);
            var listToDelete = _context.Lists.Find(myIntId);
            if (listToDelete == null) { return BadRequest("List not found !"); }

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
