import { CanActivateChildFn } from '@angular/router';
import { canactivateGuard } from './canactivate.guard';

export const canactivatechildGuard: CanActivateChildFn = (childRoute, state) => {
  return canactivateGuard(childRoute,state);
};
