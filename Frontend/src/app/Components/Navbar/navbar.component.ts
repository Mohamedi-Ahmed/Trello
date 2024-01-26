import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthenticationService } from '../../Services/Authentications/authentication.service';
import { Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {
  @Output() sectionNavigate: EventEmitter<string> = new EventEmitter();
  @Output() loginClicked: EventEmitter<void> = new EventEmitter<void>();

  isAuthenticated: boolean = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.getUserId().subscribe((userId: number) => {
      this.isAuthenticated = userId !== -1;
      console.log("Connecté ? : " + this.isAuthenticated);
    });
  }

  navigateToSection(sectionId: string) {
    this.sectionNavigate.emit(sectionId);
  }

  refreshPageandNavigateToHome() {
    localStorage.setItem('targetSection', 'accueil-inscription');
    window.location.reload();
  }

  emitLoginEvent() {
    this.loginClicked.emit();
  }

  // Fonction de déconnexion
  logout() {
    this.authService.logout();
    this.router.navigate(['/']); // Rediriger vers la page d'accueil
  }

}
