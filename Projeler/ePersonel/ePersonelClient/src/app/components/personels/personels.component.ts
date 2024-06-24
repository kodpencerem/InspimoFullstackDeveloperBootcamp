import { Component, OnInit, signal } from '@angular/core';
import { PersonelModel } from '../../models/personel.model';
import { HttpService } from '../../services/http.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { GridModule } from '@progress/kendo-angular-grid'
import { State } from '@progress/kendo-data-query';
import { DataGridComponent } from '../../common/data-grid/data-grid.component';
import { DataGridColumnComponent } from '../../common/data-grid-column/data-grid-column.component';

@Component({
  selector: 'app-personels',
  standalone: true,
  imports: [CommonModule, FormsModule, GridModule, DataGridComponent, DataGridColumnComponent],
  templateUrl: './personels.component.html',
  styleUrl: './personels.component.css'
})
export class PersonelsComponent implements OnInit {
  result = signal<{ data: PersonelModel[], total: number }>({ data: [], total: 0 });
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
  loading = signal<boolean>(false);
newState = signal<any>({pageSize: 10, skip: 0});
  constructor(
    private http: HttpService
  ) { }

  ngOnInit(): void {
    //this.getAll();
    this.getAllOData();
  }

  myGridStateChange(event:any){
    console.log(event);
    this.newState.set(event);
    this.getAllOData();
    
  }

  getAllOData() {
    this.loading.set(true);
    let endpoint = `Personels/GetAllOData?$count=true&$top=${this.newState().pageSize}&$skip=${this.newState().skip}`;
    //let endpoint = `Personels/GetAllOData?$count=true&$top=${this.state().take}&$skip=${this.state().skip}`;

    if (this.state().sort!.length > 0) {
      const dir = this.state().sort![0].dir;
      const field = this.state().sort![0].field;

      if (dir === "asc") {
        endpoint += `&$orderby=${this.toTitleCase(field)}`
      } else {
        endpoint += `&$orderby=${this.toTitleCase(field)} desc`
      }
    }

    if (this.state().filter!.filters.length > 0) {
      const filter = this.state()!.filter!.filters[0] as any;
      const field = filter.field;
      const operator = filter.operator;
      const value = filter.value;
  
      if (operator === "contains") {
        endpoint += `&$filter=contains(${this.toTitleCase(field)}, '${value}')`;
      } else {
        endpoint += `&$filter=${this.toTitleCase(field)} ${operator} '${value}'`;
      }
    }

    this.http.get<any>(endpoint, (res) => {
      this.result.set(res);
      this.loading.set(false);
    });
  }

  stateChange(event: any) {
    console.log(event);

    this.state.set(event);
    this.getAllOData();
  }

  getAll(p: number = 1) {
    this.pageNumber.set(p);

    this.http.get(`Personels/GetAll?pageNumber=${this.pageNumber()}&search=${this.search()}`, (res: any) => {
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

  toTitleCase(str: string) {
    return str
      .split(' ') // Metni boşluklardan ayırarak kelimelere bölüyoruz.
      .map(word => word.charAt(0).toUpperCase() + word.slice(1)) // Her kelimenin ilk harfini büyük yapıyoruz.
      .join(' '); // Kelimeleri tekrar birleştiriyoruz.
  }

}
