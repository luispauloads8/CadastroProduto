import { Component } from '@angular/core';
import { TituloComponent } from '../../../shared/titulo/titulo.component';


@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [TituloComponent],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent {

}
