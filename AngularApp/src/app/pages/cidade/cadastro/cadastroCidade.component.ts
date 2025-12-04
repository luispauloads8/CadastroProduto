import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Cidade } from '../../../models/Cidade';
import { CidadeService } from '../../../services/cidade.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { EnumEstado } from '../../../models/EnumEstado';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';


@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule, PaginationModule],
  providers:[BsModalService, ModalModule],
  templateUrl: './cadastroCidade.component.html',
  styleUrl: './cadastroCidade.component.css'
})
export class CadastroCidadeComponent implements OnInit {
  
  _filtroLista: string = '';
  public cidades: Cidade[] = [];
  public cidadesFiltradas: Cidade[] = [];
  public cidadeId!: number;
  public modalRef!: BsModalRef;
  EnumEstado!: EnumEstado;
  estadoKey: string | undefined
  searchChanged = new Subject<string>();


     // 🔹 Paginação
  pageNumber = 1;
  pageSize = 5;
  searchTerm: string = '';
  totalItems = 0;

   constructor(
    private cidadeService: CidadeService,
    private toastr: ToastrService,
    private modalService: BsModalService
   ){}

  ngOnInit(): void {
    this.carregarCidades();
    this.escutarDigitacao();
  }

  private escutarDigitacao() {
    this.searchChanged
    .pipe(
      debounceTime(400),        // espera 400ms entre as teclas
      distinctUntilChanged()    // só dispara se o texto mudar
    )
    .subscribe(search => {
      this.searchTerm = search;
      this.pageNumber = 1;      // resetar página ao buscar
      this.carregarCidades();
    });
  }

  public getEstadoDescricao(estadoNumero: number): string {
    return EnumEstado[estadoNumero]; // retorna o nome, ex: "Goias"
  }

  public get filtroLista(): string {return this._filtroLista;}
    
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.cidadesFiltradas = this.filtroLista ? this.filtroProduto(this.filtroLista) : this.cidades;
  }

  public filtroProduto(filtrarPor: string): Cidade[]{
      filtrarPor = filtrarPor.toLowerCase();
      return this.cidades.filter(
        (cidade: Cidade) => cidade.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
      )
  }

    // 🔹 Método de carregar com paginação
  public carregarCidades(): void {
    this.cidadeService.getCategoriasPaginadas(this.pageNumber, this.pageSize, this.searchTerm).subscribe({
      next: (result) => {
        this.cidades = result.items;
        this.totalItems = result.totalItems;
        this.cidadesFiltradas = this.cidades;
      },
      error: (erro) => {
        console.error('Erro ao carregar categorias:', erro);
      },
    });
  }

  // 🔹 Evento de mudança de página
  onPageChanged(event: any): void {
    this.pageNumber = event.page;
    this.carregarCidades();
  }

  onSearchChange(search: string): void {
    this.pageNumber = 1; // reset na página
    this.searchTerm = search;
    this.carregarCidades(); // chama API
  }

  openModal(event: any, template: TemplateRef<void>, cidadeId: number): void {
    event.stopPropagation();
    this.cidadeId = cidadeId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(){
    this.modalRef.hide();

    this.cidadeService.DeletarCidade(this.cidadeId).subscribe(
      (result: Cidade) => {
        if(result.id === this.cidadeId){
          this.toastr.success('Produto foi Deletado com Sucesso', 'Deletado!');
          this.carregarCidades();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Error ao tentar deletar produto e serviço', 'Erro');
      },
      () => {}
    );
  }

  decline(){
    this.modalRef.hide();
  }
}
