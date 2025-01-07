import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria } from '../../models/Categoria';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { provideToastr } from 'ngx-toastr';


import { AppComponent } from '../../app.component';
import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from '../../app.config';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-categoria',
  standalone: true,
  imports: [FormsModule, RouterModule, TooltipModule, ModalModule, CommonModule],
  providers: [BsModalService],
  templateUrl: './categoria.component.html',
  styleUrl: './categoria.component.css'
})

export class CategoriaComponent implements OnInit{

  ngOnInit(): void {
    
  }
  // modalRef?: BsModalRef;

  //  public categorias: Categoria[] = [];
  //  public categoriasFiltradas: Categoria[] = [];
  //  _filtroLista: string = '';

  // constructor(
  //   private categoriaService:CategoriaService,
  //   private modalService: BsModalService,
  //   private toastr: ToastrService
  // ){}

  // ngOnInit(): void {
  //   this.categoriaService.GetCategorias().subscribe(
  //     (_categoria: Categoria[]) =>
  //   {
  //     this.categorias = _categoria;
  //     this.categoriasFiltradas = this.categorias;
  //   }, error => console.log(error));
  // }

  // public get filtroLista(): string {
  //   return this._filtroLista;
  // }

  // public set filtroLista(value: string){
  //   this._filtroLista = value;
  //   this.categoriasFiltradas = this.filtroLista ? this.filtroCategoria(this.filtroLista) : this.categorias;
  // }

  // filtroCategoria(filtrarPor: string): Categoria[]{
  //   filtrarPor = filtrarPor.toLowerCase();
  //   return this.categorias.filter(
  //     (categoria: Categoria) => categoria.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
  //   )
  // }

  // deletar(id:number | undefined){
  //   if(id != undefined){
  //     this.categoriaService.DeletarCategoria(id).subscribe(response =>{
  //     console.log(response);
  //     window.location.reload();
    
  //     });
  //   }
  // }

  // openModal(template: TemplateRef<void>) {
  //   this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  // }
 
  // confirm(): void {
  //   this.modalRef?.hide();
  //   this.toastr.success('Categoria foi Deletada com Sucesso!', 'Deletado!');
  // }
 
  // decline(): void {
  //   this.modalRef?.hide();
  // }

}

