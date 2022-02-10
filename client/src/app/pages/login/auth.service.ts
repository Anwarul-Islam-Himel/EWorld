import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string;
  private userToken: BehaviorSubject<string> = new BehaviorSubject<string>(null);


  constructor(private http:HttpClient) { 
    this.apiUrl = environment.apiUrl + '/api/Auth';
  }
  get getToken(): string {
    return this.userToken.value;
  }
  get isLoggedIn(): boolean {
    return localStorage.getItem('userToken') ? true : false;
  }
  saveToken(token: string) {
    localStorage.setItem('userToken', token);
    this.userToken = new BehaviorSubject<string>(token);
  }
}
