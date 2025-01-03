import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormularioComponent } from '../../componentes/formulario/formulario.component';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria } from '../../models/Categoria';
import { TituloComponent } from "../../shared/titulo/titulo.component";

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormularioComponent, TituloComponent],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {

  btnAcao = "Cadastrar";
  descTitulo = "Cadastrar Categoria"

  constructor(private categoriaService: CategoriaService, private router: Router){}

  CriarCategoria(categoria: Categoria){
    this.categoriaService.CriarCategoria(categoria).subscribe(response => {
      this.router.navigate(['/']);
    });
  }
}
