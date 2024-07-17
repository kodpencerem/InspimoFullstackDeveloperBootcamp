import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlankComponent } from './components/blank/blank.component';
import { SectionComponent } from './components/blank/section/section.component';
import { FlexiGridModule } from 'flexi-grid';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BlankComponent,
    SectionComponent,
    FlexiGridModule
  ],
  exports: [
    CommonModule,
    BlankComponent,
    SectionComponent,
    FlexiGridModule
  ]
})
export class SharedModule { }
