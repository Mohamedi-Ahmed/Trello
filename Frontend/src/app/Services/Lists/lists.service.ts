import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ListsService {

  private baseUrl = 'https://back-trellolite.azurewebsites.net/lists';
  //private baseUrl = 'http://localhost:5111/lists';
  
  constructor(private http: HttpClient) { }

  getAllLists(): Observable<any> {
    return this.http.get(this.baseUrl);
  }

  getListsByProjectId(projectId: number): Observable<any> {
    const url = `${this.baseUrl}/project/${projectId}`;
    return this.http.get(url);
  }

  createList(newList: any): Observable<any> {
    return this.http.post(this.baseUrl, newList);
  }

  updateList(listId: number, updatedList: any): Observable<any> {
    const url = `${this.baseUrl}/${listId}`;
    return this.http.put(url, updatedList);
  }

  deleteList(listId: number): Observable<any> {
    const url = `${this.baseUrl}/${listId}`;
    return this.http.delete(url);
  }
}
