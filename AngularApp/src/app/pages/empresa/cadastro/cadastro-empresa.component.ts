import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Empresa } from '../../../models/Empresa';
import { EmpresaService } from '../../../services/empresa.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-cadastro-empresa',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
   providers:[BsModalService, ModalModule],
  templateUrl: './cadastro-empresa.component.html',
  styleUrl: './cadastro-empresa.component.css'
})
export class CadastroEmpresaComponent implements OnInit {

  _filtroLista: string = '';
  public empresaFiltradas: Empresa[] = [];
  public empresas: Empresa[] = [];
  public empresaId!: number;
  public modalRef!: BsModalRef;

  constructor(
    private empresaService: EmpresaService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ){}


  public get filtroLista(): string {return this._filtroLista;}

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.empresaFiltradas = this.filtroLista 
                            ? this.filtroEmpresa(this.filtroLista) 
                            : this.empresas;
  }

  public filtroEmpresa(filtrarPor: string): Empresa[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.empresas.filter(
      (empresa: Empresa) => empresa.nomeFantasia.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  ngOnInit(): void {
    this.carregaEmpresa();
  }

  public carregaEmpresa(): void{
    this.empresaService.GetEmpresa().subscribe(
      (empresa: Empresa[]) => {
        this.empresas = empresa.map(emp => {
          return {...emp};
        });

        this.empresaFiltradas = this.empresas;
      },
      (error: any) => {
        console.error('Erro ao carregar empresa', error);
      },
      () => {},
    )
  }

  openModal(event: any, template: TemplateRef<void>, empresaId: number): void{
  event.stopPropagation();
  this.empresaId = empresaId;
  this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(){
    this.modalRef.hide();

    this.empresaService.DeletarEmpresa(this.empresaId).subscribe(
      (result: Empresa) => {
        if(result.id === this.empresaId){
          this.toastr.success('Empresa foi Deletado com Sucesso', 'Deletado!');
          this.carregaEmpresa();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Error ao tentar deletar empresa', 'Erro');
      },
      () => {}
    );
  }
  
  decline(){
    this.modalRef.hide();
  }
}
