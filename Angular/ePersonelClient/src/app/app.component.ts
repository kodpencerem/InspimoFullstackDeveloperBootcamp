import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationResultModel } from './models/pagination-result.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgxPaginationModule, CommonModule, FormsModule],
  template: `
  <h1>Personel List</h1>  

  <input type="search" [(ngModel)]="search" placeholder="Search something..."/>
  <button (click)="getAll(1)">Search</button>
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
    @if(p !== 1){
      <li class="page-part" (click)="getAll(1)">İlk</li>    
    }  
    @for(page of result.totalPages; track page){
      <li class="page-part" [ngClass]="p === page ? 'active' : ''" (click)="getAll(page)">{{page}}</li>    
    }
    @if(p !== result.totalPageCount){
      <li class="page-part" (click)="getAll(result.totalPageCount)">Son</li>        
    }  
  </ul>
  <p>{{p}} / {{result.totalPageCount}} sayfasındasınız</p>

  <!-- <pagination-controls (pageChange)="p = $event" previousLabel="Önceki" nextLabel="Sonraki"></pagination-controls> -->
  `
})
export class AppComponent {
  p: number = 1;
  search: string = "";

  result: PaginationResultModel = new PaginationResultModel();
  constructor(private http: HttpClient) {
    this.getAll();
  }

  getAll(pageNumber: number = 1) {
    this.p = pageNumber;
    this.http.get<PaginationResultModel>(`https://localhost:7052/api/Personels/GetAll?pageNumber=${pageNumber}&search=${this.search}`)
      .subscribe((res) => {
        this.result = res;
        this.setTotalPages();
      })
  }

  setTotalPages() {
    const pages: number[] = [];
    const startPage = this.p === 1 ? 1 : this.p - 1;
    const endPage = this.p + 3 > this.result.totalPageCount ? this.result.totalPageCount : this.p + 3
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    this.result.totalPages = pages;
  }
}
