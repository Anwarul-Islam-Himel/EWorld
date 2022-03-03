import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IToken } from 'src/app/Models/token.model';
import { AuthService } from '../auth.service';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.maxLength(32)]]
  });

  errorText = null;
  ShowPass: boolean = false;
  ShowEye: boolean = false;

  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  
  }
  send(){
    if(this.form.valid){
      this.authService.signIn(this.form.value)
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
    else{
      this.errorText = " ";
    }
  }
  signup(){
    this.router.navigate(['/signup']);
  }
  showPassword() {
    this.ShowPass = !this.ShowPass;
    this.ShowEye = !this.ShowEye;
  }
}
