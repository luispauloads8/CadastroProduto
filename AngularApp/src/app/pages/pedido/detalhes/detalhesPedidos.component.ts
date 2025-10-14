import { Component, OnInit} from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
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

 pedidoForm: FormGroup;
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
  ) {
    this.pedidoForm = this.fb.group({
      cliente: [''],
      ItensPedido: this.fb.array([]),
      formaPagamento: ['']
    });
  }


  ngOnInit(): void {
  this.validation();
  this.carregarPedido();
  this.carregarCliente();
  this.carregarProdutos();
  }

  public validation(): void {
        this.pedidoForm = this.fb.group({
      cliente: this.fb.control('', { nonNullable: true, validators: [Validators.required] }),
      formaDePagamento: [null, Validators.required],
      statusPedido:    [null, Validators.required],
      telefone: this.fb.control('', { nonNullable: true, validators: [Validators.required] }),
      ItensPedido: this.fb.array<ItemPedidoForm>([], [Validators.required])
    });
  }

  public get f(): any {
    return this.pedidoForm.controls;
  }

onInputChangeCliente(): void {
  const termo = this.clienteFiltro.trim();

  if (termo === '') {
    // limpa imediatamente quando apaga o campo
    this.clientesFiltrados = [];
    return;
  }

  this.carregando = true;
  this.buscaClienteSubject.next(termo);
}

  get ItensPedido(): FormArray<ItemPedidoForm> {
  return this.pedidoForm.get('ItensPedido') as FormArray<ItemPedidoForm>;
  }

    // Adiciona item ao FormArray
  adicionarItem(produto: ProdutoServico) {
    const ItensPedido: FormGroup = this.fb.group({
      id: [produto.id],
      nome: [produto.descricao],
      valorItem: [produto.preco],
      quantidade: [1]
    });
    
    // Atualiza o total quando a quantidade mudar
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


    // Calcula o total do pedido
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
        return of([]); // Retorna um Observable vazio
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
            // cria URL base64 a partir da string recebida da API
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
    this.calcularTotal(); // 🔑 Recalcula o total após a remoção
  }

  fecharPedido(){

    if(!this.pedidoForm.valid){
      return;
    }

    const pedido = {
      ...this.pedidoForm.value,
      descricao: this.clienteSelecionado?.pessoa.nome,
      cliente: this.clienteSelecionado,
      clienteId: this.clienteSelecionado?.id,
      valorTotal: this.total,
      dataPedido: new Date().toISOString(),
      formaDePagamento: +this.pedidoForm.value.formaDePagamento,
      statusPedido: +this.pedidoForm.value.statusPedido,
        itensPedido: this.pedidoForm.value.ItensPedido.map((item: any) => ({
        produtoServicoId: item.id,
        valorItem: item.valorItem,
        valorTotal: item.valorItem * item.quantidade,
        quantidade: item.quantidade,
      }))
    }

    this.pedido = (this.estadoSalvar === 'post')
                  ? {...pedido} 
                  : {id: this.pedido.id,
                    ...pedido,
                      clienteId: this.clienteSelecionado?.id,
                      itensPedido: this.pedidoForm.value.ItensPedido.map((item: any) => ({
                      id: item.id,
                      pedidoId: this.pedido.id,
                      produtoServicoId: item.id,  
                      valorItem: item.valorItem,
                      valorTotal: item.valorItem * item.quantidade,
                      quantidade: item.quantidade 
                    }))
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
    id:         [item.id],
    nome:       [item.produtoServico?.descricao || ''],
    valorItem:  [item.valorItem],
    valorTotal: [item.valorTotal],
    quantidade: [item.quantidade]
  });

    grupo.get('quantidade')?.valueChanges.subscribe(() => this.calcularTotal());
    return grupo;
  }

  carregarPedido() : void{
    const pedido = this.route.snapshot.paramMap.get('id');

      if(pedido != null){
        this.estadoSalvar = 'put';

        this.pedidoService.GetPedidoId(+pedido).subscribe(
          (pedido: Pedido) =>{
            this.pedido = {...pedido}

            this.pedidoForm.patchValue({...this.pedido});

            // *** NOVO trecho ***
          this.ItensPedido.clear(); // esvazia o FormArray
          
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
