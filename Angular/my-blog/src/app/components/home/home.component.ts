import { Component, signal } from '@angular/core';
import { TranslocoModule } from '@jsverse/transloco';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [TranslocoModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  blogs = signal<number[]>([1,2,3,4,5,6]);
}
