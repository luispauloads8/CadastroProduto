import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-cidade',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './cidade.component.html',
  styleUrl: './cidade.component.css'
})
export class CidadeComponent {

}
