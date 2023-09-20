import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { ToastrModule } from 'ngx-toastr';
import {FormsModule,ReactiveFormsModule} from '@angular/forms'
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { CarDetailsComponent } from './components/car-details/car-details.component';
import { AuthService } from './services/auth.service';
import { TokenInterceptor } from './interceptors/token-interceptor.interceptor';
import { MyAgreementsComponent } from './components/my-agreements/my-agreements.component';
import { EditAgreementComponent } from './components/edit-agreement/edit-agreement.component';
import { CarListComponent } from './components/car-list/car-list.component';
import { CarEditComponent } from './components/car-edit/car-edit.component';
import { CarCreateComponent } from './components/car-create/car-create.component';
// import { EditAgreementAdminComponent } from './components/edit-agreement-admin/edit-agreement-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavbarComponent,
    UserHomeComponent,
    AdminHomeComponent,
    CarDetailsComponent,
    MyAgreementsComponent,
    EditAgreementComponent,
    CarListComponent,
    CarEditComponent,
    CarCreateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [AuthService,{provide:HTTP_INTERCEPTORS,useClass:TokenInterceptor,multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
