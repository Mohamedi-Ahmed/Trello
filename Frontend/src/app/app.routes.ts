import { Routes } from '@angular/router';
import { UserHomePageComponent } from './Components/User-home-page/user-home-page.component';
import { AppComponent } from './Components/App/app.component';

export const routes: Routes = [
  {
    path: 'HomePage',
    component: UserHomePageComponent,
  }
];