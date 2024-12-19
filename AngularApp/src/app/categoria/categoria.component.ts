import { Component, OnInit } from '@angular/core';
import { CategoriaService } from '../services/categoria.service';
import { FormsModule } from '@angular/forms';
import { error } from 'console';

@Component({
  selector: 'app-categoria',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './categoria.component.html',
  styleUrl: './categoria.component.css'
})
export class CategoriaComponent implements OnInit{

   public categorias: any = [];
   public categoriasFiltradas: any = [];
   _filtroLista: string = '';

  constructor(private categoriaService:CategoriaService){}

  ngOnInit(): void {
    this.categoriaService.GetCategoria().subscribe(response =>
    {
      this.categorias = response;
      this.categoriasFiltradas = response;
    }, error => console.log(error));
  }

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.categoriasFiltradas = this.filtroLista ? this.filtroCategoria(this.filtroLista) : this.categorias;
  }

  filtroCategoria(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLowerCase();
    return this.categorias.filter(
      (categoria: any) => categoria.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
    )
  }
}
