import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core';
import { RoleAuthGuardService } from './core/guards/role-auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import {
  AuthLayoutComponent,
  HeaderComponent,
  HomeLayoutComponent,
} from './shared';

const routes: Routes = [
  {
    path: '',
    component: HomeLayoutComponent,
    children: [{ path: '', component: HomeComponent }],
  },
  {
    path: 'auth',
    component: AuthLayoutComponent,
    loadChildren: () =>
      import(`./auth/auth.module`).then((module) => module.AuthModule),
  },
  {
    path: 'dashboard',
    component: HeaderComponent,
    canActivate: [AuthGuard],
    children: [{ path: '', component: DashboardComponent }],
  },
  // {
  //   path: 'admin',
  //   loadChildren: () =>
  //     import(`./auth/auth.module`).then((module) => module.AuthModule),
  //   canActivate:[RoleAuthGuardService],
  //   data:{
  //     Role: 'Admin'
  //   }
  // },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', redirectTo: 'not-found' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
