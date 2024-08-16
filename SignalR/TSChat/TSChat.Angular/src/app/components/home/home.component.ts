import { Component, computed, signal, ViewEncapsulation } from '@angular/core';
import { UserModel } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { mainUrl } from '../../constants';
import { UserPipe } from '../../pipes/user.pipe';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, UserPipe, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class HomeComponent {
  users = signal<UserModel[]>([
    {
      id: "",
      firstName: "Taner",
      lastName: "Saydam",
      fullName: "Taner Saydam",
      isActive: true,
      avatar: "",
      isActiveInformation: "Right now they here!",
      profession: "Software Engineer",
      password: "",
      userName: "tsaydam"
    }
  ]);
  total = computed(()=> this.users().length);
  mainUrl = mainUrl;
  search = signal<string>("");
}
