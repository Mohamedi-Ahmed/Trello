import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class RegistrationService {
  private baseUrl = 'https://back-trellolite.azurewebsites.net/users'; 

  constructor(private http: HttpClient) {}

  checkNicknameAvailability(nickname: string): Observable<any> {
    const apiUrl = `${this.baseUrl}/checknickname/${nickname}`;
    return this.http.get(apiUrl);
  }

  checkEmailAvailability(email: string): Observable<any> {
    const apiUrl = `${this.baseUrl}/checkemail/${email}`;
    return this.http.get(apiUrl);
  }

  registerUser(formData: any): Observable<any> {
    return this.http.post(this.baseUrl + '/register', formData);
  }
}

