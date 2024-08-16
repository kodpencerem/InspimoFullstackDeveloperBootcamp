import { Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class RegisterComponent {
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

  signUp(){
    
  }
}
