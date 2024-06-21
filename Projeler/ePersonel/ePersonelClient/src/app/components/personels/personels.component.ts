import { Component, OnInit, signal } from '@angular/core';
import { PersonelModel } from '../../models/personel.model';
import { HttpService } from '../../services/http.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { GridModule } from '@progress/kendo-angular-grid'
import { State } from '@progress/kendo-data-query';

@Component({
  selector: 'app-personels',
  standalone: true,
  imports: [CommonModule, FormsModule, GridModule],
  templateUrl: './personels.component.html',
  styleUrl: './personels.component.css'
})
export class PersonelsComponent implements OnInit {
  result = signal<{data: PersonelModel[], total: number}>({data: [], total: 0});
  search = signal("");
  pageNumber = signal(1);
  pageNumbers = signal<number[]>([]);
  totalPageCount = signal<number>(0);
  state = signal<State>({
    skip: 0,
    take: 10,
    filter: {
      logic: "and",
      filters: [],
    },
    group: [],
    sort: [],
  });

  constructor(
    private http: HttpService
  ){}

  ngOnInit(): void {
    //this.getAll();
    this.getAllOData();
  }

  getAllOData(){
    this.http.get<any>(`Personels/GetAllOData?$count=true&$top=${this.state().take}&$skip=${this.state().skip}`,(res)=> {
      this.result.set(res); 
    });
  }

  stateChange(event:any){
    console.log(event);    

    this.state.set(event);
    this.getAllOData();
  }

  getAll(p: number = 1){
    this.pageNumber.set(p);

    this.http.get(`Personels/GetAll?pageNumber=${this.pageNumber()}&search=${this.search()}`, (res:any)=> {
      this.result().data = res.data;
      this.totalPageCount.set(res.totalPageCount);

      const startPage = Math.max(1, this.pageNumber() - 2);
      const endPage = Math.min(res.totalPageCount, this.pageNumber() + 2);

      const numbers = [];
      for (let i = startPage; i <= endPage; i++) {
        numbers.push(i);
      }

      this.pageNumbers.set(numbers);
    });
  }
}
