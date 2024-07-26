import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslocoService } from '@jsverse/transloco';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: '<router-outlet></router-outlet>'
})
export class AppComponent {
  constructor(
    private transloco: TranslocoService
  ){
    if(localStorage.getItem("lang")){
      this.transloco.setActiveLang(localStorage.getItem("lang")!)
    }else{
      this.transloco.setActiveLang("tr");
    }    
  }
}
