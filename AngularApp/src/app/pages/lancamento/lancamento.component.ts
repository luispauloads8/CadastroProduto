import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-lancamento',
  standalone: true,
  imports: [RouterModule, TituloComponent],
  templateUrl: './lancamento.component.html',
  styleUrl: './lancamento.component.css'
})
export class LancamentoComponent {

}
