import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './login/auth.service';
import { DashboardService } from './dashboard/dashboard.service';
import { SharedModule } from './shared/shared.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthInterceptor } from './auth.interceptor';
import { RegisterComponent } from './register/register.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MaterialModule } from './material.module';
import { StoreModule } from '@ngrx/store';
import { userReducer } from './shared/auth.store';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { SharedMaterialModule } from './shared/modules/shared-material.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    RegisterComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    SharedMaterialModule,
    ToastrModule.forRoot(),
    StoreModule.forRoot({
      user: userReducer
    }),
    MaterialModule,
    SidebarComponent 
  ],
  providers: [AuthService, DashboardService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
