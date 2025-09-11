import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-pessoa',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './pessoa.component.html',
  styleUrl: './pessoa.component.css'
})
export class PessoaComponent {

}
