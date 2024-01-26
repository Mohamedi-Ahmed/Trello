import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject } from 'rxjs';
import { Observable } from 'rxjs';
import { Router, NavigationEnd } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private isAuthenticated: boolean = false;
  private apiUrl: string = 'https://back-trellolite.azurewebsites.net/users';

  // ReplaySubject pour stocker l'id de l'utilisateur
  private userIdSubject: ReplaySubject<number> = new ReplaySubject<number>(1);

  constructor(
    private http: HttpClient,
    private router: Router
    ) {}

  // Méthode de connexion
  login(username: string, password: string): Promise<boolean> {
    const loginDto = {
      Username: username,
      Password: password
    };  
    return new Promise<boolean>((resolve) => {
      this.http.post<any>(`${this.apiUrl}/login`, loginDto)
        .subscribe(
          response => {
            if (response && response.success) {
              this.isAuthenticated = true;
              this.userIdSubject.next(response.userId);
              resolve(true);
            } else {
              this.isAuthenticated = false;
              resolve(false);
            }
          },
          error => {
            console.error('Erreur lors de la connexion :', error);
            this.isAuthenticated = false;
            resolve(false); 
          }
        );
    });
  }

  // Méthode de déconnexion
  logout(): void {
    if (this.isAuthenticated) {
      this.isAuthenticated = false;
      this.userIdSubject.next(-1); // Définir l'ID de l'utilisateur à -1 lors de la déconnexion
      this.router.navigate(['/']);
    }
  }

  isAuthenticatedUser(): boolean {
    return this.isAuthenticated;
  }

  getUserId(): Observable<number> {
    return this.userIdSubject.asObservable();
  }

  getUsernameById(userId: number): Observable<any> {
    const url = `${this.apiUrl}/username/${userId}`; 
    return this.http.get<string>(url);
  }
}
