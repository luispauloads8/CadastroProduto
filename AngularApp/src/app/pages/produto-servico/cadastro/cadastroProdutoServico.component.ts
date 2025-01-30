import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProdutoServicoService } from '../../../services/produto-servico.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ProdutoServico } from '../../../models/ProdutoServico';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, ToastrModule, CommonModule],
  templateUrl: './cadastroProdutoServico.component.html',
  styleUrl: './cadastroProdutoServico.component.css'
})
export class CadastroProdutoServicoComponent implements OnInit {

  public produtosServicos: ProdutoServico[] = [];
  public produtosServicosFiltrados: ProdutoServico[] = [];
  _filtroLista: string = '';
  public produtoServicoId!: number;
  public modalRef!: BsModalRef;

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

  // public carregaProdutoServico(): void {
  //   this.produtoServicoService.GetProdutoServico().subscribe(
  //     (produtosServicos: ProdutoServico[]) => {
  //       this.produtosServicos = produtosServicos.map(produto => {
  //         if (produto.imagem && typeof produto.imagem === 'string') {
  //           // Use o tipo MIME vindo do back-end para prefixar o Base64
  //           produto.imagem = `data:${produto.imagem};base64,${produto.imagem}`;
  //         }
  //         return produto;
  //       });
  
  //       this.produtosServicosFiltrados = this.produtosServicos;
  //     },
  //     (error: any) => {
  //       console.error('Erro ao carregar produtos/serviços:', error);
  //       this.toastr.error('Erro ao carregar produtos/serviços', 'Erro!');
  //     }
  //   );
  // }
  

  public carregaProdutoServico(): void {
    this.produtoServicoService.GetProdutoServico().subscribe(
      (produtoServico: ProdutoServico[]) => {
        this.produtosServicos = produtoServico.map(produto => {
          return {
            ...produto,
            imagem: `data:image/png;base64,${produto.imagem}`
          };
        });
        this.produtosServicosFiltrados = this.produtosServicos;
      },
      (error : any) => {
        console.error('Erro ao carregar produto serviço:', error);
        //this.toastr.error('Erro ao carragar produto serviço:', 'Erro!');
      },
      () => {}
    );
  }
    
  public filtroProduto(filtrarPor: string): ProdutoServico[]{
      filtrarPor = filtrarPor.toLowerCase();
      return this.produtosServicos.filter(
        (produto: ProdutoServico) => produto.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
      )
  }
  
  public deletar(id:number | undefined){
    if(id != undefined){
      this.produtoServicoService.DeletarProdutoServico(id).subscribe(response =>{
      console.log(response);
      window.location.reload();
    
      });
    }
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
