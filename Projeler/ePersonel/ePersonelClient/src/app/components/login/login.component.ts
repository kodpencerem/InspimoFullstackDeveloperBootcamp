import { Component, ElementRef, ViewChild } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule, NgForm } from '@angular/forms';
import { ValidateDirective } from '../../directives/validate.directive';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';
import { SwalService } from '../../services/swal.service';
import { HttpService } from '../../services/http.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,ValidateDirective, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  @ViewChild("password") passwordEl: ElementRef<HTMLInputElement> | undefined 

  data: LoginModel = new LoginModel();
  isShowPassword: boolean = false; 

  constructor(
    public http: HttpService, 
    private router: Router,
    private swal: SwalService
  ){}

  signIn(form: NgForm){
    if(form.valid){
      this.http.post("Auth/Login", this.data,(res)=> {
        localStorage.setItem("response", JSON.stringify(res))
        this.router.navigateByUrl("/");
        this.swal.callToast("Login is successful");
      });    
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
