import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/login/login/login.module').then(m => m.LoginModule)
  },
  { 
    path: 'home',
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) 
  },
  { 
    path: 'signup', 
    loadChildren: () => import('./pages/signup/signup.module').then(m => m.SignupModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
