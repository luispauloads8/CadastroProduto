import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';

import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { LancamentoService } from '../../../services/lancamento.service';
import { Lancamento } from '../../../models/Lancamento';
import { ProdutoServico } from '../../../models/ProdutoServico';
import { debounceTime, distinctUntilChanged, of, Subject, switchMap } from 'rxjs';
import { ProdutoServicoService } from '../../../services/produto-servico.service';
import { Empresa } from '../../../models/Empresa';
import { EmpresaService } from '../../../services/empresa.service';
defineLocale('pr-br', ptBrLocale);

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [FormsModule, RouterModule, ReactiveFormsModule, CommonModule,  BsDatepickerModule],
  templateUrl: './detalhesLancamentos.component.html',
  styleUrl: './detalhesLancamentos.component.css'
})
export class DetalhesLancamentosComponent {

  form!: FormGroup;
  estadoSalvar = 'post';
  lancamento!: Lancamento;
  produtosServicos: ProdutoServico[] = [];
  produtoServicoSelecionando: ProdutoServico | null = null;//produto escolhido
  produtoServicoFiltro: string = ''; // valor digitado no campo
  produtosServicosFiltrados: ProdutoServico[] = []; // resultado da busca
  carregando: boolean = false; // Indica se está carregando os dados

  empresas: Empresa[] = [];
  empresasFiltradas: Empresa[] = [];
  empresaSelecionado: Empresa | null = null;// produto escolhido
  empresaFiltro: string = ''; //valor digitado no campo

  private buscaSubject = new Subject<string>();
  private buscaEmpresaSubject = new Subject<string>();

  get f(): any{
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      showWeekNumbers: false
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private route: ActivatedRoute,
    private lancamentoService: LancamentoService,
    private produtoServicoService: ProdutoServicoService,
    private empresaService: EmpresaService)
  {
    this.localeService.use('pr-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarLancamento();
    this.carregarProdutoServico();
    this.carregarEmpresa();
  }

  OnIputChangeEmpresa(){

     if(this.empresaFiltro.trim() === ''){
       this.empresasFiltradas = []
      }

    this.carregando = true;
    this.buscaEmpresaSubject.next(this.empresaFiltro);
  }

  onInputChange(){

    if(this.produtoServicoFiltro.trim() ===''){
      this.produtosServicosFiltrados = []
    }

    this.carregando = true;
    this.buscaSubject.next(this.produtoServicoFiltro);
  }

  public validation(): void {
    this.form = this.fb.group({
      empresa: ['', [Validators.required]],
      produtoServico: ['', [ Validators.required]],
      cliente: ['', [Validators.required]],
      quantidade: ['', [ Validators.required]],
      valor: ['', [Validators.required]],
      contaContabil: ['', [ Validators.required]],
      dataLancamento: ['', [Validators.required]],
      observacao: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(300)]],
    });
  }

  selecionarProdutoServico(produtoServico: ProdutoServico) {
    this.produtoServicoSelecionando = produtoServico;
    this.produtoServicoFiltro = produtoServico.descricao; // Exibe o valor selecionado no input
    this.produtosServicosFiltrados = []; // Limpa o dropdown após seleção
  }

  selecionarEmpresa(empresa: Empresa){
    this.empresaSelecionado = empresa;
    this.empresaFiltro = empresa.razaoSocial;
    this.empresasFiltradas = [];
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public carregarLancamento(): void{
    const lancamento = this.route.snapshot.paramMap.get('id');

    if(lancamento != null){
      this.estadoSalvar = 'put';

      this.lancamentoService.GetLancamentoId(+lancamento).subscribe(
        (lancamento: Lancamento) => {
          this.lancamento = {...lancamento};
          this.form.patchValue(this.lancamento);

          //configurar para exibir empresa
         if(lancamento.empresa){
          this.empresaSelecionado = lancamento.empresa;
          this.empresaFiltro = lancamento.empresa.razaoSocial;
         }
        //configurar para exibir produto
         if(lancamento.produtoServico){
          this.produtoServicoSelecionando = lancamento.produtoServico; // exibie os dados do produto
          this.produtoServicoFiltro = lancamento.produtoServico.descricao; // mostra a descrição no input
         }
        //configurar para exibir cliente
         if(lancamento.cliente){}

        //configurar conta contabil
        if(lancamento.contaContabil){} 


        },
        (error: any) => {
          console.error(error);
        },
        () => {},
      );
    }
  }

  buscarProdutosServicos(termo: string) {
    if (!termo.trim()) {
      this.carregando = false;
      return of([]); // Retorna um Observable vazio
    }
    return this.produtoServicoService.GetProdutoServicoTermo(termo);
  }

  carregarProdutoServico(): void{
    this.buscaSubject
      .pipe(
        debounceTime(300), // atraso de 300ms após a digitação
        distinctUntilChanged(), //evita buscas, repetidas para o mesmo valor
        switchMap((termo) => this.buscarProdutosServicos(termo))
      )
      .subscribe((produtosServicos) => {
        this.produtosServicosFiltrados = produtosServicos;
        this.carregando = false;
      });
  }

  buscarEmpresa(termo: string){
    if(!termo.trim()){
      this.carregando = false;
      return of([]); // Retorna um Observable vazio
    }
    return this.empresaService.GetEmpresaTermo(termo);
  }

  carregarEmpresa(): void{
    this.buscaEmpresaSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((termo) => this.buscarEmpresa(termo))
      )
      .subscribe((empresas) => {
        this.empresasFiltradas = empresas;
        this.carregando = false;
      });
  }

}
