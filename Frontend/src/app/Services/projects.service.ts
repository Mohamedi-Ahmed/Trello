// Les différents imports
import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';

//Mon interface
export interface Project{
  id: string,
  name: string,
  description : string | undefined,
  dateCreation : Date
}

@Injectable({
  providedIn: 'root'
})

export class ProjectsService {
  constructor( private http: HttpClient) {
    this.getProjects().subscribe((projects: any) => {
      this.projects = projects;
    });
   }
   projects = this.getProjects();

   getProjects(){
      return this.http.get("http://localhost:5111/projects")
   }


}
