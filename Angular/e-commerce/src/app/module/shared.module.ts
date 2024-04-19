import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrCurrencyPipe } from 'tr-currency';
import { SearchComponent } from '../common/components/search/search.component';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    TrCurrencyPipe,
    SearchComponent
  ],
  exports: [
    CommonModule,
    FormsModule,
    TrCurrencyPipe,
    SearchComponent
  ]
})
export class SharedModule { }
