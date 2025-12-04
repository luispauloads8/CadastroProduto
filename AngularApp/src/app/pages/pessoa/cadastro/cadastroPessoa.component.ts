import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PessoaService } from '../../../services/pessoa.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { Pessoa } from '../../../models/Pessoa';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule, PaginationModule],
  providers:[BsModalService, ModalModule],
  templateUrl: './cadastroPessoa.component.html',
  styleUrl: './cadastroPessoa.component.css'
})
export class CadastroPessoaComponent implements OnInit {

  _filtroLista: string = '';
  public pessoas: Pessoa[] = [];
  public pessoasFiltrados: Pessoa[] = [];
  public pessoaId!: number;
  public modalRef!: BsModalRef;

           // 🔹 Paginação
  pageNumber = 1;
  pageSize = 5;
  totalItems = 0;


  ngOnInit(): void {
    this.CarregaPessoa();
  }

  constructor(
    private PessoaService: PessoaService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ){}

    public get filtroLista(): string {return this._filtroLista;}

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.pessoasFiltrados = this.filtroLista 
                            ? this.filtroPessoa(this.filtroLista) 
                            : this.pessoas;
  }

  public filtroPessoa(filtrarPor: string): Pessoa[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.pessoas.filter(
      (cliente: Pessoa) => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

        // 🔹 Método de carregar com paginação
  public CarregaPessoa(): void {
    this.PessoaService.getCategoriasPaginadas(this.pageNumber, this.pageSize).subscribe({
      next: (result) => {
        this.pessoas = result.items;
        this.totalItems = result.totalItems;
        this.pessoasFiltrados = this.pessoas;
      },
      error: (erro) => {
        console.error('Erro ao carregar categorias:', erro);
      },
    });
  }

  // 🔹 Evento de mudança de página
  onPageChanged(event: any): void {
    this.pageNumber = event.page;
    this.CarregaPessoa();
  }

  openModal(event: any, template: TemplateRef<void>, clienteId: number): void{
  event.stopPropagation();
  this.pessoaId = clienteId;
  this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(){
    this.modalRef.hide();

    this.PessoaService.DeletarPessoa(this.pessoaId).subscribe(
      (result: Pessoa) => {
        if(result.id === this.pessoaId){
          this.toastr.success('Cliente foi Deletado com Sucesso', 'Deletado!');
          this.CarregaPessoa();
        }
      },
      // (error: any) => {
      //   console.error(error);
      //   this.toastr.error('Error ao tentar deletar Cliente', 'Erro');
      // },
      () => {}
    );
  }
    
  decline(){
    this.modalRef.hide();
  }


}
