import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TodoModel } from './models/todo.model';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  todos: TodoModel[] = [];

  createModel: TodoModel = new TodoModel();
  updateModel: TodoModel = new TodoModel();

  isUpdateFormActive: boolean = false;

  constructor(private http: HttpClient) {
    this.getAll();
  }

  save() {
    this.http.post("http://localhost:5500/todos", this.createModel).subscribe(() => {
      this.getAll();
      this.createModel = new TodoModel();
    });
  }

  getAll() {
    this.http.get<TodoModel[]>("http://localhost:5500/todos").subscribe((res) => this.todos = res);
  }

  deleteById(id: string | undefined) {
    this.http.delete(`http://localhost:5500/todos/${id}`).subscribe(() => this.getAll());
  }

  get(model: TodoModel) {
    this.updateModel = { ...model }; //params operatörü ... {} [...todos]
    this.isUpdateFormActive = true;
  }

  update() {
    this.http.put(`http://localhost:5500/todos/${this.updateModel.id}`, this.updateModel).subscribe({
      next: () => {
        this.getAll();
        this.isUpdateFormActive = false;
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }

  cancel() {
    this.isUpdateFormActive = false;
  }
} 
