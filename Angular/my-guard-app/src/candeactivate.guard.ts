import { CanDeactivateFn } from '@angular/router';
import { HomeComponent } from './app/home/home.component';

export const canDeactivateGuard: CanDeactivateFn<HomeComponent> = (component, currentRoute, currentState, nextState) => {
  return confirm("Home componentden çıkmak üzeresin, devam edilsin mi?");
};
