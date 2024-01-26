import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProjectCommunicationService {

  // Suppression de projets
  private projectDeletedSubject = new Subject<number>();
  projectDeleted$ = this.projectDeletedSubject.asObservable();

  emitProjectDeleted(projectId: number) {
    this.projectDeletedSubject.next(projectId);
  }

  // Update titre & description des projets
  private projectNameUpdatedSubject = new Subject<{ projectId: number; newName: string }>();
  private projectInfoUpdatedSubject = new Subject<{ projectId: number; updatedProject: { name: string, description: string } }>();

  emitProjectNameUpdated(projectId: number, newName: string) {
    this.projectNameUpdatedSubject.next({ projectId, newName });
  }
  
  emitProjectInfoUpdated(projectId: number, updatedProject: { name: string, description: string }) {
    this.projectInfoUpdatedSubject.next({ projectId, updatedProject });
  }
}
