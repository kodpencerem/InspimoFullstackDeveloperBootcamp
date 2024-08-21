import { Component, computed, inject, OnInit, signal, ViewEncapsulation, ChangeDetectorRef } from '@angular/core';
import { UserModel } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { mainUrl } from '../../constants';
import { UserPipe } from '../../pipes/user.pipe';
import { FormsModule } from '@angular/forms';
import { SignalRService } from '../../services/signalr.service';
import { HttpService } from '../../services/http.service';
import { formatDistanceToNow } from 'date-fns';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, UserPipe, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class HomeComponent implements OnInit {
  users = signal<UserModel[]>([]);
  total = computed(() => this.users().length);
  mainUrl = mainUrl;
  search = signal<string>("");

  #signalR = inject(SignalRService);
  #http = inject(HttpService);
  #cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.getAllUsers();
    this.#signalR.connect(() => {
      this.#signalR.builder?.on("LogoutUserInformation", (res) => {
        const user = this.users().find(p => p.id == res.id);
        if (user) {
          user.isActive = res.isActive;
          user.lastActiveDate = res.lastActiveDate;
          user.isActiveInformation = this.calculateInActiveTime(res.isActive, user.lastActiveDate!);
          this.#cdr.markForCheck();
        };
      })
    });
  }

  getAllUsers() {
    this.#http.get<UserModel[]>("Users/GetAll", (res) => {
      this.users.set(res);
      res.forEach(val => {
        val.isActiveInformation = this.calculateInActiveTime(val.isActive, val.lastActiveDate!);
      });
    });
  }

  calculateInActiveTime(isActive: boolean, dateString?: string) {
    if(!isActive){
      const date = new Date(dateString!);
      const timeAgo = formatDistanceToNow(date, { addSuffix: true });
      return timeAgo; // örneğin "20 dakika önce"
    }

    return "They are here now!";
  }
}
