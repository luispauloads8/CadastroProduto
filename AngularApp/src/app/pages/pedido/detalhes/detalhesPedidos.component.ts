import { Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClienteService } from '../../../services/cliente.service';
import { ProdutoServico } from '../../../models/ProdutoServico';
import { ToastrService } from 'ngx-toastr';
import { ProdutoServicoService } from '../../../services/produto-servico.service';
import { CommonModule } from '@angular/common';
import { PedidoService } from '../../../services/pedido.service';
import { debounceTime, distinctUntilChanged, of, Subject, switchMap } from 'rxjs';
import { Cliente } from '../../../models/Cliente';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { Pedido } from '../../../models/Pedido';

type  ItemPedidoForm = FormGroup<{
  nome: FormControl<string>;
  valorItem: FormControl<number>;
  valorTotal: FormControl<number>;
  quantidade: FormControl<number>;
  }>;

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [NgxMaskDirective, FormsModule, RouterModule, ReactiveFormsModule, CommonModule,  BsDatepickerModule],
   providers: [provideNgxMask()],
  templateUrl: './detalhesPedidos.component.html',
  styleUrl: './detalhesPedidos.component.css',
})
export class DetalhesPedidosComponent implements OnInit {

 pedidoForm!: FormGroup;
 estadoSalvar = 'post';
 produtos!: ProdutoServico[];
 pedido!: Pedido;
 total: number = 0;
 carregando: boolean = false; // Indica se está carregando os dados
 clientesFiltrados: Cliente[] = [];
 clienteFiltro: string = '';
 clienteSelecionado: Cliente | null = null;

   private buscaClienteSubject = new Subject<string>();

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private produtoService: ProdutoServicoService,
    private toastr: ToastrService,
    private clienteService: ClienteService,
    private pedidoService: PedidoService
  ) {}


  ngOnInit(): void {
    this.criarFormulario();
    this.carregarPedido();
    this.carregarCliente();
    this.carregarProdutos();
  }

  private criarFormulario(): void {
    this.pedidoForm = this.fb.group({
      id: [0],
      descricao: ['', Validators.required],
      clienteId: [null, Validators.required],
      cliente: [null],
      telefone: ['', Validators.required],
      dataPedido: ['', Validators.required],
      formaDePagamento: [null, Validators.required],
      statusPedido: [null, Validators.required],
      valorTotal: [0, Validators.required],
      itensPedido: this.fb.array([])
    }); 
  }

  public get f(): any {
    return this.pedidoForm.controls;
  }

  onInputChangeCliente(): void {
    const termo = this.clienteFiltro.trim();

    if (termo === '') {
      this.clientesFiltrados = [];
      return;
    }

    this.carregando = true;
    this.buscaClienteSubject.next(termo);
  }

  get ItensPedido(): FormArray<ItemPedidoForm> {
    return this.pedidoForm.get('itensPedido') as FormArray<ItemPedidoForm>;
  }

  adicionarItem(produto: ProdutoServico) {
    const ItensPedido: FormGroup = this.fb.group({
      id: 0,
      produtoServicoId: [produto.id],
      nome: [produto.descricao],
      valorItem: [produto.preco],
      quantidade: [1]
    });
    
    ItensPedido.get('quantidade')?.valueChanges.subscribe(() => this.calcularTotal());

    this.ItensPedido.push(ItensPedido);
    this.calcularTotal();
  }

  selecionarCliente(cliente: Cliente){
    this.clienteSelecionado = cliente;
    this.clienteFiltro = cliente.pessoa.nome;
    this.pedidoForm.get('cliente')?.setValue(cliente.pessoa.nome); // atualiza o form
    this.clientesFiltrados = [];
  }

  public cssValidator(control: any) {
    return {
      'is-invalid': control.invalid && (control.dirty || control.touched),
      'is-valid': control.valid && (control.dirty || control.touched),
    };
  }

  calcularTotal() {
    this.total = this.ItensPedido.controls.reduce((acc, item) => {
      const valorItem = item.get('valorItem')?.value || 0;
      const qtd = item.get('quantidade')?.value || 0;
      return acc + valorItem * qtd;
    }, 0);
  }
  
  buscarCliente(termo: string){
      if(!termo.trim()){
        this.carregando = false;
        return of([]);
      }
      return this.clienteService.GetClienteTermo(termo);
  }
  
  carregarCliente(): void{
      this.buscaClienteSubject
        .pipe(
          debounceTime(300),
          distinctUntilChanged(),
          switchMap((termo) => termo.trim() === '' 
          ? of([]) 
          : this.buscarCliente(termo))
        )
        .subscribe((clientes) => {
          this.clientesFiltrados = clientes;
          this.carregando = false;
        });
  }

  carregarProdutos() {
    this.produtoService.GetProdutoServico().subscribe({
      next: (res: any[]) => {
        this.produtos = res.map(p => {
          if (p.imagem) {
            const url = `data:image/jpeg;base64,${p.imagem}`;
            return { ...p, imagem: url };
          }
          return { ...p, imagem: 'assets/default.jpg' };
        });
      },
      error: () => this.toastr.error('Erro ao carregar produtos')
    });
  }

  removerItem(index: number) {
    this.ItensPedido.removeAt(index);
    this.calcularTotal();
  }

  fecharPedido() {

    if (!this.pedidoForm.valid) {
          return;
    }

    const itensPedido = this.pedidoForm.value.itensPedido.map((item: any) => ({
      id: this.estadoSalvar === 'put' && item.id ? item.id : 0,
      pedidoId: this.pedido?.id ?? 0,
      produtoServicoId: item.produtoServico?.id ?? item.produtoServicoId,
      valorItem: item.valorItem,
      valorTotal: item.valorItem * item.quantidade,
      quantidade: item.quantidade 
    }));

    this.pedido = {
      id: this.estadoSalvar === 'put' ? this.pedido.id : 0,
      descricao: this.clienteSelecionado?.pessoa.nome ?? "",
      clienteId: this.clienteSelecionado?.id ?? 0,
      cliente: this.clienteSelecionado!,
      valorTotal: this.total,
      dataPedido: new Date().toISOString(),
      formaDePagamento: +this.pedidoForm.value.formaDePagamento,
      statusPedido: +this.pedidoForm.value.statusPedido,
      telefone: this.pedidoForm.value.telefone,
      itensPedido
    };

    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
      this.pedidoService[this.estadoSalvar](this.pedido).subscribe(
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

  private criarItemForm(item: any): FormGroup {
    const grupo = this.fb.group({
      id:             [Number(item?.id) || 0],
      produtoServicoId: [
        item?.produtoServicoId ?? item?.produtoServico?.id ?? null,
        Validators.required
      ],
      produtoServico: [item?.produtoServico ?? null],

      nome: [
        item?.produtoServico?.descricao || '',
        Validators.required
      ],

      quantidade: [
        item?.quantidade ?? 1,
        [Validators.required, Validators.min(1)]
      ],

      valorItem: [
        item?.valorItem ?? 0,
        [Validators.required, Validators.min(0.01)]
      ],

      valorTotal: [
        item?.valorTotal ?? (item?.valorItem || 0) * (item?.quantidade || 0)
      ]
      });

      grupo.get('quantidade')?.valueChanges.subscribe(() => this.atualizarTotal(grupo));
      grupo.get('valorItem')?.valueChanges.subscribe(() => this.atualizarTotal(grupo));

      return grupo; 
    }

    private atualizarTotal(grupo: FormGroup) {
      const quantidade = Number(grupo.get('quantidade')?.value || 0);
      const valorItem = Number(grupo.get('valorItem')?.value || 0);
      grupo.patchValue({ valorTotal: quantidade * valorItem }, { emitEvent: false });

      this.calcularTotal();
  }


  carregarPedido() : void{
    const pedido = this.route.snapshot.paramMap.get('id');

      if(pedido != null){
        this.estadoSalvar = 'put';

        this.pedidoService.GetPedidoId(+pedido).subscribe(
          (pedido: Pedido) =>{
            this.pedido = {...pedido}

        this.pedidoForm.patchValue({
          descricao: pedido.descricao,
          valorTotal: pedido.valorTotal,
          telefone: pedido.telefone,
          dataPedido: pedido.dataPedido,
          formaDePagamento: +pedido.formaDePagamento,
          statusPedido: +pedido.statusPedido,
          clienteId: pedido.clienteId
        });

          this.ItensPedido.clear();
          
          this.total = pedido.valorTotal ?? 0;

          if (this.pedido.itensPedido?.length) {
            this.pedido.itensPedido.forEach(item =>
              this.ItensPedido.push(this.criarItemForm(item))
            );
          }

          if (pedido.cliente) {
            this.clienteSelecionado = pedido.cliente;
            this.clienteFiltro = pedido.cliente.pessoa.nome;
          }

          },
          () =>{},
          () =>{}
      )
    }
  }
 
}
