import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProdutoServicoService } from '../../../services/produto-servico.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ProdutoServico } from '../../../models/ProdutoServico';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, ToastrModule, CommonModule, PaginationModule],
  providers:[BsModalService, ModalModule],
  templateUrl: './cadastroProdutoServico.component.html',
  styleUrl: './cadastroProdutoServico.component.css'
})
export class CadastroProdutoServicoComponent implements OnInit {

  public produtosServicos: ProdutoServico[] = [];
  public produtosServicosFiltrados: ProdutoServico[] = [];
  _filtroLista: string = '';
  public produtoServicoId!: number;
  public modalRef!: BsModalRef;

     // 🔹 Paginação
  pageNumber = 1;
  pageSize = 5;
  totalItems = 0;

  constructor(
    private produtoServicoService: ProdutoServicoService,
    private toastr: ToastrService,
    private modalService: BsModalService
  )
  {}

  ngOnInit(): void {
    this.carregaProdutoServico();
  }


  public get filtroLista(): string {return this._filtroLista;}
    
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.produtosServicosFiltrados = this.filtroLista ? this.filtroProduto(this.filtroLista) : this.produtosServicos;
  }
  

public carregaProdutoServico(): void {
  this.produtoServicoService.getProdutoServicoPaginadas(this.pageNumber, this.pageSize).subscribe({
    next: (result) => {
      this.produtosServicos = result.items.map(produto => ({
        ...produto,
        imagem: `data:image/png;base64,${produto.imagem}`
      }));
      this.totalItems = result.totalItems;
      this.produtosServicosFiltrados = this.produtosServicos;
    },
    error: (error) => {
      console.error('Erro ao carregar produto serviço:', error);
    }
  });
}

    // 🔹 Evento de mudança de página
  onPageChanged(event: any): void {
    this.pageNumber = event.page;
    this.carregaProdutoServico();
  }
    
  public filtroProduto(filtrarPor: string): ProdutoServico[]{
      filtrarPor = filtrarPor.toLowerCase();
      return this.produtosServicos.filter(
        (produto: ProdutoServico) => produto.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
      )
  }

  openModal(event: any, template: TemplateRef<void>, produtoServicoId: number): void {
    event.stopPropagation();
    this.produtoServicoId = produtoServicoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(){
    this.modalRef.hide();

    this.produtoServicoService.DeletarProdutoServico(this.produtoServicoId).subscribe(
      (result: ProdutoServico) => {
        if(result.id === this.produtoServicoId){
          this.toastr.success('Produto foi Deletado com Sucesso', 'Deletado!');
          this.carregaProdutoServico();
        }
      },
      () => {}
    );
  }

  decline(){
    this.modalRef.hide();
  }
}
