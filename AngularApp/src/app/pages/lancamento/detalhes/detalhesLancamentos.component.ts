import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
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
import { Cliente } from '../../../models/Cliente';
import { ClienteService } from '../../../services/cliente.service';
import { ContaContabil } from '../../../models/ContaContabil';
import { ContaService } from '../../../services/conta.service';
import { ToastrService } from 'ngx-toastr';
import { ItensLancamentos } from '../../../models/ItensLancamentos';
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
  carregando: boolean = false; // Indica se está carregando os dados
  valorItem: number | null = null;
  quantidade: number | null = null;

  produtosServicos: ProdutoServico[] = [];
  produtosServicosFiltrados: ProdutoServico[] = []; // resultado da busca
  produtoServicoSelecionando: ProdutoServico | null = null;//produto escolhido
  produtoServicoFiltro: string = ''; // valor digitado no campo
  
  empresas: Empresa[] = [];
  empresasFiltradas: Empresa[] = [];
  empresaSelecionado: Empresa | null = null;// produto escolhido
  empresaFiltro: string = ''; //valor digitado no campo

  clientes: Cliente[] = [];
  clientesFiltrados: Cliente[] = [];
  clienteSelecionado: Cliente | null = null;
  clienteFiltro: string = '';

  contaContabeis: ContaContabil[] = [];
  contaContabilFiltrados: ContaContabil[] = [];
  contaContabilSelecionado: ContaContabil | null = null;
  contaContabilFiltro: string = '';

  private buscaEmpresaSubject = new Subject<string>();
  private buscaProdutoServicoSubject = new Subject<string>();
  private buscaClienteSubject = new Subject<string>();
  private buscaContaContabilSubject = new Subject<string>();

  get f(): any{
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      showWeekNumbers: false
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private lancamentoService: LancamentoService,
    private produtoServicoService: ProdutoServicoService,
    private empresaService: EmpresaService,
    private clienteService: ClienteService,
    private contaContabilService: ContaService
  )
    
  {
    this.localeService.use('pr-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarLancamento();
    this.carregarEmpresa();
    this.carregarProdutoServico();
    this.carregarCliente();
    this.carregarContaContabil();
  }

  OnIputChangeEmpresa(){

     if(this.empresaFiltro.trim() === ''){
       this.empresasFiltradas = []
      }

    this.carregando = true;
    this.buscaEmpresaSubject.next(this.empresaFiltro);
  }

  onInputChangeProdutoServico(){

    if(this.produtoServicoFiltro.trim() ===''){
      this.produtosServicosFiltrados = []
    }

    this.carregando = true;
    this.buscaProdutoServicoSubject.next(this.produtoServicoFiltro);
  }

  onInputChangeCliente(){
    if(this.clienteFiltro.trim() === ''){
      this.clientesFiltrados = []
    }

    this.carregando = true;
    this.buscaClienteSubject.next(this.clienteFiltro);
  }

  onInputChangeContaContabil(){
    if(this.contaContabilFiltro.trim() === ''){
      this.contaContabilFiltrados = [];
    }

    this.carregando = true;
    this.buscaContaContabilSubject.next(this.contaContabilFiltro);
  }

  public validation(): void {
    this.form = this.fb.group({
      //empresa: ['', [Validators.required]],
      //produtoServico: ['', [ Validators.required]],
      //cliente: ['', [Validators.required]],
      quantidade: ['', [ Validators.required]],
      valorItem: ['', [Validators.required]],
      //itensLancamentosGet: this.fb.array([]), // Usando FormArray para lista de itens
      //contaContabil: ['', [ Validators.required]],
      dataLancamento: ['', [Validators.required]],
      observacao: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(300)]]
    });
  }

  selecionarEmpresa(empresa: Empresa){
    this.empresaSelecionado = empresa;
    this.empresaFiltro = empresa.razaoSocial;
    this.empresasFiltradas = [];
  }

  selecionarProdutoServico(produtoServico: ProdutoServico) {
    this.produtoServicoSelecionando = produtoServico;
    this.produtoServicoFiltro = produtoServico.descricao; // Exibe o valor selecionado no input
    this.produtosServicosFiltrados = []; // Limpa o dropdown após seleção
  }

  selecionarCliente(cliente: Cliente){
    this.clienteSelecionado = cliente;
    this.clienteFiltro = cliente.nome;
    this.clientesFiltrados = [];
  }

  selecionarContaContabil(contaContabil: ContaContabil){
    this.contaContabilSelecionado = contaContabil;
    this.contaContabilFiltro = contaContabil.descricao;
    this.contaContabilFiltrados = [];
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

        //formatar a data de lancamento
        this.lancamento.dataLancamento = new Date(this.lancamento.dataLancamento);
          
        this.form.patchValue({...this.lancamento});

        // Exibir empresa, produto, cliente e conta contábil
        this.atualizarFiltros(lancamento);

        //configurar valor e quantidade
        if(lancamento.itensLancamentos){
          lancamento.itensLancamentos.forEach((item) => {
            this.form.patchValue({
              valorItem: item.valorItem,
              quantidade: item.quantidade
            });
          });
        }

        },
        (error: any) => {
          console.error(error);
        },
        () => {},
      );
    }
  }

  private atualizarFiltros(lancamento: Lancamento): void {
    if (lancamento.empresa) {
      this.empresaSelecionado = lancamento.empresa;
      this.empresaFiltro = lancamento.empresa.razaoSocial;
    }
    if (lancamento.produtoServico) {
      this.produtoServicoSelecionando = lancamento.produtoServico;
      this.produtoServicoFiltro = lancamento.produtoServico.descricao;
    }
    if (lancamento.cliente) {
      this.clienteSelecionado = lancamento.cliente;
      this.clienteFiltro = lancamento.cliente.nome;
    }
    if (lancamento.contaContabil) {
      this.contaContabilSelecionado = lancamento.contaContabil;
      this.contaContabilFiltro = lancamento.contaContabil.descricao;
    }
  }

  public salvarLancamento(): void{
    if(!this.form.valid){
      return;
    }

      const dataLancamento = this.form.value.dataLancamento
        ? new Date(this.form.value.dataLancamento).toISOString()
        : null;  

      const dadosLancamento = {
        ...this.form.value,
        empresaId: this.empresaSelecionado?.id,
        produtoServicoId: this.produtoServicoSelecionando?.id,
        clienteId: this.clienteSelecionado?.id,
        contaContabilId: this.contaContabilSelecionado?.id,
        dataLancamento,
        itensLancamentos: [{
          quantidade: this.form.value.quantidade,
          valorItem: this.form.value.valorItem
        }]
      }  

      this.lancamento = (this.estadoSalvar === 'post')
                        ? {...dadosLancamento}
                        : {id: this.lancamento.id,
                          ...dadosLancamento,
                          itensLancamentos: [{
                            id: this.lancamento.itensLancamentos.find(item => item.id)?.id,
                            lancamentoId: this.lancamento.itensLancamentos.find(item => item.lancamentoId)?.lancamentoId,
                            quantidade: this.form.value.quantidade,
                            valorItem: this.form.value.valorItem
                          }]
                        };

      if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
        this.lancamentoService[this.estadoSalvar](this.lancamento).subscribe(
          () => {
            this.toastr.success('Lançamento gravado com sucesso!', 'Sucesso');
          },
          (error: any) => {
            console.error(error);
            this.toastr.error('Error ao salvar o lançamento', 'Error');
          },
          () => {},
        );
      }
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

  buscarProdutosServicos(termo: string) {
    if (!termo.trim()) {
      this.carregando = false;
      return of([]); // Retorna um Observable vazio
    }
    return this.produtoServicoService.GetProdutoServicoTermo(termo);
  }

  carregarProdutoServico(): void{
    this.buscaProdutoServicoSubject
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

  buscarCliente(termo: string){
    if(!termo.trim()){
      this.carregando = false;
      return of([]); // Retorna um Observable vazio
    }
    return this.clienteService.GetClienteTermo(termo);
  }

  carregarCliente(): void{
    this.buscaClienteSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((termo) => this.buscarCliente(termo))
      )
      .subscribe((clientes) => {
        this.clientesFiltrados = clientes;
        this.carregando = false;
      });
  }

  buscarContaContabil(termo: string){
    if(!termo.trim()){
      this.carregando = false;
      return of([]); // Retorna um Observable vazio
    }
    return this.contaContabilService.GetContaContabilTermo(termo);
  }

  carregarContaContabil(): void{
    this.buscaContaContabilSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((termo) => this.buscarContaContabil(termo))
      )
      .subscribe((contaContabeis) => {
        this.contaContabilFiltrados = contaContabeis;
        this.carregando = false;
      });
  }
}
