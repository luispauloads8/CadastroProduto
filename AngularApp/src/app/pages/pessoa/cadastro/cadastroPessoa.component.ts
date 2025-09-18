import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PessoaService } from '../../../services/pessoa.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { Pessoa } from '../../../models/Pessoa';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
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

  public CarregaPessoa() : void{
    this.PessoaService.GetPessoa().subscribe(
      (pessoa: Pessoa[]) => {
        this.pessoas = pessoa.map(pes => {
          return {...pes};
        });

        this.pessoasFiltrados = this.pessoas;
      },
      (error: any) => {
        console.error('Erro ao carregar pessoa', error);
      },
      () => {}
    )
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
      (error: any) => {
        console.error(error);
        this.toastr.error('Error ao tentar deletar Cliente', 'Erro');
      },
      () => {}
    );
  }
    
  decline(){
    this.modalRef.hide();
  }


}
