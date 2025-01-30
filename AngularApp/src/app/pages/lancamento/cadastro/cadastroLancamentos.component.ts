import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { Lancamento } from '../../../models/Lancamento';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [FormsModule, RouterModule, ToastrModule, CommonModule],
  templateUrl: './cadastroLancamentos.component.html',
  styleUrl: './cadastroLancamentos.component.css'
})
export class CadastroLancamentosComponent {

  _filtroLista: string = '';
  public lancamentos: Lancamento[] = [];
  public lancamentoFiltrados: Lancamento[] = [];


  public get filtroLista(): string {return this._filtroLista;}
    
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.lancamentoFiltrados = this.filtroLista ? this.filtroLancamento(this.filtroLista) : this.lancamentos;
  }

  public filtroLancamento(filtrarPor: string): Lancamento[]{
      filtrarPor = filtrarPor.toLowerCase();
      return this.lancamentos.filter(
        (lancamento: Lancamento) => lancamento.produtoServico.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
      )
  }

}
