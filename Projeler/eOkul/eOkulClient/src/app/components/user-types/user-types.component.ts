import { Component, OnInit, signal } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { BreadCrumbModel } from '../blank/blank.component';
import { UserTypeModel } from '../../models/user-type.model';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-user-types',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './user-types.component.html',
  styleUrl: './user-types.component.css'
})
export default class UserTypesComponent implements OnInit {
  breadCrumbs = signal<BreadCrumbModel[]>([
    {
      name: "Ana Sayfa",
      class: "",
      route: "/admin"
    },
    {
      name: "Kullanıcı Tipleri",
      class: "active",
      route: "/admin/user-types"
    }
  ]);
  userTypes = signal<UserTypeModel[]>([]);
  isLoading = signal<boolean>(false);

  constructor(
    private http: HttpService
  ){}

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.isLoading.set(true);
    this.http.get<UserTypeModel[]>("UserTypes/GetAll",(res)=> {
      this.userTypes.set(res);
      this.isLoading.set(false);
    },()=> this.isLoading.set(false));
  }
}
