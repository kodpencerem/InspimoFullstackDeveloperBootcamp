import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgxPaginationModule],
  template: `
  <h1>Personel List</h1>

  <table>
    <thead>
      <tr>
        <th>#</th>
        <th>First Name</th>
        <th>Last Name</th>
        <th>Starting Date</th>
      </tr>
    </thead>
    <tbody>
      @for (personel of personels | paginate: { itemsPerPage: 10, currentPage: p }; track personel) {
        <tr>
        <th>{{(($index + ((p -1) * 10)) + 1)}}</th>
          <td>{{personel.firstName}}</td>
          <td>{{personel.lastName}}</td>
          <td>{{personel.startingDate}}</td>
        </tr>
      }
    </tbody>
  </table>

  <pagination-controls (pageChange)="p = $event"></pagination-controls>
  
  
  `
  
})
export class AppComponent {
  p: number = 1;
  personels: {firstName: string, lastName: string, startingDate:string}[] = [];
  constructor(private http: HttpClient){
    this.getAll();
  }

  getAll(){
    this.http.get("https://localhost:7052/api/Personels/GetAll")
      .subscribe((res:any)=> {
        this.personels = res;
      })
  }
}
