import { Component, computed, inject, ViewEncapsulation } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-layouts',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './layouts.component.html',
  styleUrl: './layouts.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class LayoutsComponent {

  #router = inject(Router);
  #shared = inject(SharedService);

  user = computed(()=> this.#shared.user);

  logout(){
    localStorage.removeItem("access-token");
    this.#router.navigateByUrl("/login");
  }
}
