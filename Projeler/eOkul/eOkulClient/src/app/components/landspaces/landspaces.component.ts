import { Component, signal } from '@angular/core';
import { TrCurrencyPipe } from 'tr-currency';

@Component({
  selector: 'app-landspaces',
  standalone: true,
  imports: [TrCurrencyPipe],
  templateUrl: './landspaces.component.html',
  styleUrl: './landspaces.component.css'
})
export default class LandspacesComponent {
  basketCount = signal<number>(0);
  books = signal<{price: number, discountPrice?: number}[]>([
    {
      price: 16.99,
      discountPrice: 7
    },
    {
      price: 9.99      
    },
    {
      price: 16.99,
      discountPrice: 7
    },
    {
      price: 16.99,
      discountPrice: 7
    },
    {
      price: 9.99      
    },
    {
      price: 16.99,
      discountPrice: 7
    }
  ]);
}
