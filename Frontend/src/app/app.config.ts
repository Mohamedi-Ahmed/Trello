import { ApplicationConfig, importProvidersFrom } from '@angular/core';
// permet d'intégrer la configuration des routes dans le système de dépendances de l'application
import { provideRouter } from '@angular/router';
// Importe la configuration des routes
import { routes } from './app.routes';
//permet de faire des requêtes HTTP dans l'application
import { HttpClientModule } from '@angular/common/http';

// rend les routes disponibles dans toute l'application.
// + permet de faire des requêtes HTTP dans l'application.
export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), importProvidersFrom(HttpClientModule)],
};

