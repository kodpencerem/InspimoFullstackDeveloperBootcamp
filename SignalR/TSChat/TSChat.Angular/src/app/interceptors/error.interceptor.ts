import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { FlexiToastService } from 'flexi-toast';
import { catchError, of } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(FlexiToastService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse)=> {
      console.log(err);
      
      if(err.status === 400){
        toast.showToast("Error", "One or more validation errors occurred.", "error");
      }
      else if(err.status === 500){
        for(const msg of err.error.errorMessages){
          toast.showToast("Error",msg,"error");
        }
      }else if(err.status === 0){
        toast.showToast("Error","We cannot reach the API address", "error")
      }else if(err.status === 404){
        toast.showToast("Error","We cannot find the API address", "error")
      }
      
      return of();
    })
  );
};
