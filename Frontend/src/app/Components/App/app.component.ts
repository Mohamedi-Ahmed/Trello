import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { MatDialog } from '@angular/material/dialog';
import { RegistrationModalComponent } from '../Registration-modal/registration-modal.component';
import { AuthenticationComponent } from '../Authentication/authentication.component';
import { UserHomePageComponent } from '../User-home-page/user-home-page.component'
import { NavbarComponent } from '../Navbar/navbar.component'
import { AuthenticationService } from '../../Services/Authentications/authentication.service';

@Component({ 
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, UserHomePageComponent, NavbarComponent]
})

export class AppComponent implements OnInit {
  currentSection = 'accueil-inscription';
  username: string = '';
  password: string = '';
  loginErrorMsg: string = ''; 
  isAuthenticated: boolean = false; 
  userEmail: string = '';
  emailErrorMsg: string = '';

  constructor(
    public dialog: MatDialog,
    private authService: AuthenticationService 
  ) {}

  ngOnInit() {
    this.authService.getUserId().subscribe((userId: number) => {
      this.isAuthenticated = userId !== -1;
      if (!this.isAuthenticated) {
        this.scrollTo('accueil-inscription');
      }
    });
  }

  scrollTo(section: string) {
    this.currentSection = section;
    const sectionElement = document.getElementById(section);
    if (sectionElement) {
      sectionElement.scrollIntoView({ behavior: 'smooth' });
    } else {
      console.warn(`Section with ID '${section}' not found.`);
    }
  }

  setCurrentSection(section: string) {
    this.currentSection = section;
  }

  validateEmail() {
    if (!this.isValidEmail(this.userEmail)) {
      this.emailErrorMsg = "Adresse e-mail invalide.";
      return false;
    }
    this.emailErrorMsg = '';
    return true;
  }

  isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  openRegistrationForm() {
    if (!this.validateEmail()) {
      console.error("L'adresse e-mail n'est pas valide.");
      return;
    }
    this.dialog.open(RegistrationModalComponent, {
      width: '400px',
      data: { userEmail: this.userEmail },
      disableClose: false
    }).afterClosed().subscribe(() => {
      this.userEmail = '';
    });
  }

  openLoginForm() {
    this.dialog.open(AuthenticationComponent, {
      width: '400px',
      data: { },
      disableClose: false
    }).afterClosed().subscribe((result) => {
      this.isAuthenticated = result;
    });
  }
}