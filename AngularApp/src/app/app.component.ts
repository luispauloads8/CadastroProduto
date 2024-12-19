import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CategoriaComponent } from "./categoria/categoria.component";
import { CommonModule } from '@angular/common';
import { ProdutoServicoComponent } from './produto-servico/produto-servico.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CategoriaComponent, ProdutoServicoComponent, CommonModule, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

}
