import { Component, OnInit, signal } from '@angular/core';
import { PersonelModel } from '../../models/personel.model';
import { HttpService } from '../../services/http.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-personels',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './personels.component.html',
  styleUrl: './personels.component.css'
})
export class PersonelsComponent implements OnInit {
  personels = signal<PersonelModel[]>([]);
  search = signal("");
  pageNumber = signal(1);
  pageNumbers = signal<number[]>([]);
  totalPageCount = signal<number>(0);

  constructor(
    private http: HttpService
  ){}

  ngOnInit(): void {
    this.getAll();
  }

  getAll(p: number = 1){
    this.pageNumber.set(p);

    this.http.get(`Personels/GetAll?pageNumber=${this.pageNumber()}&search=${this.search()}`, (res:any)=> {
      this.personels.set(res.data);      
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
