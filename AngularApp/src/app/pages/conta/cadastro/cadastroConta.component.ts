import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ContaContabil } from '../../../models/ContaContabil';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ContaService } from '../../../services/conta.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
  providers:[BsModalService, ModalModule],
  templateUrl: './cadastroConta.component.html',
  styleUrl: './cadastroConta.component.css'
})
export class CadastroContaComponent implements OnInit {

  _filtroLista: string = '';
  public contaContabeis: ContaContabil[] = [];
  public contaContabilFiltrados: ContaContabil[] = [];
  public contaContabilId!: number;
  public modalRef!: BsModalRef;

  ngOnInit(): void {
    this.carregaContaContabil();
  }

  constructor(
    private contaContabilService: ContaService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ){}

  public get filtroLista(): string {return this._filtroLista;}

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.contaContabilFiltrados = this.filtroLista
                                  ? this.filtroConta(this.filtroLista)
                                  : this.contaContabeis
  }

  public filtroConta(filtrarPor: string): ContaContabil[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.contaContabeis.filter(
      (contaContabil: ContaContabil) => contaContabil.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public carregaContaContabil(): void{
    this.contaContabilService.GetContaContabil().subscribe(
      (contaContabil: ContaContabil[]) => {
        this.contaContabeis = contaContabil.map(conta => {
          return {...conta};
        });

        this.contaContabilFiltrados = this.contaContabeis;
      },
      (error: any) => {
        console.error('Erro ao carregar conta Contabil');
      },
      () => {},
    )
  }

  openModal(event: any, template: TemplateRef<void>, contaContabilId: number): void{
    event.stopPropagation();
    this.contaContabilId = contaContabilId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(){
    this.modalRef.hide();

    this.contaContabilService.DeletarContaContabil(this.contaContabilId).subscribe(
      (result: ContaContabil) => {
        if(result.id === this.contaContabilId){
          this.toastr.success('Conta Contabil foi Deletada com Sucesso', 'Deletada"');
          this.carregaContaContabil();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Error ao tentar deletar Conta Contabil', 'Erro');
      },
      () => {}
    )
  }

  decline(){
    this.modalRef.hide();
  }

}
