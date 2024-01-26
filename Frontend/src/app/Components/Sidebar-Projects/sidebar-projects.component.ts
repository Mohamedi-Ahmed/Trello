import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProjectService } from '../../Services/Projects/projects.service';
import { AuthenticationService } from '../../Services/Authentications/authentication.service';
import { ProjectCommunicationService } from '../../Services/Project-Communication/project-communication.service';


@Component({
  selector: 'app-sidebar-project',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sidebar-projects.component.html',
  styleUrls: ['./sidebar-projects.component.css']
})


export class SidebarProjectComponent implements OnInit {

  @Output() projectSelected = new EventEmitter<{ name: string; id: number; description: string; }>();
  @Input() projectId: number | undefined;
  @Input() projectName: string | undefined;
  @Input() projectDescription!: string;
  
  userProjects: any[] = [];

  newProjectName: string = '';
  newProjectDescription: string = '';

  currentUserId: number = -1; // Initialiser avec -1 = aucun utilisateur connecté
  projectsPerPage: number = this.getProjectsPerPage();
  currentGroup: number = 0;

  projectDeleted: boolean = false;

  constructor(
    private projectsService: ProjectService,
    private authService: AuthenticationService,
    private projectCommunicationService: ProjectCommunicationService
  ) { }

  ngOnInit() {
    this.loadUserProjects();

    window.addEventListener('resize', () => {
      this.projectsPerPage = this.getProjectsPerPage(); 
    });

    this.projectCommunicationService.projectDeleted$.subscribe((projectId: number) => {
      this.userProjects = this.userProjects.filter(project => project.id !== projectId);
      this.currentGroup = 0;
    });
  }

  isSmallScreen(): boolean {
    return window.innerWidth < 768; 
  }
  
  isMediumScreen(): boolean {
    return window.innerWidth >= 768 && window.innerWidth < 1024; 
  }
  
  isLargeScreen(): boolean {
    return window.innerWidth >= 1024; 
  }
  

  getProjectsPerPage(): number {
    if (this.isSmallScreen()) {
      return 3; 
    } else if (this.isMediumScreen()) {
      return 5;
    } else {
      return 7; 
    }
  }

  onSelectProject(project: { name: string, id: number, description: string}) {
    console.log(project)
    this.projectSelected.emit({ id: project.id, name: project.name, description: project.description});  
  }

  loadUserProjects() {
    this.authService.getUserId().subscribe((userId: number) => {
      this.currentUserId = userId;
      if (userId !== -1) {
        this.projectsService.getProjectsByUserId(userId.toString()).subscribe(
          projects => {
            this.userProjects = projects;
            console.log('Projects recupérés:', this.userProjects);
          },
          error => {
            console.error('Erreur lors de la récupération des projets', error);
          }
        );
      }
    });
  }

  createNewProject() {
    if (!this.newProjectName.trim()) {
      alert('Le nom du projet est requis.');
      return;
    }

    const newProject = {
      Name: this.newProjectName,
      Description: this.newProjectDescription || null,
      UserId: this.currentUserId
    };

    this.projectsService.createProject(newProject).subscribe(
      response => {
        this.userProjects.push(response);
        this.newProjectName = '';
        this.newProjectDescription = '';
        console.log('Projet créé avec succès:', response);
      },
      error => {
        console.error('Erreur lors de la création du projet', error);
      }
    );
  }

deleteProject(event: Event, projectId: string) {
  event.stopPropagation(); // Empêche la propagation de l'événement de clic
  if (confirm('Êtes-vous sûr de vouloir supprimer ce projet ?')) {
    this.projectsService.deleteProject(projectId).subscribe(
      () => {
        this.userProjects = this.userProjects.filter(project => project.id !== projectId);
        console.log('Projet supprimé avec succès');
        
        // Émettez l'événement de suppression du projet
        this.projectCommunicationService.emitProjectDeleted(Number(projectId));
      },
      error => {
        console.error('Erreur lors de la suppression du projet', error);
      }
    );
  }
}

  previousGroup() {
    if (this.currentGroup > 0) {
      this.currentGroup--;
    }
  }

  nextGroup() {
    const totalGroups = Math.ceil(this.userProjects.length / this.projectsPerPage);
    if (this.currentGroup < totalGroups - 1) {
      this.currentGroup++;
    }
  }

  getDisplayedProjects() {
    const startIndex = this.currentGroup * this.projectsPerPage;
    const endIndex = startIndex + this.projectsPerPage;
    return this.userProjects.slice(startIndex, endIndex);
  }

  getTotalGroups() {
    return Math.ceil(this.userProjects.length / this.projectsPerPage);
  }

}
