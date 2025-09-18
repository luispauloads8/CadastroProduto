import { Component, OnInit} from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClienteService } from '../../../services/cliente.service';
import { ProdutoServico } from '../../../models/ProdutoServico';
import { ToastrService } from 'ngx-toastr';
import { ProdutoServicoService } from '../../../services/produto-servico.service';
import { CommonModule } from '@angular/common';
import { PedidoService } from '../../../services/pedido.service';

type  ItemPedidoForm = FormGroup<{
  nome: FormControl<string>;
  preco: FormControl<number>;
  quantidade: FormControl<number>;
  }>;

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './detalhesPedidos.component.html',
  styleUrl: './detalhesPedidos.component.css',
})
export class DetalhesPedidosComponent implements OnInit {

 pedidoForm: FormGroup;
 produtos!: ProdutoServico[];
 total: number = 0;

  constructor(
    private fb: FormBuilder,
    private produtoService: ProdutoServicoService,
    private toastr: ToastrService
  ) {
    this.pedidoForm = this.fb.group({
      cliente: [''],
      itens: this.fb.array([]),
      pagamento: ['']
    });
  }
  ngOnInit(): void {
    this.pedidoForm = this.fb.group({
    cliente: this.fb.control('', { nonNullable: true, validators: [Validators.required] }),
    pagamento: this.fb.control('', { nonNullable: true, validators: [Validators.required] }),
    itens: this.fb.array<ItemPedidoForm>([])
  });

  // this.carregarClientes();
   this.carregarProdutos();
  }

  get itens(): FormArray<ItemPedidoForm> {
  return this.pedidoForm.get('itens') as FormArray<ItemPedidoForm>;
  }

    // Adiciona item ao FormArray
  adicionarItem(produto: ProdutoServico) {
    const item: FormGroup = this.fb.group({
      nome: [produto.descricao],
      preco: [produto.preco],
      quantidade: [1]
    });
    
    // Atualiza o total quando a quantidade mudar
    item.get('quantidade')?.valueChanges.subscribe(() => this.calcularTotal());

    this.itens.push(item);
    this.calcularTotal();
  }

    // Calcula o total do pedido
  calcularTotal() {
    this.total = this.itens.controls.reduce((acc, item) => {
      const preco = item.get('preco')?.value || 0;
      const qtd = item.get('quantidade')?.value || 0;
      return acc + preco * qtd;
    }, 0);
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
    this.itens.removeAt(index);
  }

  fecharPedido() {
    console.log(this.pedidoForm.value);
    alert('Pedido enviado!');
  }
}
