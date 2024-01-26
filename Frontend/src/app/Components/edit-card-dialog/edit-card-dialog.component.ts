import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { OverlayModule } from '@angular/cdk/overlay';

@Component({
  selector: 'app-edit-card-dialog',
  standalone: true,
  imports: [FormsModule, DragDropModule, OverlayModule],
  templateUrl: './edit-card-dialog.component.html',
  styleUrls: ['./edit-card-dialog.component.css']
})

export class EditCardDialogComponent {
  updatedCard: any;

  constructor(
    public dialogRef: MatDialogRef<EditCardDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    // Pré-remplissez les champs avec les données de la carte
    this.updatedCard = { ...data };
  }

  saveChanges() {
    // L'utilisateur a soumis les modifications, renvoyez les données mises à jour
    this.dialogRef.close(this.updatedCard);
  }

  dialogIsOpen = false;

  openDialog() {
    this.dialogIsOpen = true;
  }

  closeDialog() {
    this.dialogIsOpen = false;
  }

}
