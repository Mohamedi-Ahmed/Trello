import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService } from '../../Services/Authentications/authentication.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { Router } from '@angular/router';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})

export class AuthenticationComponent {

  constructor(
    private authService: AuthenticationService,
    private dialogRef: MatDialogRef<AuthenticationComponent>,
    private router: Router
  ) {}

  username: string = '';
  password: string = '';
  loginError: boolean = false;
  loginErrorMessage: string = '';

  async performLogin() {
    try {
      const loginSuccessful = await this.authService.login(this.username, this.password);
  
      if (loginSuccessful) {
        this.dialogRef.close(true);
      } else {
        this.loginError = true;
        this.loginErrorMessage = 'Identifiant ou mot de passe incorrect';
      }
    } catch (error) {
      console.error('Erreur lors de la connexion :', error);
      this.loginError = true;
      this.loginErrorMessage = 'Une erreur est survenue. Veuillez réessayer.';
    }
  }

  closeLoginForm() {
    this.dialogRef.close(false);
  }

  
}
