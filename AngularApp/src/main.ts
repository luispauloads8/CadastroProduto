import { bootstrapApplication, BrowserModule } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideToastr, ToastrModule } from 'ngx-toastr';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { provideNgxMask } from 'ngx-mask';
import { authInterceptor } from './app/interceptors/auth.interceptor';


  bootstrapApplication(AppComponent, {
    providers: [
      provideRouter(routes),
      importProvidersFrom(BrowserAnimationsModule), // Importando animações
      importProvidersFrom(HttpClientModule),
      importProvidersFrom(BrowserModule),
      importProvidersFrom(ToastrModule), 
      provideToastr(),
      provideAnimations(),
      provideNgxMask(),
      provideHttpClient(withInterceptors([authInterceptor])),
      provideHttpClient(withFetch()) // Habilita o uso da API fetch
    ],
  }).catch((err) => console.error(err));
