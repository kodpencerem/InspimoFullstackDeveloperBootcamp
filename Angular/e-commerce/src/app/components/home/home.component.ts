import { Component } from '@angular/core';
import { CategoryModel } from '../../models/category.model';
import { CategoryPipe } from '../../pipes/category.pipe';
import { ProductModel } from '../../models/product.model';
import { ProductPipe } from '../../pipes/product.pipe';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { ProductService } from '../../services/product.service';
import { ShoppingCartModel } from '../../models/shopping-cart.model';
import { SharedModule } from '../../module/shared.module';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ CategoryPipe, ProductPipe, SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  categories: CategoryModel[] = [];
  numbers: number[] = [1,2,3,4]
  categorySearch: string = "";
  productSearch: string = "";
  selectedCategoryId: string = "";

  constructor(
    private _cart: ShoppingCartService,
    public _product: ProductService,
    private _http: HttpService
  ) {
    this.getAllCategories();
  }

  getAllCategories() {
    this._http.get<CategoryModel[]>("categories", (res)=> {
      this.categories = res;
    });
  }

  selectCategory(id: string = "") {
    this.selectedCategoryId = id;
  }

  decrementProductQuantity(product: ProductModel) {
    if (product.quantity > 1) {
      product.quantity--;
    }
  }

  incrementProductQuantity(product: ProductModel) {
    if (product.quantity < product.stock) {
      product.quantity++;
    }
  }

  addShoppingCart(product: ProductModel) {
    const productModel = { ...product };  

    const model = this._cart.shoppingCarts.find(p => p.productId === product.id);
    if (model === undefined) {
      //eğer sepette eklemek istediğim ürün yoksa ürünü sepete ekle.
      const cart:ShoppingCartModel = {
        productId: productModel.id,
        categoryId: productModel.categoryId,
        description: productModel.description,
        discountedPrice: product.discountedPrice,
        imageUrl: productModel.imageUrl,
        kdvRate: productModel.kdvRate,
        name: productModel.name,
        price: productModel.price,
        quantity: productModel.quantity,        
        category: productModel.category,
        id: undefined
      }

      this._http.post("shoppingCarts", cart, ()=> this._cart.getAll());
    } else {
      //eğer sepette ürün varsa adedini güncelle ve API isteği ile kayıttaki bilgisini değiştir
      model.quantity += productModel.quantity;

      this._http.put(`shoppingCarts/${model.id}`, model,()=> this._cart.getAll());
    }

    product.stock -= product.quantity;
    this._http.put(`products/${product.id}`, product,()=> this._product.getAll());
  }
}
