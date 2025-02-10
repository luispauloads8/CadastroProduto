import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appEmailMask]',
  standalone: true
})
export class EmailMaskDirective {

  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event']) onInput(event: Event) {
    let input = this.el.nativeElement.value;

    // Remove espaços e caracteres inválidos para e-mail
    input = input.replace(/[^a-zA-Z0-9@._-]/g, '');

    // Atualiza o valor do campo
    this.el.nativeElement.value = input;
    
  }
  

}
