import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { api } from '../constants';
import { ResultModel } from '../models/result.model';
import { FlexiToastService } from 'flexi-toast';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  token: string = "";

  constructor(
    private http: HttpClient,
    private toast: FlexiToastService
  ) { }


  get<T>(endpoint: string, callBack: (res: T) => void, errorCallBack?: ()=> void) {

    this.token = localStorage.getItem("access-token") ?? "";

    this.http.get<ResultModel<T>>(`${api}/${endpoint}`,{
      headers: {
        "Authorization": `Bearer ${this.token}`
      }
    }).subscribe({
      next: (res) => {
        callBack(res.data!);
      },
      error: (err: HttpErrorResponse) => {
        this.errorHandler(err);

        if(errorCallBack){
          errorCallBack();
        }
      }
    })
  }

  post<T>(endpoint: string, body: any,callBack: (res: T) => void, errorCallBack?: ()=> void) {
    this.token = localStorage.getItem("access-token") ?? "";
    
    this.http.post<ResultModel<T>>(`${api}/${endpoint}`, body,{
      headers: {
        "Authorization": `Bearer ${this.token}`
      }
    }).subscribe({
      next: (res) => {
        callBack(res.data!);
      },
      error: (err: HttpErrorResponse) => {
        this.errorHandler(err);

        if(errorCallBack){
          errorCallBack();
        }
      }
    })
  }

  errorHandler(err: HttpErrorResponse) {
    console.log(err);

    if (err.status === 400) {
      this.toast.showToast("Error", "One or more validation errors occurred.", "error");
    }
    else if (err.status === 500) {
      for (const msg of err.error.errorMessages) {
        this.toast.showToast("Error", msg, "error");
      }
    } else if (err.status === 0) {
      this.toast.showToast("Error", "We cannot reach the API address", "error")
    } else if (err.status === 404) {
      this.toast.showToast("Error", "We cannot find the API address", "error")
    }
  }
}
