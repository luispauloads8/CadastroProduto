import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PedidoService } from '../../../services/pedido.service';
import { Pedido } from '../../../models/Pedido';

@Component({
  selector: 'app-cadastro',
  standalone: true,
   imports: [FormsModule, RouterModule, CommonModule],
   providers:[BsModalService, ModalModule],
  templateUrl: './cadastroPedidos.component.html',
  styleUrl: './cadastroPedidos.component.css',
})
export class CadastroPedidosComponent implements OnInit {

    _filtroLista: string = '';
    public pedidos: Pedido[] = [];
    public pedidoFiltrados: Pedido[] = [];
    public pedidoId!: number
    public modalRef!: BsModalRef;

    constructor(
      private pedidoService: PedidoService,
      private toastr: ToastrService,
      private modalService: BsModalService
    ){}

    ngOnInit(): void {
      this.CarregaPedido();
    }

    openModal(event: any, template: TemplateRef<void>, pedidoId: number): void{
    event.stopPropagation();
    this.pedidoId = pedidoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }

    public get filtroLista(): string {return this._filtroLista;}
  
    public set filtroLista(value: string){
      this._filtroLista = value;
      this.pedidoFiltrados = this.filtroLista 
                              ? this.filtroPedido(this.filtroLista) 
                              : this.pedidos;
    }
    
    public filtroPedido(filtrarPor: string): Pedido[]{
      filtrarPor = filtrarPor.toLowerCase();
      return this.pedidos.filter(
        (pedido: Pedido) => pedido.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      )
    }

    public CarregaPedido() : void{
      this.pedidoService.GetPedido().subscribe(
        (pessoa: Pedido[]) => {
          this.pedidos = pessoa.map(pes => {
            return {...pes};
          });
  
          this.pedidoFiltrados = this.pedidos;
        },
        (error: any) => {
          console.error('Erro ao carregar pedido', error);
        },
        () => {}
      )
    }

    confirm(){
      this.modalRef.hide();
  
      this.pedidoService.DeletarPedido(this.pedidoId).subscribe(
        (result: Pedido) => {
          // if(result.id === this.pedidoId){
          //   this.toastr.success('Cliente foi Deletado com Sucesso', 'Deletado!');
          //   this.CarregaPessoa();
          // }
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
