import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

//ng serve -o
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  work: string = "";
  updateWork:string = "";
  updateIndex:number = 0;

  isUpdateFormActive: boolean = false;

  todos: string[] = [
    "asdasd",
    "12312312",
    "azxczxczxc"
  ];

  save() {
    this.todos.push(this.work);
    this.work = "";
  }

  get(index:number){
    this.updateWork = this.todos[index];
    this.updateIndex = index;
    this.isUpdateFormActive = true;
  }

  update(){
    this.todos[this.updateIndex] = this.updateWork;
    this.updateWork = "";   
    this.isUpdateFormActive = false; 
  }

  remove(index:number){
    this.todos.splice(index,1);
  }
}