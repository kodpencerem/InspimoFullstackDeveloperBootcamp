import { Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { api } from '../../constants';
import { lastValueFrom } from 'rxjs';
import { ResultModel } from '../../models/result.model';

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

  constructor(
    private http: HttpClient,
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

  async signIn(){
    const res = await lastValueFrom(this.http.post<ResultModel<string>>(`${api}/Auth/Login`, this.model()));

    localStorage.setItem("access-token",res.data!);
    this.router.navigateByUrl("/");
  }
}
