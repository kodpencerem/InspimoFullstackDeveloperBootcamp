import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  constructor(private http: HttpClient) { }

  async apiRequest() {
    // this.http.get<any>("https://jsonplaceholder.typicode.com/todos").subscribe({
    //   next: (res)=> {
    //     console.log(res);        
    //   },
    //   error: (err: HttpErrorResponse)=> {
    //     console.log(err);        
    //   }
    // });
    const data = await fetch("https://jsonplaceholder.typicode.com/todos").then(res => res.json());
    console.log(data);

    console.log("deneme");


    //this.http.get<any>("https://jsonplaceholder.typicode.com/todos").subscribe(res => console.log(res));
  }
}
