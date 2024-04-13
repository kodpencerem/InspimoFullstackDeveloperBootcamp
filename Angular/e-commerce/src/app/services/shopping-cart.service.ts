import { Injectable } from '@angular/core';
import { ShoppingCartModel } from '../models/shopping-cart.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  shoppingCarts: ShoppingCartModel[] = [];
  
  constructor(
    private http: HttpClient
  ) { 
    this.getAll();
  }

  getAll(){
    this.http.get<ShoppingCartModel[]>("http://localhost:5000/shoppingCarts").subscribe({
      next: (res)=> {
        this.shoppingCarts = res;
      },
      error: (err:HttpErrorResponse)=> {
        console.log(err);        
      }
    })
  }
}
