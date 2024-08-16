import { Component, inject, ViewEncapsulation } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

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

  logout(){
    localStorage.removeItem("access-token");
    this.#router.navigateByUrl("/login");
  }
}
