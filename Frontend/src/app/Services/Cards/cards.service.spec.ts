// TODO : CE SONT DES FICHIERS DE TESTS UNITAIRES GENEREES PAR NG AUTOMATIQUEMENT

import { TestBed } from '@angular/core/testing';

import { CardsService } from '../Cards/cards.service';

describe('CardsService', () => {
  let service: CardsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CardsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
