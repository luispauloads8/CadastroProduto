import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { error } from 'console';


import { ToastrModule, ToastrService } from 'ngx-toastr';
import { provideToastr } from 'ngx-toastr';


import { bootstrapApplication } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ProdutoServico } from '../../models/ProdutoServico';
import { ProdutoServicoService } from '../../services/produto-servico.service';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-produto-servico',
  standalone: true,
  imports: [FormsModule, RouterModule, ToastrModule, CommonModule, TituloComponent],
  providers: [BsModalService],
  templateUrl: './produto-servico.component.html',
  styleUrl: './produto-servico.component.css'
})
export class ProdutoServicoComponent  implements OnInit {

  // public produtosServicos: ProdutoServico[] = [];
  // public produtosServicosFiltrados: ProdutoServico[] = [];
  // _filtroLista: string = '';

  // constructor(
  //   private produtoServicoService: ProdutoServicoService,
  //   private toastr: ToastrService
  // ){}

  ngOnInit(): void {
    // this.produtoServicoService.GetProdutoServico().subscribe(
    //   (_produtoServico: ProdutoServico[]) => {
    //    this.produtosServicos = _produtoServico;
    //    this.produtosServicosFiltrados = this.produtosServicos;
    // }, error => console.log(error));
  }

  //   public get filtroLista(): string {
  //     return this._filtroLista;
  //   }
  
  //   public set filtroLista(value: string){
  //     this._filtroLista = value;
  //     this.produtosServicosFiltrados = this.filtroLista ? this.filtroCategoria(this.filtroLista) : this.produtosServicos;
  //   }
  
  //   filtroCategoria(filtrarPor: string): ProdutoServico[]{
  //     filtrarPor = filtrarPor.toLowerCase();
  //     return this.produtosServicos.filter(
  //       (categoria: ProdutoServico) => categoria.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
  //     )
  //   }

  // deletar(id:number | undefined){
  //   if(id != undefined){
  //     this.produtoServicoService.DeletarProdutoServico(id).subscribe(response =>{
  //     console.log(response);
  //     window.location.reload();
    
  //     });
  //   }
  // }

}
