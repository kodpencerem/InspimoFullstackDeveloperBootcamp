import { AfterViewInit, Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';
import { FlexiToastService } from 'flexi-toast';
import { professions } from '../../constants';
import { FlexiSelectModule } from 'flexi-select';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule, FlexiSelectModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class RegisterComponent implements AfterViewInit {
  showPassword = signal<boolean>(false);
  @ViewChild("passwordInput") passwordInput: ElementRef<HTMLInputElement> | undefined;
  @ViewChild("firstNameInput") firstNameInput: ElementRef<HTMLInputElement> | undefined;

  model = signal<UserModel>(new UserModel());
  professions = signal<{id: number,value: string}[]>(professions);
  isLoading = signal<boolean>(false);

  constructor(
    private http: HttpService,
    private toast: FlexiToastService,
    private router: Router
  ){}

  ngAfterViewInit(): void {
    if(this.firstNameInput){
      this.firstNameInput.nativeElement.focus();
    }
  }

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
    const formData = new FormData();
    formData.append("firstName", this.model().firstName);
    formData.append("lastName", this.model().lastName);
    formData.append("userName", this.model().userName);
    formData.append("password", this.model().password);
    formData.append("profession", this.model().profession);
    if(this.model().file){
      formData.append("file", this.model().file,this.model().file.name);
    }

    this.isLoading.set(true);
    this.http.post<string>(`Auth/Register`,formData,(res)=> {
        this.isLoading.set(false);
        this.toast.showToast("Success",res);
        this.router.navigateByUrl("/login");        
    },()=> this.isLoading.set(false));
  }

  setFile(event:any){
    const file = event.target.files[0];

    this.model().file = file;    
  }
}
