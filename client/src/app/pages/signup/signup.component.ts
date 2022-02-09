import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: SignupService
  ) {}

  ngOnInit(): void {
  }

  send(){
    this.authService.signUp(this.form.value)
      .subscribe();
  }

}
