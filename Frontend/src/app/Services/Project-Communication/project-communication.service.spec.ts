import { TestBed } from '@angular/core/testing';

import { ProjectCommunicationService } from './project-communication.service';

describe('ProjectCommunicationService', () => {
  let service: ProjectCommunicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectCommunicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
