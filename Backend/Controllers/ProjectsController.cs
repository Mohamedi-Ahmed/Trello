using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrelloMVC.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization; // Assurez-vous d'inclure cette directive


namespace TrelloMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult GetProjects()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Récupération des projets");
            Console.WriteLine("----------------------------------------------");
            var myProjects = _context.Projects;

            var projectInfo = myProjects.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DateCreation = p.DateCreation
            }).ToList();

            return Json(projectInfo);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetProject(int? id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans le choix d'un des projets");
            Console.WriteLine("----------------------------------------------");
            if (id == null) { return BadRequest("Pas d'id fourni !"); }
            var myProject = _context.Projects.Find(id);
            if (myProject == null) { return BadRequest("Projet non trouvé !"); }
            
            return Json(myProject);

        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            // Recherche de l'utilisateur par son ID, en incluant les projets associés
            var user = await _context.Users
                .Include(u => u.UserProjects)
                .ThenInclude(up => up.Project)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            // Récupération des projets associés à l'utilisateur
            var projects = user.UserProjects
                .Where(up => up.UserRole == "Créateur" || up.UserRole == "Membre")
                .Select(up => up.Project)
                .ToList();

            // Création d'une liste d'objets contenant les IDs, les noms et les rôles des projets
            var projectInfo = projects.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Role = user.UserProjects.FirstOrDefault(up => up.ProjectId == p.Id)?.UserRole // Obtenez le rôle de l'utilisateur pour ce projet
            }).ToList();

            return Json(projectInfo);
        }


        public class CreateProjectDto
        {
            public required string Name { get; set; }

            public string? Description { get; set; }

            public required int UserId { get; set; }
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectDto projectDto)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans la fonction create Projects");
            Console.WriteLine("Name : " + projectDto.Name + " | UserId : " + projectDto.UserId);
            Console.WriteLine("----------------------------------------------");

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Création du projet
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                DateCreation = DateTime.Now
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            
            // Création de l'association UserProject
            var userProject = new UserProject
            {
                UserId = projectDto.UserId,
                ProjectId = project.Id,
                UserRole = "Créateur"
            };

            _context.UserProjects.Add(userProject);
            _context.SaveChanges();
            
            return Ok(project);
        }

        public class UpdateProjectDto
        {
            public string Name { get; set; } = null!;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto updateDto)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans la fonction d'updateProject");
            Console.WriteLine("----------------------------------------------");

            var projectToUpdate = _context.Projects.Find(id);
            if (projectToUpdate == null) { return NotFound("Projet non trouvé !"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Mise à jour du titre du projet
            projectToUpdate.Name = updateDto.Name;
            
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Je suis dans la fonction delete");
            Console.WriteLine("----------------------------------------------");

            var projectToDelete = _context.Projects
                .Include(p => p.UserProjects) // Incluez les enregistrements liés dans UserProjects
                .FirstOrDefault(p => p.Id == id);

            if (projectToDelete == null) { return NotFound("Projet non trouvé !"); }

            // Supprimez d'abord les enregistrements liés dans UserProjects
            foreach (var userProject in projectToDelete.UserProjects.ToList())
            {
                _context.UserProjects.Remove(userProject);
            }

            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
            return Ok();
        }

    }
}
