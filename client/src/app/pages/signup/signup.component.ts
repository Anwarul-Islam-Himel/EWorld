import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IToken } from 'src/app/Models/token.model';
import { AuthService } from '../login/auth.service';
import { SignupService } from './signup.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  form: FormGroup = this.fb.group({
    name:['',[Validators.required,Validators.minLength(4)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.maxLength(32)]],
    confirmPassword: ['', [Validators.required, Validators.maxLength(32)]]
  });
  errorText = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private signService: SignupService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
  }

  send(){
    this.signService.signUp(this.form.value)
      .subscribe({
        next: (result: IToken) => {
          this.authService.saveToken(result.token);
          this.router.navigate(['/home']);
        },
        error: e => {
          this.errorText = e.error.message;
        }
      });
  }

}
