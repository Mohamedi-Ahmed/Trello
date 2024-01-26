import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { ListsService } from '../../Services/Lists/lists.service';
import { ProjectService } from '../../Services/Projects/projects.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CardsComponent } from '../Cards/cards.component';
import { CardsService } from '../../Services/Cards/cards.service';
import { ProjectCommunicationService } from '../../Services/Project-Communication/project-communication.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [CommonModule, FormsModule, CardsComponent],
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnChanges {

  @Input() projectId!: number;
  @Input() projectName!: string;
  @Input() projectDescription!: string;

  lists: any[] = [];
  newListName: string = '';

  editingTitle: boolean = false;
  editingDescription: boolean = false;

  newProjectName: string = '';
  newProjectDescription: string = '';

  constructor(
    private listService: ListsService,
    private projectService: ProjectService,
    private cardsService: CardsService,
    private projectCommunicationService: ProjectCommunicationService,
    private dialog: MatDialog
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['projectId'] && this.projectId) {
      this.getListsByProjectId(this.projectId);
    }
  }

  getListsByProjectId(projectId: number) {
    this.listService.getListsByProjectId(projectId).subscribe((data: any) => {
      this.lists = data;
      this.loadListsAndCards();
    });
  }

  loadListsAndCards() {
    this.lists.forEach(list => {
      this.cardsService.getCardsByListId(list.id).subscribe(cards => {
        list.cards = cards;
      });
    });
  }

  createNewList() {
    if (this.newListName && this.projectId) {
      const newList = {
        name: this.newListName,
        description: '',
        projectId: this.projectId,
      };

      this.listService.createList(newList).subscribe(() => {
        this.getListsByProjectId(this.projectId);
        this.newListName = '';
      });
    }
  }

  toggleListSize(list: any) {
    list.active = !list.active;
  }

  deleteProject(projectId: number) {
    if (projectId !== undefined) {
      if (confirm('Êtes-vous sûr de vouloir supprimer ce projet ?')) {
        const projectIdString = projectId.toString();
        this.projectService.deleteProject(projectIdString).subscribe(
          () => {
            this.projectCommunicationService.emitProjectDeleted(projectId);
            console.log('Projet supprimé avec succès');
            this.loadListsAndCards();
          },
          (error) => {
            console.error('Erreur lors de la suppression du projet', error);
          }
        );
      }
    } else {
      console.error("projectId est undefined.");
    }
  }

  toggleProjectsFavorite() {
    alert("Cette fonctionnalité est en cours de développement.");
  }

  @Output() projectNameUpdated = new EventEmitter<string>();
  @Output() projectInfoUpdated = new EventEmitter<{ name: string, description: string }>();

openEditDialog(choice: string) {
    if (choice == 'title') {
      
      const newProjectName = prompt("Entrez le nouveau titre du projet:", this.projectName);
      
      if (newProjectName && newProjectName.trim() !== '') {
        
        this.projectName = newProjectName;
        this.projectCommunicationService.emitProjectNameUpdated(this.projectId, newProjectName);
        
        // Mettez à jour le projet dans la base de données
        const updateProject = { name: newProjectName };

        this.projectService.updateProject(this.projectId, updateProject).subscribe(
          (response) => {
            this.projectName = newProjectName;
            console.log('Nom du projet mis à jour avec succès');
          },
          (error) => {
            console.error('Erreur lors de la mise à jour du nom de la liste', error);
          }
        );
      
      }
    } 
    else if (choice == 'description') {
      
      const newProjectDescription = prompt("Entrez la nouvelle description du projet:", this.projectDescription);
      
      if (newProjectDescription !== null) {
        
        this.projectDescription = newProjectDescription;
        this.projectCommunicationService.emitProjectInfoUpdated(this.projectId, { name: this.projectName, description: newProjectDescription });
      
                const updateProject = { description: newProjectDescription };

                this.projectService.updateProject(this.projectId, updateProject).subscribe(
                  (response) => {
                    this.projectDescription = newProjectDescription;
                    console.log('Nom du projet mis à jour avec succès');
                  },
                  (error) => {
                    console.error('Erreur lors de la mise à jour du nom de la liste', error);
                  }
                );

      }
    } else {
      alert("Choix invalide ou annulé.");
    }
}

  showDropdown: boolean = false;

  toggleDropdown() {
    this.showDropdown = !this.showDropdown;
  }

  editTitle() {
    this.showDropdown = false;
    this.openEditDialog('title')
  }

  editDescription() {
    this.showDropdown = false;
    this.openEditDialog('description')

  }

  editList(list: any) {
    const newName = prompt('Entrez le nouveau nom de la liste:', list.name);
    if (newName && newName.trim() !== '') {
      const updatedList = { name: newName };
      this.listService.updateList(list.id, updatedList).subscribe(
        (response) => {
          list.name = newName;
          console.log('Nom de la liste mis à jour avec succès');
        },
        (error) => {
          console.error('Erreur lors de la mise à jour du nom de la liste', error);
        }
      );
    }
  }

  toggleListFavorite(list: any) {
    alert("Cette fonctionnalité est en cours de développement.");
  }

  deleteList(list: any) {
    if (confirm('Êtes-vous sûr de vouloir supprimer cette liste ?')) {
      this.listService.deleteList(list.id).subscribe(
        () => {
          const index = this.lists.indexOf(list);
          if (index !== -1) {
            this.lists.splice(index, 1);
            console.log('Liste supprimée avec succès');
          }
        },
        (error) => {
          console.error('Erreur lors de la suppression de la liste', error);
        }
      );
    }
  }

}


