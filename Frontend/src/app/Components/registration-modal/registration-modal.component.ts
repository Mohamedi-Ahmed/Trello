import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RegistrationService } from '../../Services/Registration/registration.service';
import { of } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registration-modal',
  templateUrl: './registration-modal.component.html',
  styleUrls: ['./registration-modal.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class RegistrationModalComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<RegistrationModalComponent>,
    private registrationService: RegistrationService // Injectez votre service
  ) {}

  userEmail: string = this.data.userEmail;
  firstName: string = '';
  lastName: string = '';
  password: string = '';
  nickName: string = '';

  nickNameError: boolean = false;
  emailError: boolean = false;
  passwordError: boolean = false;

  nicknameTakenError: boolean = false;
  emailTakenError: boolean = false; // Ajout de la vérification pour l'e-mail

  registrationSuccess = false;

  onNicknameChange() {
    this.nicknameTakenError = false; // Réinitialise l'indicateur d'erreur
  }
  onEmailChange() {
    this.emailError = false; // Réinitialise l'indicateur d'erreur
  }

  onSubmit() {
    
    this.nickNameError = this.nickName.trim() === '';
    this.emailError = this.userEmail.trim() === '';
    this.passwordError = this.password.trim() === '';
    
  
    // Vérifiez s'il y a des erreurs
    if (this.nickNameError || this.emailError || this.passwordError) {
      return; // Ne validez pas le formulaire si des erreurs existent
    }

    // Vérifiez la disponibilité du pseudo
    this.registrationService.checkNicknameAvailability(this.nickName).subscribe(
      (nicknameResponse) => {
        if (nicknameResponse.isTaken) {
          this.nicknameTakenError = true;
        } else {
          // Le pseudo n'est pas pris, continuez avec la vérification de l'e-mail
          this.registrationService.checkEmailAvailability(this.userEmail).subscribe(
            (emailResponse) => {
              if (emailResponse.isTaken) {
                this.emailTakenError = true;
              } else {
                // Les deux pseudo et e-mail ne sont pas pris, continuez avec la soumission normale
                const formData = {
                  firstName: this.firstName,
                  lastName: this.lastName,
                  username: this.nickName,
                  email: this.userEmail,
                  password: this.password,
                };

                /*
                // Simuler une requête réussie
                const fakeResponse = { message: 'Inscription réussie' };
                of(fakeResponse).subscribe(
                  (response) => {
                    // La requête a réussi, définissez registrationSuccess sur true
                    this.registrationSuccess = true;
                    console.log('Réponse du serveur :', response);

                    // Fermez la boîte de dialogue modale après la soumission réussie
                    //this.dialogRef.close();
                  },
                  (error) => {
                    // La requête a échoué, vous pouvez gérer l'erreur ici
                    console.error('Erreur de la requête :', error);
                  }
                );
               */
              
                 
                // Utilisez le service pour gérer la requête d'inscription
                this.registrationService.registerUser(formData).subscribe(
                  (response) => {
                    // La requête a réussi, vous pouvez gérer la réponse ici
                    console.log('Réponse du serveur :', response);
                    this.registrationSuccess = true;
                    this.userEmail = '';
                  },
                  (error) => {
                    // La requête a échoué, vous pouvez gérer l'erreur ici
                    console.error('Erreur de la requête :', error);
                  }
                );
                
              }
            },
            (error) => {
              console.error('Erreur de la requête pour la vérification de l\'e-mail :', error);
            }
          );
        }
      },
      (error) => {
        console.error('Erreur de la requête pour la vérification du pseudo :', error);
      }
    );
  }

  closeModal() {
    this.dialogRef.close();
  }
}
