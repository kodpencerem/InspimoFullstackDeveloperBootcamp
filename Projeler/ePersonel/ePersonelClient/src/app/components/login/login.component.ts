import { Component, ElementRef, ViewChild } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule, NgForm } from '@angular/forms';
import { ValidateDirective } from '../../directives/validate.directive';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,ValidateDirective],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  @ViewChild("password") passwordEl: ElementRef<HTMLInputElement> | undefined

  data: LoginModel = new LoginModel();
  isShowPassword: boolean = false; 

  constructor(private http: HttpClient, private router: Router){}

  signIn(form: NgForm){
    if(form.valid){
      this.http.post("https://localhost:7052/api/Auth/Login",this.data).subscribe({
        next: (res:any)=> {
          localStorage.setItem("response",JSON.stringify(res))
          this.router.navigateByUrl("/");
        },
        error: (err: HttpErrorResponse)=> {
          console.log(err);          
        }
      })
    }
  }

  changeShowPassword(){
    this.isShowPassword = !this.isShowPassword;

    if(this.isShowPassword){
      this.passwordEl!.nativeElement.type = "text";
    }else{
      this.passwordEl!.nativeElement.type = "password";
    }
  }
}
