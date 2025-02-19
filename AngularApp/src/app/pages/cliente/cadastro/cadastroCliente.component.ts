import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ClienteService } from '../../../services/cliente.service';
import { Cliente } from '../../../models/Cliente';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { response } from 'express';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
  providers:[BsModalService, ModalModule],
  templateUrl: './cadastroCliente.component.html',
  styleUrl: './cadastroCliente.component.css'
})
export class CadastroClienteComponent implements OnInit {

  _filtroLista: string = '';
  public clientes: Cliente[] = [];
  public clienteFiltrados: Cliente[] = [];
  public clienteId!: number;
  public modalRef!: BsModalRef;

  ngOnInit(): void {
    this.carregaCliente();
  }

  constructor(
    private clienteService: ClienteService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ){}

  public get filtroLista(): string {return this._filtroLista;}

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.clienteFiltrados = this.filtroLista 
                            ? this.filtroCliente(this.filtroLista) 
                            : this.clientes;
  }


  public filtroCliente(filtrarPor: string): Cliente[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.clientes.filter(
      (cliente: Cliente) => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public carregaCliente(): void{
    this.clienteService.GetCliente().subscribe(
      (cliente: Cliente[]) => {
        this.clientes = cliente.map(clie => {
          return {...clie};
        });

        this.clienteFiltrados = this.clientes;
      },
      (error: any) => {
        console.error('Erro ao carregar cliente', error);
      },
      () => {}
    )
  }

  openModal(event: any, template: TemplateRef<void>, clienteId: number): void{
  event.stopPropagation();
  this.clienteId = clienteId;
  this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(){
    this.modalRef.hide();

    this.clienteService.DeletarCliente(this.clienteId).subscribe(
      (result: Cliente) => {
        if(result.id === this.clienteId){
          this.toastr.success('Cliente foi Deletado com Sucesso', 'Deletado!');
          this.carregaCliente();
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
