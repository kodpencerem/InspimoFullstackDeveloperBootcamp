import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { api } from '../constants';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(
    private http: HttpClient,
    private error: ErrorService
  ) { }

  get<T>(apiUrl:string, callBack:(res:T) => void){    
    this.http.get<T>(`${api}/${apiUrl}`).subscribe({
      next: (res)=> {
        callBack(res);
      },
      error: (err: HttpErrorResponse)=> this.error.errorHandler(err)
    })
  }


  post<T>(apiUrl:string, body:any, callBack:(res:T)=> void){
    this.http.post<T>(`${api}/${apiUrl}`, body).subscribe({
      next: (res)=> {
        callBack(res);
      },
      error: (err: HttpErrorResponse)=> this.error.errorHandler(err)
    })
  }

  put<T>(apiUrl:string, body:any, callBack:(res:T)=> void){
    this.http.put<T>(`${api}/${apiUrl}`, body).subscribe({
      next: (res)=> {
        callBack(res);
      },
      error: (err: HttpErrorResponse)=> this.error.errorHandler(err)
    })
  }

  delete<T>(apiUrl:string, callBack:(res:T) => void){    
    this.http.delete<T>(`${api}/${apiUrl}`).subscribe({
      next: (res)=> {
        callBack(res);
      },
      error: (err: HttpErrorResponse)=> this.error.errorHandler(err)
    })
  }
}
