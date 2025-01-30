import { Component, TemplateRef } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormularioComponent } from '../../../componentes/formulario/formulario.component';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/Categoria';
import { TituloComponent } from "../../../shared/titulo/titulo.component";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [RouterModule, FormsModule, HttpClientModule, CommonModule],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {

  modalRef!: BsModalRef;

  public categorias: Categoria[] = [];
  public categoriasFiltradas: Categoria[] = [];
  _filtroLista: string = '';
  public categoriaId!: number;

 constructor(
   private categoriaService:CategoriaService,
   private modalService: BsModalService,
   private toastr: ToastrService
 ){}

 ngOnInit(): void {
  this.carregarCategoria();
 }

 public get filtroLista(): string {
   return this._filtroLista;
 }

 public set filtroLista(value: string){
   this._filtroLista = value;
   this.categoriasFiltradas = this.filtroLista ? this.filtroCategoria(this.filtroLista) : this.categorias;
 }

 filtroCategoria(filtrarPor: string): Categoria[]{
   filtrarPor = filtrarPor.toLowerCase();
   return this.categorias.filter(
     (categoria: Categoria) => categoria.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
   )
 }

 public carregarCategoria(): void {
  this.categoriaService.GetCategorias().subscribe({
    next: (categorias: Categoria[]) => {
      this.categorias = categorias;
      this.categoriasFiltradas = this.categorias;
    },
    error: (erro) => {
      console.error('Erro ao carregar categorias:', erro);
     // this.toastr.error('Erro ao Carregar categorias', 'Erro!');
    },
    complete: () => {},
  });
 }

 openModal(event: any, template: TemplateRef<void>, categoriaId: number): void {
  event.stopPropagation();
  this.categoriaId = categoriaId;
   this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
 }

  confirm(): void {
   this.modalRef.hide();

    this.categoriaService.DeletarCategoria(this.categoriaId).subscribe(
      (result: Categoria) => {
        if(result.id === this.categoriaId){
          this.toastr.success('Categoria foi Deletada com Sucesso!', 'Deletado!');
          this.carregarCategoria();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao tentar deletar a categoria.', 'Erro');
      },
      () => {},
    ); 
  }

 decline(): void {
   this.modalRef.hide();
 }

}
