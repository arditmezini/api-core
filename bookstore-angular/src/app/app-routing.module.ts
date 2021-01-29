import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleAuthGuardService } from './core/guards/role-auth-guard.service';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'auth',
    loadChildren: () =>
      import(`./auth/auth.module`).then((module) => module.AuthModule),
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
  { path: "**", redirectTo: "not-found" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
