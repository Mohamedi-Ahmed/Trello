import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  private baseUrl = 'https://back-trellolite.azurewebsites.net/comments';
  //private baseUrl = 'http://localhost:5111/comments';

  constructor(private http: HttpClient) { }

  getAllComments(cardId: number): Observable<any> {
    const url = `${this.baseUrl}/card/${cardId}`;
    return this.http.get(url);
  }

  getCommentsByCardId(cardId: number): Observable<any> {
    const url = `${this.baseUrl}/bycard/${cardId}`;
    return this.http.get(url);
  }

  createComment(newComment: any): Observable<any> {
    const url = `${this.baseUrl}`;
    return this.http.post(url, newComment);
  }

  updateComment(commentId: number, updatedComment: any): Observable<any> {
    const url = `${this.baseUrl}/${commentId}`;
    return this.http.put(url, updatedComment);
  }

  deleteComment(commentId: number): Observable<any> {
    const url = `${this.baseUrl}/${commentId}`;
    return this.http.delete(url);
  }
}
