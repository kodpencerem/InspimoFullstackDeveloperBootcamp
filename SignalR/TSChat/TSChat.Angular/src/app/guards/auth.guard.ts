import { inject } from '@angular/core';
import { CanActivateChildFn, Router } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';

export const authGuard: CanActivateChildFn = (childRoute, state) => {
  if(localStorage.getItem("access-token")){
    return true;
  }  

  const router = inject(Router);
  const toast = inject(FlexiToastService);

  router.navigateByUrl("/login");
  toast.showToast("Warning","If you want to continue, you need sign in!", "warning");
  return false;
};
