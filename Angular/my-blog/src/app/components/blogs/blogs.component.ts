import { Component, signal } from '@angular/core';
import { TranslocoModule } from '@jsverse/transloco';

@Component({
  selector: 'app-blogs',
  standalone: true,
  imports: [TranslocoModule],
  templateUrl: './blogs.component.html',
  styleUrl: './blogs.component.css'
})
export class BlogsComponent {
  blogs = signal<number[]>([1,2,3,4,5,6]);
}
