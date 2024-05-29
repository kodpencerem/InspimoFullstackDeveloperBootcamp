import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationResultModel } from './models/pagination-result.model';

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
      @for (personel of result.data; track personel) {
        <tr>
        <th>{{(($index + ((p -1) * 10)) + 1)}}</th>
          <td>{{personel.firstName}}</td>
          <td>{{personel.lastName}}</td>
          <td>{{personel.startingDate}}</td>
        </tr>
      }
    </tbody>
  </table>

  <ul class="page">
    <li class="page-part" (click)="getAll(1)">1</li>
    <li class="page-part" (click)="getAll(2)">2</li>
    <li class="page-part" (click)="getAll(3)">3</li>
    <li class="page-part" (click)="getAll(4)">4</li>
  </ul>

  <!-- <pagination-controls (pageChange)="p = $event" previousLabel="Önceki" nextLabel="Sonraki"></pagination-controls> -->
  `

})
export class AppComponent {  
  p: number = 1;
  result: PaginationResultModel = new PaginationResultModel();
  constructor(private http: HttpClient) {
    this.getAll();
  }

  getAll(pageNumber: number = 1) {
    this.p = pageNumber;
    this.http.get<PaginationResultModel>(`https://localhost:7052/api/Personels/GetAll?pageNumber=${pageNumber}`)
      .subscribe((res) => {
        this.result = res;
      })
  }
}
