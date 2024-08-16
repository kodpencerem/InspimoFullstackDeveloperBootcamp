import { Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';
import { FormsModule } from '@angular/forms';

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

  }
}
