import { Component, Input, ElementRef , OnInit, ViewChild } from '@angular/core';
import { ListsService } from '../../Services/Lists/lists.service';
import { ProjectService } from '../../Services/Projects/projects.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CardsService } from '../../Services/Cards/cards.service';
import { AuthenticationService } from '../../Services/Authentications/authentication.service';
import { ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditCardDialogComponent } from '../edit-card-dialog/edit-card-dialog.component';
import { CommentsComponent } from '../Comments/comments.component';

@Component({
  selector: 'app-cards',
  standalone: true,
  imports: [CommonModule, FormsModule, CommentsComponent],
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})

export class CardsComponent implements OnInit {

  @Input() listId!: number;
  
  cards: any[] = [];
  newCard: any = {};

  editButton: HTMLElement | undefined;

  constructor(
    private cardsService: CardsService,
    private authService: AuthenticationService,
    private dialog: MatDialog,
    private elementRef: ElementRef 
  ) {
    this.newCard = { title: '', description: '', backgroundColor: '#ffffff', CreatorId: -1 };
  }

  ngOnInit() {
    this.loadCards();
  }

  loadCards() {
    this.cardsService.getCardsByListId(this.listId).subscribe(
      cards => {
        this.cards = cards;
      });
      this.loadCreatorNames();
  }

  loadCreatorNames() {
    console.log("Je eeeeeeeeee")
    console.log(this.cards)
    this.cards.forEach(card => {
      this.authService.getUsernameById(card.CreatorId).subscribe(
        name => {
          card.creator.name = { name: name.name };
        },
        error => {
          console.error('Erreur lors de la récupération du nom du créateur:', error);
        }
      );
    });
  }

  createCard() {
    if (!this.newCard.title.trim()) {
      alert('Le nom du projet est requis.');
      return;
    }

    this.authService.getUserId().subscribe(creatorId => {
      const cardToCreate = {
        Title: this.newCard.title,
        Description: this.newCard.description,
        BackgroundColor: this.newCard.backgroundColor,
        CreatorId: creatorId,
        IdList: this.listId
      };

      this.cardsService.createCard(cardToCreate).subscribe(
        createdCard => {
          this.cards.push(createdCard);
          this.loadCards();
          this.resetNewCardForm();


        },
        error => {
          console.error('Erreur lors de la création de la carte:', error);
        }
      );
    });
  }

  resetNewCardForm() {
    this.newCard = { title: '', description: '', backgroundColor: '#ffffff' };
  }

  editCard(card: any, button: HTMLElement) {
    const buttonPosition = button.getBoundingClientRect(); // Obtenir la position du bouton "Modifier"
    
    const dialogRef = this.dialog.open(EditCardDialogComponent, {
      width: '400px',
      data: { ...card },
      position: {
        top: 0 - buttonPosition.bottom + 'px', // Position en dessous du bouton
        left: buttonPosition.left + 'px' // Position à gauche du bouton
      }
    });
  
    dialogRef.afterClosed().subscribe(updatedCard => {
      if (updatedCard) {
        this.cardsService.updateCard(card.id, updatedCard).subscribe(
          () => {
            this.loadCards();
            this.loadCreatorNames();
          },
          error => {
            console.error('Erreur lors de la mise à jour de la carte :', error);
          }
        );
      }
    });
  }

deleteCard(card: any) {
  const confirmation = confirm('Voulez-vous vraiment supprimer cette carte ?');
  if (confirmation) {
      this.cardsService.deleteCard(card.id).subscribe(
          () => {
              this.cards = this.cards.filter(c => c.id !== card.id);
          },
          error => {
              console.error('Erreur lors de la suppression de la carte :', error);
          }
      );
  }
}


}
