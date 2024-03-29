import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

//ng serve -o
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
work: string = "";
todos:string[] = [];

save(){
  console.log(this.work);
  this.todos.push(this.work);
  this.work = "";
}
}