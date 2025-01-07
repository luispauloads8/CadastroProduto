import { Component, TemplateRef } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormularioComponent } from '../../../componentes/formulario/formulario.component';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/Categoria';
import { TituloComponent } from "../../../shared/titulo/titulo.component";
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {

  modalRef?: BsModalRef;

  public categorias: Categoria[] = [];
  public categoriasFiltradas: Categoria[] = [];
  _filtroLista: string = '';

 constructor(
   private categoriaService:CategoriaService,
   private modalService: BsModalService,
   private toastr: ToastrService
 ){}

 ngOnInit(): void {
   this.categoriaService.GetCategorias().subscribe(
     (_categoria: Categoria[]) =>
   {
     this.categorias = _categoria;
     this.categoriasFiltradas = this.categorias;
   }, error => console.log(error));
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

 deletar(id:number | undefined){
   if(id != undefined){
     this.categoriaService.DeletarCategoria(id).subscribe(response =>{
     console.log(response);
     window.location.reload();
   
     });
   }
 }

 openModal(template: TemplateRef<void>) {
   this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
 }

 confirm(): void {
   this.modalRef?.hide();
   this.toastr.success('Categoria foi Deletada com Sucesso!', 'Deletado!');
 }

 decline(): void {
   this.modalRef?.hide();
 }

  // btnAcao = "Cadastrar";
  // descTitulo = "Cadastrar Categoria"

  // constructor(private categoriaService: CategoriaService, private router: Router){}

  // CriarCategoria(categoria: Categoria){
  //   this.categoriaService.CriarCategoria(categoria).subscribe(response => {
  //     this.router.navigate(['/']);
  //   });
  // }
}
