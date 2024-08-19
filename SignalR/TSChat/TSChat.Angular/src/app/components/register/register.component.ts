import { AfterViewInit, Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserModel } from '../../models/user.model';
import { HttpClient } from '@angular/common/http';
import { ResultModel } from '../../models/result.model';
import { FlexiToastService } from 'flexi-toast';
import { lastValueFrom } from 'rxjs';
import { api, professions } from '../../constants';
import { FlexiSelectModule } from 'flexi-select';

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

  constructor(
    private http: HttpClient,
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

  async signUp(){
    const formData = new FormData();
    formData.append("firstName", this.model().firstName);
    formData.append("lastName", this.model().lastName);
    formData.append("userName", this.model().userName);
    formData.append("password", this.model().password);
    formData.append("profession", this.model().profession);
    if(this.model().file){
      formData.append("file", this.model().file,this.model().file.name);
    }

    const res = await lastValueFrom(this.http.post<ResultModel<string>>(`${api}/Auth/Register`,formData));

    this.toast.showToast("Success",res.data!);
    this.router.navigateByUrl("/login");
  }

  setFile(event:any){
    const file = event.target.files[0];

    this.model().file = file;    
  }
}
