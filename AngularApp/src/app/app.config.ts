import { ApplicationConfig, RendererFactory2 } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';

import { ɵDomRendererFactory2 } from '@angular/platform-browser';

import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(), provideHttpClient(),  provideToastr({
    timeOut: 3000,
    positionClass: 'toast-bottom-right',
    preventDuplicates: true,
    progressBar: true
  }), 
  { provide: RendererFactory2, useClass: ɵDomRendererFactory2 }
]
};