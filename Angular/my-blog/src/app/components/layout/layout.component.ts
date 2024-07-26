import { Component, signal } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { TranslocoModule, TranslocoService } from '@jsverse/transloco';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, TranslocoModule, RouterLink, RouterLinkActive],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  lang = signal<string>("");

  constructor(){
    this.lang.set(localStorage.getItem("lang") ?? "tr");
  }

  setLang(lang: string) {
    localStorage.setItem("lang", lang);
    location.reload();
  }
}
