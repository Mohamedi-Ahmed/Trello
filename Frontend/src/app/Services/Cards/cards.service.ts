import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  private baseUrl = 'https://back-trellolite.azurewebsites.net/cards';
  //private baseUrl = 'http://localhost:5111/cards';
  
  constructor(private http: HttpClient) { }

  getAllCards(cardId: number): Observable<any> {
    const url = `${this.baseUrl}/card/${cardId}`;
    return this.http.get(url);
  }

  getCardsByListId(listId: number): Observable<any> {
    const url = `${this.baseUrl}/bylist/${listId}`;
    return this.http.get(url);
  }

  createCard(newCard: any): Observable<any> {
    return this.http.post(this.baseUrl, newCard);
  }

  updateCard(CardId: number, updatedCard: any): Observable<any> {
    const url = `${this.baseUrl}/${CardId}`;
    return this.http.put(url, updatedCard);
  }

  deleteCard(commentId: number): Observable<any> {
    const url = `${this.baseUrl}/${commentId}`;
    return this.http.delete(url);
  }
}
