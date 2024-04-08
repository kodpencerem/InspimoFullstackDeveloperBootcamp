import { Component, OnInit } from '@angular/core';
import { SearchComponent } from '../../common/components/search/search.component';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { TrCurrencyPipe } from 'tr-currency';

@Component({
  selector: 'app-shopping-carts',
  standalone: true,
  imports: [SearchComponent, TrCurrencyPipe],
  templateUrl: './shopping-carts.component.html',
  styleUrl: './shopping-carts.component.css'
})
export class ShoppingCartsComponent implements OnInit {
  totalAmount: number = 0;
  totalKDV1: number = 0;
  totalKDV10: number = 0;
  totalKDV20: number = 0;
  total: number = 0;

  constructor(
    public cart: ShoppingCartService
  ) { }

  ngOnInit(): void {
    this.calculateTotal();
  }

  calculateTotal() {
    this.total = 0;
    this.totalAmount = 0;
    this.totalKDV1 = 0;
    this.totalKDV10 = 0;
    this.totalKDV20 = 0;

    for (const data of this.cart.shoppingCarts) {
      const amount = data.quantity * data.discountedPrice;
      const kdv = amount - (amount / ((data.kdvRate / 100) + 1))

      this.totalAmount += amount - kdv;
      if (data.kdvRate === 1) {
        this.totalKDV1 += kdv;
      } else if (data.kdvRate === 10) {
        this.totalKDV10 += kdv;
      } else if (data.kdvRate === 20) {
        this.totalKDV20 += kdv;
      }

      this.total += amount;
    }
  }
}
