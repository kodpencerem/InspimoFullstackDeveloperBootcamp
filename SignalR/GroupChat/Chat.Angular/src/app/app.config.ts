import { ApplicationConfig, provideExperimentalZonelessChangeDetection, provideZoneChangeDetection, ɵinternalProvideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),
    provideRouter(routes),
    provideHttpClient()
  ]
};
