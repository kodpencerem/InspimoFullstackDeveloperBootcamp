import { Component } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  data: LoginModel = new LoginModel();

  signIn(){
    console.log(this.data);
    
  }
}
