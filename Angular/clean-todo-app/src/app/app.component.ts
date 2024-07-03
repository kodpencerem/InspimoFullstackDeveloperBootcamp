import { Component, ElementRef, signal, ViewChild } from '@angular/core';
import { TodoModel } from './models/todo.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FlexiGridModule } from 'flexi-grid';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FlexiGridModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  todos = signal<TodoModel[]>([]);
  addModel = signal<TodoModel>(new TodoModel());

  @ViewChild("addModalCloseBtn") addModalCloseBtn : ElementRef<HTMLButtonElement> | undefined;
  
  constructor(
    private http: HttpClient
  ){
    this.getAll();
  }

  getAll(){
    this.http.get<TodoModel[]>("https://localhost:7248/api/Todos/GetAll").subscribe({
      next: (res) => {
        this.todos.set(res);
      },
      error: (err: HttpErrorResponse)=> {
        console.log(err);        
      }
    })
  }

  create(){
    this.http.post("https://localhost:7248/api/Todos/Create", this.addModel()).subscribe({
      next: ()=> {
        this.getAll();
        this.addModalCloseBtn!.nativeElement.click();
        this.addModel.set(new TodoModel());
      },
      error: (err: HttpErrorResponse)=> {
        console.log(err);        
      }
    })
  }

  changeIsCompleted(model: TodoModel){
    this.http.put("https://localhost:7248/api/Todos/Update", model).subscribe({
      next: ()=> {},
      error: (err: HttpErrorResponse)=> {
        console.log(err);        
      }
    })
  }
}
