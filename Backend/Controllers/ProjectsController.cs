using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrelloMVC.Models;

namespace TrelloMVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly DbTrelloContext _context;
        
        public ProjectsController(ILogger<ProjectsController> logger, DbTrelloContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("projects")]
        public IActionResult GetProjects([FromQuery] int? projectId)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Récupération des projets");
            Console.WriteLine("----------------------------------------------");
            var myProjects = _context.Projects.ToList();
            return Json(myProjects);
        }

        [HttpGet]
        [Route("projects/{id:int}")]
        public IActionResult GetProject(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans le choix d'un des projets");
            Console.WriteLine("----------------------------------------------");
            if(id == null) { return BadRequest("Pas d'id fourni !");}
            var myProject = _context.Projects.Find(id);
            if (myProject == null) {return BadRequest("Projet non trouvé !");}
            return Json(myProject);
            
        }

        [HttpPost]
        [Route("projects")]
        public IActionResult CreateProject([FromBody] Project project)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans la fonction create Projects");
            Console.WriteLine("----------------------------------------------");

            if(!ModelState.IsValid){ return BadRequest(ModelState);}
            
            if(project.DateCreation == default)
            { 
                DateTime now = DateTime.Now;
                project.DateCreation = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            }
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("/projects/{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Project projectRequest)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans la fonction d'updateProject");
            Console.WriteLine("----------------------------------------------");

            var projectToUpdate = _context.Projects.Find(id);
            if(projectToUpdate == null) {return BadRequest("Message non trouvé !");}
            
            // Je ne modifie pas l'id ici
            projectRequest.Id = projectToUpdate.Id;

            if(!ModelState.IsValid){ return BadRequest(ModelState);}
            
            // Je recupere les proprietes de l'object Project
            PropertyInfo[] props = projectToUpdate.GetType().GetProperties();
            // iteration dessus
            foreach (PropertyInfo prop in props)
            {
                // Je recupere la valeur associee a chaque propriete dans projectRequest
                var myPropertyValue = prop.GetValue(projectRequest, null);
                Console.WriteLine(myPropertyValue);
                if( myPropertyValue!= null)
                {
                    //si valeur non nulle => Modification dans projectToUpdate
                    prop.SetValue(projectToUpdate, myPropertyValue, null);
                }
            }
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("/projects/{id}")]
        public IActionResult DeleteProject(int id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans la fonction delete");
            Console.WriteLine("----------------------------------------------");

            var projectToDelete = _context.Projects.Find(id);
            if(projectToDelete == null) {return BadRequest("Message non trouvé !");}

            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
            return Ok();
        }

    }
}
