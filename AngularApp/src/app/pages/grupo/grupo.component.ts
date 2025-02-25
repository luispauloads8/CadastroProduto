import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-grupo',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './grupo.component.html',
  styleUrl: './grupo.component.css'
})
export class GrupoComponent {

}
