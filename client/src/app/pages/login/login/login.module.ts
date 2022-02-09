import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { MaterialsModule } from 'src/app/materials/materials.module';
import { LoginRoutingModule } from './login-routing.module';




@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    MaterialsModule,
    LoginRoutingModule
  ]
})
export class LoginModule { }
