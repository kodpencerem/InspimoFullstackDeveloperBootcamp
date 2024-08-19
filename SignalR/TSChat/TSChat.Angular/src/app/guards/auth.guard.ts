import { inject } from '@angular/core';
import { CanActivateChildFn, Router } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';
import { jwtDecode } from 'jwt-decode';
import { SharedService } from '../services/shared.service';
import { mainUrl } from '../constants';

export const authGuard: CanActivateChildFn = (childRoute, state) => {
  const router = inject(Router);
  const toast = inject(FlexiToastService);
  if(localStorage.getItem("access-token")){
    try {
      const decode:any = jwtDecode(localStorage.getItem("access-token")!);
      const shared = inject(SharedService);

      shared.user.fullName = decode["FullName"];
      shared.user.userName = decode["UserName"];
      shared.user.avatar =  `${mainUrl}/avatars/${decode["Avatar"]}`;
      shared.user.profession = decode["Profession"];
      shared.user.id = decode["UserId"];

    } catch (error) {
       router.navigateByUrl("/login");
       toast.showToast("Warning","If you want to continue, you need sign in!", "warning");
       return false;
    }
    



    return true;
  }  

  

  router.navigateByUrl("/login");
  toast.showToast("Warning","If you want to continue, you need sign in!", "warning");
  return false;
};