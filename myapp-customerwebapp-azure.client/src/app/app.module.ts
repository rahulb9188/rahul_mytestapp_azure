import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
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
import { MaterialModule } from './material.module';
import { Store, StoreModule } from '@ngrx/store';
import { SharedMaterialModule } from './shared/modules/shared-material.module';
import { authReducer } from './shared/store/auth.reducer';
import { AuthEffects } from './shared/store/auth.effects';
import { EffectsModule } from '@ngrx/effects';
import { checkAuth } from './shared/store/auth.actions';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';

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
    StoreModule.forRoot({ auth: authReducer }),
    EffectsModule.forRoot([AuthEffects]),
    MaterialModule,
    SidebarComponent
  ],
  providers: [AuthService, DashboardService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
