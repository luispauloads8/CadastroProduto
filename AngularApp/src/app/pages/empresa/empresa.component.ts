import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-empresa',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './empresa.component.html',
  styleUrl: './empresa.component.css'
})
export class EmpresaComponent {

}
