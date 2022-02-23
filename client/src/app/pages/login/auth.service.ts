import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IUser } from 'src/app/Models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string;
  private userToken: BehaviorSubject<string> = new BehaviorSubject<string>(null);


  constructor(private http:HttpClient) { 
    this.apiUrl = environment.apiUrl + '/api/Auth';
    const token = localStorage.getItem('userToken');
    if(token){
        this.userToken = new BehaviorSubject<string>(token);
    }
  }
  get getToken(): string {
    return this.userToken.value;
  }
  get isLoggedIn(): boolean {
    return localStorage.getItem('userToken') ? true : false;
  }
  signIn(model: IUser){
    return this.http.post(this.apiUrl + '/login', model);
  }
  saveToken(token: string) {
    localStorage.setItem('userToken', token);
    this.userToken = new BehaviorSubject<string>(token);
  }
  logOut() {
    localStorage.clear();
  }
}
