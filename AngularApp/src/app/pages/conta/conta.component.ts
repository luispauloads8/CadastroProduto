import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';


@Component({
  selector: 'app-conta',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './conta.component.html',
  styleUrl: './conta.component.css'
})
export class ContaComponent {

}
