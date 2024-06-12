import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  isLoading: boolean = false;

  constructor(
    private http: HttpClient,
    private swal: SwalService
  ) {}

  post<T>(api: string, body: any, callBack: (res:T) => void) {
    this.isLoading = true;
    this.http.post<T>(`https://localhost:7052/api/${api}`, body).subscribe({
      next: (res) => {
        callBack(res);
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading = false;
        console.log(err);
        if (err.status === 400) {
          this.swal.callToast(err.error.message, "error");
        }
        else if (err.status === 401) {
          this.swal.callToast("You are not authorized!", "error");
        }
        else {
          this.swal.callToast("Someting went wrong!", "error");
        }
      }
    })
  }
}
