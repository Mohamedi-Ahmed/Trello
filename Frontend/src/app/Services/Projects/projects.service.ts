import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private baseUrl = 'https://back-trellolite.azurewebsites.net/projects';
  //private baseUrl = 'http://localhost:5111/projects';

  constructor(private http: HttpClient) {}

  // Méthode pour récupérer tous les projets
  getProjects(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  getProjectsByUserId(userId: string): Observable<any[]> {
    const url = `${this.baseUrl}/user/${userId}`;
    return this.http.get<any[]>(url);
}

  // Méthode pour créer un nouveau projet
  createProject(newProject: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, newProject);
  }

  // Méthode pour mettre à jour un projet existant
  updateProject(projectId: number, updatedProject: any): Observable<any> {
    const url = `${this.baseUrl}/${projectId}`;
    return this.http.put<any>(url, updatedProject);
  }

  // Méthode pour supprimer un projet
  deleteProject(projectId: string): Observable<any> {
    const url = `${this.baseUrl}/${projectId}`;
    return this.http.delete(url);
  }
}
