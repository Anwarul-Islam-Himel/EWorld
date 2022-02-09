import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IRegister } from 'src/app/Models/register.model';

@Injectable({
  providedIn: 'root'
})
export class SignupService {
  private apiUrl :string;

  constructor(private http: HttpClient) { 
    this.apiUrl = environment.apiUrl + '/api/User';
  }

  signUp(model: IRegister) {
    return this.http.post(this.apiUrl + '/add-user', model);
  }
}
