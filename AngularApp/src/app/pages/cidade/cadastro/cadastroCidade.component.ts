import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Cidade } from '../../../models/Cidade';
import { CidadeService } from '../../../services/cidade.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
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

   constructor(
    private cidadeService: CidadeService,
    private toastr: ToastrService,
    private modalService: BsModalService
   ){}

  ngOnInit(): void {
    this.carregaCidade();
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

  public carregaCidade(): void{
    this.cidadeService.GetCidade().subscribe(
      (cidade: Cidade[]) => {
        this.cidades = cidade.map(cid => {
          return {...cid};
        });
        this.cidadesFiltradas = this.cidades;
      },
      (error: any) => {
        console.error('Erro ao carregar cidade:', error);
      },
      () => {},
    )
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
          this.carregaCidade();
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
