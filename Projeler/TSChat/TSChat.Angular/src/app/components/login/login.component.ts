import { Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';
import { FormsModule } from '@angular/forms';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class LoginComponent {
  showPassword = signal<boolean>(false);
  @ViewChild("passwordInput") passwordInput: ElementRef<HTMLInputElement> | undefined;

  model = signal<UserModel>(new UserModel());
  isLoading = signal<boolean>(false);

  constructor(
    private http: HttpService,
    private router: Router
  ){}

  showOrHidePassword() {
    if (this.passwordInput?.nativeElement.type === "password") {
      this.passwordInput?.nativeElement.setAttribute("type", "text");
      this.showPassword.set(true);
    } else {
      this.passwordInput?.nativeElement.setAttribute("type", "password");
      this.showPassword.set(false);
    }
  }

  signIn(){
    this.isLoading.set(true);
    this.http.post<string>(`Auth/Login`, this.model(), (res)=> {
      localStorage.setItem("access-token",res);
      this.router.navigateByUrl("/");
    },()=> this.isLoading.set(false));    
  }
}
