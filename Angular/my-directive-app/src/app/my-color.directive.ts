import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[color]',
  standalone: true
})
export class MyColorDirective {
  @Input("color") color:string = "";
  @Input("bck") bck: string = "";

  constructor(
    private el: ElementRef<HTMLSpanElement>
  ) { }

  @HostListener("mouseenter") mouseenter(){
   this.el.nativeElement.style.color = this.color;
   this.el.nativeElement.style.backgroundColor = this.bck;
  }

  @HostListener("mouseleave") mouseleave(){
    this.el.nativeElement.style.color="black";
    this.el.nativeElement.style.backgroundColor = "white";
  }
}
