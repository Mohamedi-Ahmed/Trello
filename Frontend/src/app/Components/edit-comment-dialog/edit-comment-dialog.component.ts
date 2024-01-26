import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { CommentsService } from '../../Services/Comments/comments.service';

@Component({
  selector: 'app-edit-comment-dialog',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-comment-dialog.component.html',
  styleUrls: ['./edit-comment-dialog.component.css']
})

export class EditCommentDialogComponent {
  updatedComment: any;

  constructor(
    public dialogRef: MatDialogRef<EditCommentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commentsService: CommentsService
  ) {
    this.updatedComment = { ...data };
  }

  saveChanges() {
    // Vous pouvez ajouter la logique pour enregistrer les modifications du commentaire ici
    // Vous pouvez utiliser un service pour envoyer les modifications au backend
    // Exemple : this.commentService.updateComment(this.updatedComment).subscribe(...);
    this.dialogRef.close(this.updatedComment);
  }

  closeDialog() {
    this.dialogRef.close();
  }

  editComment() {
    // Vous pouvez ajouter la logique pour modifier le commentaire ici
    // Par exemple, ouvrir un formulaire de modification dans la boîte de dialogue
  }

  deleteComment() {
    const confirmation = confirm('Voulez-vous vraiment supprimer ce commentaire ?');
    
    if (confirmation) {
      // Utilisez le service pour supprimer le commentaire côté serveur
      this.commentsService.deleteComment(this.data.commentId).subscribe(
        () => {
          // Suppression réussie, fermez la boîte de dialogue
          this.dialogRef.close();
        },
        (error) => {
          console.error('Erreur lors de la suppression du commentaire :', error);
          // Gérez les erreurs ici, par exemple, affichez un message d'erreur à l'utilisateur
        }
      );
    }
  }
}