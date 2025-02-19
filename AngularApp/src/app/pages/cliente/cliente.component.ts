import { Component } from '@angular/core';
import { TituloComponent } from '../../shared/titulo/titulo.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cliente',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './cliente.component.html',
  styleUrl: './cliente.component.css'
})
export class ClienteComponent {

}
