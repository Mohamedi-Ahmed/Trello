import { Component, Input, ElementRef , OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthenticationService } from '../../Services/Authentications/authentication.service';
import { MatDialog } from '@angular/material/dialog';
import { EditCommentDialogComponent } from '../edit-comment-dialog/edit-comment-dialog.component';
import { CommentsService } from '../../Services/Comments/comments.service';

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})

export class CommentsComponent implements OnInit {
  @Input() cardId!: number;
  

  comments: any[] = [];
  newComment: any = {};

  constructor(
    private commentsService: CommentsService,
    private authService: AuthenticationService,
    private dialog: MatDialog
  ) {
    this.newComment = { Content: '', CreatorId: 0 };
  }

  ngOnInit() {
    this.loadComments();
  }

  loadComments() {
    this.commentsService.getCommentsByCardId(this.cardId).subscribe(
      (comments) => {
        this.comments = comments;
        this.loadCreatorNames();
        this.resetNewCommentForm();

      },
      (error) => {
        console.error('Erreur lors du chargement des commentaires :', error);
      }
    );
  }

  loadCreatorNames() {
    //console.log(this.comments)
    this.comments.forEach((comment) => {
      this.authService.getUsernameById(comment.CreatorId).subscribe(
        (name) => {
          comment.creator.name = { name: name.name };
        },
        (error) => {
          console.error(
            'Erreur lors de la récupération du nom du créateur :',
            error
          );
        }
      );
    });
  }

  openOptionsDialog(comment: any,  button: HTMLElement) {
    const dialogRef = this.dialog.open(EditCommentDialogComponent, {
      data: comment 
    });
  
    dialogRef.afterClosed().subscribe((choice: string) => {
      if (choice === 'modifier') {
        this.openEditCommentDialog(comment, button);
      } else if (choice === 'supprimer') {
        const confirmation = confirm('Voulez-vous vraiment supprimer ce commentaire ?');
        if (confirmation) {
          this.deleteComment(comment);
        }
      }
    });
  }
  
  openEditCommentDialog(comment: any, button: HTMLElement) {

    const buttonPosition = button.getBoundingClientRect(); // Obtenir la position du bouton "Modifier"

    const dialogRef = this.dialog.open(EditCommentDialogComponent, {
      panelClass: 'custom-dialog',
      data: comment
    });
  
    dialogRef.afterClosed().subscribe(result => {
      this.loadComments();
      this.loadCreatorNames();    
    });
  }

  createComment() {
    if (!this.newComment.text.trim()) {
      alert('Le texte du commentaire est requis.');
      return;
    }

    this.authService.getUserId().subscribe((creatorId) => {
      const commentToCreate = {
        Content: this.newComment.text,
        IdCard: this.cardId, 
        UserId: creatorId
      };

      console.log("commentToCreate : " + commentToCreate.IdCard + ' ' + commentToCreate.UserId + ' ' + commentToCreate.Content)
      this.commentsService.createComment(commentToCreate).subscribe(
        (createdComment) => {
          this.comments.push(createdComment);
          this.loadComments();
        },
        (error) => {
          console.error('Erreur lors de la création du commentaire :', error);
        }
      );
    });
  }

  resetNewCommentForm() {
    this.newComment = { text: '', CreatorId: -1 };
  }

  deleteComment(comment: any) {
    const confirmation = confirm('Voulez-vous vraiment supprimer ce commentaire ?');
    if (confirmation) {
      this.commentsService.deleteComment(comment.id).subscribe(
        () => {
          this.comments = this.comments.filter((c) => c.id !== comment.id);
        },
        (error) => {
          console.error(
            'Erreur lors de la suppression du commentaire :',
            error
          );
        }
      );
    }
  }



}
