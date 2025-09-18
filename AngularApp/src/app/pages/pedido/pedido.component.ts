import { Component } from '@angular/core';
import { TituloComponent } from '../../shared/titulo/titulo.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-pedido',
  standalone: true,
  imports: [TituloComponent, RouterOutlet],
  templateUrl: './pedido.component.html',
  styleUrl: './pedido.component.css',
})
export class PedidoComponent {}
