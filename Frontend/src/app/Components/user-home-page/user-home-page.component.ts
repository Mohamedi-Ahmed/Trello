import { Component, OnInit, Input} from '@angular/core';
import { SidebarProjectComponent } from '../Sidebar-Projects/sidebar-projects.component';
import { ListsComponent } from '../Lists/lists.component'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectCommunicationService } from '../../Services/Project-Communication/project-communication.service';

@Component({
  selector: 'app-user-home-page',
  templateUrl: './user-home-page.component.html',
  styleUrls: ['./user-home-page.component.css'],
  standalone: true,
  imports: [SidebarProjectComponent, ListsComponent, CommonModule, FormsModule]
})

export class UserHomePageComponent implements OnInit {

  @Input() projectName: string | undefined;
  @Input() projectId: number | undefined;
  @Input() projectDescription: string | undefined;
  
  isProjectSelected: boolean = true;
  selectedProject: { id: number; name: string; description: string;} | undefined;

  constructor(private projectCommunicationService: ProjectCommunicationService) {}

  ngOnInit(): void {
    this.projectCommunicationService.projectDeleted$.subscribe((projectId) => {
      this.isProjectSelected = false;
    });
  }

  onProjectSelect(project: { name: string, id: number, description: string }) {
    this.selectedProject = project;
    this.isProjectSelected = true;
  }
}