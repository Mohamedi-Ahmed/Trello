// Importation des modules nécessaires depuis Angular
import { Component, Input, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { Project, ProjectsService } from "../../Services/projects.service";
import { UserService } from "../../Services/users.service";


@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.css'
})
export class ProjectsComponent implements OnInit{

  projects!: Project[]; // Liste des threads

    // Constructeur du composant, injecte les services nécessaires
    constructor(
      public ProjectsService: ProjectsService,
      public userService: UserService
    ) {}

    // Méthode appelée lors de l'initialisation du composant
    ngOnInit() {
      // Récupère la liste des threads depuis le service
      this.ProjectsService.getProjects().subscribe((projects: any) => {
          // Met à jour la liste des threads
          this.projects = projects;
          // Affiche les threads dans la console (à des fins de débogage)
          console.log(this.projects);
          // Sélectionne le premier thread par défaut
          //this.selectThread(this.projects[0]);
      });
  }

}
