import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PedidoService } from '../../../services/pedido.service';
import { Pedido } from '../../../models/Pedido';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-cadastro',
  standalone: true,
   imports: [FormsModule, RouterModule, CommonModule, PaginationModule],
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

           // 🔹 Paginação
  pageNumber = 1;
  pageSize = 5;
  totalItems = 0;

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

      // 🔹 Método de carregar com paginação
  public CarregaPedido(): void {
    this.pedidoService.getCategoriasPaginadas(this.pageNumber, this.pageSize).subscribe({
      next: (result) => {
        this.pedidos = result.items;
        this.totalItems = result.totalItems;
        this.pedidoFiltrados = this.pedidos;
      },
      error: (erro) => {
        console.error('Erro ao carregar categorias:', erro);
      },
    });
  }

  // 🔹 Evento de mudança de página
  onPageChanged(event: any): void {
    this.pageNumber = event.page;
    this.CarregaPedido();
  }

    confirm(){
      this.modalRef.hide();
  
      this.pedidoService.DeletarPedido(this.pedidoId).subscribe(
        (result: Pedido) => {
          if(result.id === this.pedidoId){
            this.toastr.success('Pedido foi Deletado com Sucesso', 'Deletado!');
            this.CarregaPedido();
          }
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao tentar deletar Pedido', 'Erro');
        },
        () => {}
      );
    }
        
    decline(){
      this.modalRef.hide();
    }
}
