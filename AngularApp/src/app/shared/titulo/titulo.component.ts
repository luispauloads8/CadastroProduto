import { Component, inject, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-titulo',
  standalone: true,
  imports: [],
  templateUrl: './titulo.component.html',
  styleUrl: './titulo.component.css'
})
export class TituloComponent {
  
  @Input() title = '';

  private titleService = inject(Title);

  ngOnChanges() {
    if (this.title) {
      this.titleService.setTitle(this.title);
    }
  }

}
