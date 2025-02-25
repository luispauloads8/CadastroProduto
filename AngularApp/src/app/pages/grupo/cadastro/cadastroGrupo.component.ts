import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { GrupoConta } from '../../../models/GrupoConta';
import { GrupoService } from '../../../services/grupo.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
    providers:[BsModalService, ModalModule],
  templateUrl: './cadastroGrupo.component.html',
  styleUrl: './cadastroGrupo.component.css'
})
export class CadastroGrupoComponent implements OnInit{

  _filtroLista: string = '';
  public grupoContas: GrupoConta[] = [];
  public grupoContaFiltrados: GrupoConta[] = [];
  public grupoContaId!: number; 
  public modalRef!: BsModalRef;

  ngOnInit(): void {
    this.carregaGrupoConta();
  }

  constructor(
    private grupoContaService: GrupoService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ){}

  public get filtroLista(): string {return this._filtroLista;}

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.grupoContaFiltrados = this.filtroLista
                               ? this.filtroGrupoConta(this.filtroLista)
                               : this.grupoContas;
  }

  public filtroGrupoConta(filtrarPor: string): GrupoConta[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.grupoContas.filter(
      (grupoConta: GrupoConta) => grupoConta.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public carregaGrupoConta(): void{
    this.grupoContaService.GetGrupoConta().subscribe(
      (grupoConta: GrupoConta[]) => {
        this.grupoContas = grupoConta.map(grupo => {
          return {...grupo};
        });

        this.grupoContaFiltrados = this.grupoContas;
      },
      (error: any) => {
        console.error('Erro ao carregar grupo de conta');
      },
      () => {}
    )
  }

  openModal(event: any, template: TemplateRef<void>, grupoContaId: number): void{
    event.stopPropagation();
    this.grupoContaId = grupoContaId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
  
  confirm(){
      this.modalRef.hide();
  
      this.grupoContaService.DeletarGrupoConta(this.grupoContaId).subscribe(
        (result: GrupoConta) => {
          if(result.id === this.grupoContaId){
            this.toastr.success('Grupo Conta foi Deletada com Sucesso', 'Deletada"');
            this.carregaGrupoConta();
          }
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao tentar deletar Grupo Conta', 'Erro');
        },
        () => {}
      )
  }
  
  decline(){
    this.modalRef.hide();
  }
  
}
