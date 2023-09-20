import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { CarDetailsComponent } from './components/car-details/car-details.component';
import { MyAgreementsComponent } from './components/my-agreements/my-agreements.component';
import { EditAgreementComponent } from './components/edit-agreement/edit-agreement.component';
import { CarEditComponent } from './components/car-edit/car-edit.component';
import { CarCreateComponent } from './components/car-create/car-create.component';
import { AuthUserGuard } from './guards/auth-user.guard';
import { AuthAdminGuard } from './guards/auth-admin.guard';
// import { EditAgreementAdminComponent } from './components/edit-agreement-admin/edit-agreement-admin.component';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'home',
    canActivate:[AuthUserGuard],
    component: UserHomeComponent,
  },
  {
    path: 'admin_home',
    canActivate:[AuthAdminGuard],
    component: AdminHomeComponent,
  },
  {
    //agreement creating form usersonly
    path: 'carDetail/:id',
    canActivate:[AuthUserGuard],
    component: CarDetailsComponent,
  },
  {
    path: 'myAgreements',
    canActivate:[AuthUserGuard],
    component: MyAgreementsComponent,
  },
  {
    path: 'editAgreement/:id',
    component: EditAgreementComponent,
  },
  {
    path: 'admin-carlist',
    canActivate:[AuthAdminGuard],
    component: UserHomeComponent,
  },
  {
    path: 'admin-edit-car/:id',
    canActivate:[AuthAdminGuard],
    component: CarEditComponent,
  },
  {
    path: 'admin-create-car',
    canActivate:[AuthAdminGuard],
    component: CarCreateComponent,
  },
  {
    path: '**',
    component: LoginComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
