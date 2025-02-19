import { Component, OnInit, TemplateRef } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Lancamento } from '../../../models/Lancamento';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { LancamentoService } from '../../../services/lancamento.service';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, ToastrModule, CommonModule],
  providers:[BsModalService, ModalModule],
  templateUrl: './cadastroLancamentos.component.html',
  styleUrl: './cadastroLancamentos.component.css'
})
export class CadastroLancamentosComponent implements OnInit{

  template: any;
  _filtroLista: string = '';
  public lancamentos: Lancamento[] = [];
  public lancamentoFiltrados: Lancamento[] = [];
  public lancamentoId!: number;
  public modalRef!: BsModalRef;


  public get filtroLista(): string {return this._filtroLista;}
    
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.lancamentoFiltrados = this.filtroLista ? this.filtroLancamento(this.filtroLista) : this.lancamentos;
  }

  constructor(
    private lancamentoService: LancamentoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
  ){}

  ngOnInit(): void {
    this.carregarLancamentos();
  }

  public carregarLancamentos(): void{
    this.lancamentoService.GetLancamentos().subscribe(
      (lancamento: Lancamento[]) => {
        this.lancamentos = lancamento.map(lan => {
          return {...lan}
        })
        this.lancamentoFiltrados = this.lancamentos;
      },
      (error: any) => {
        console.error('Erro ao carregar lançamentos', error);
      },
      () => {},
    );
  }

  openModal(event: any, template: TemplateRef<void>, lancamentoId: number): void {
    event.stopPropagation();
    this.lancamentoId = lancamentoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public filtroLancamento(filtrarPor: string): Lancamento[]{
      filtrarPor = filtrarPor.toLowerCase();
      return this.lancamentos.filter(
        (lancamento: Lancamento) => lancamento.produtoServico.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
      )
  }

  confirm(){
    this.modalRef.hide();

    this.lancamentoService.DeletarLancamento(this.lancamentoId).subscribe(
      (result: Lancamento) => {
        if(result.id === this.lancamentoId){
          this.toastr.success('Lançamento foi deletado com Sucesso', 'Deletado!');
          this.carregarLancamentos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Error ao tentar deletar lançamento', 'Erro');
      },
      () => {},
    );
  }

  decline(){
    this.modalRef.hide();
  }

}
