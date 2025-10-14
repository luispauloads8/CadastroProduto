import { ApplicationConfig, importProvidersFrom, RendererFactory2 } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';

import { ɵDomRendererFactory2 } from '@angular/platform-browser';

import { provideToastr, ToastrModule } from 'ngx-toastr';
import { errorInterceptor } from './interceptors/error.interceptor';
import { authInterceptor } from './interceptors/auth.interceptor';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { provideNgxMask } from 'ngx-mask';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(), provideToastr({
    timeOut: 3000,
    positionClass: 'toast-bottom-right',
    preventDuplicates: true,
    progressBar: true
  }), 
  { provide: RendererFactory2, useClass: ɵDomRendererFactory2 },
    provideHttpClient(
      withFetch(), // Habilita o uso da API fetch
      withInterceptors([
        authInterceptor,    // Seu interceptor de autenticação
        errorInterceptor    // Seu interceptor de erro
      ])
    ),
    importProvidersFrom(BrowserAnimationsModule), // Importando animações
    importProvidersFrom(BrowserModule),
    importProvidersFrom(ToastrModule),
    provideToastr(),
    provideAnimations(),
    provideNgxMask(),
    provideAnimations(), // Necessário para animações do Toastr
    provideToastr({
      timeOut: 5000,
      positionClass: 'toast-top-right', // Aqui você define a posição!
      preventDuplicates: true,
      // ... outras configurações
    }),
]
};