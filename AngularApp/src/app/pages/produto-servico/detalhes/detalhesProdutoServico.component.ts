import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProdutoServicoService } from '../../../services/produto-servico.service';
import { ProdutoServico } from '../../../models/ProdutoServico';
import { ToastrService } from 'ngx-toastr';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/Categoria';
import { debounceTime, distinctUntilChanged, map, Observable, Subject, switchMap } from 'rxjs';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule,  ReactiveFormsModule],
  templateUrl: './detalhesProdutoServico.component.html',
  styleUrl: './detalhesProdutoServico.component.css'
})
export class DetalhesProdutoServicoComponent implements OnInit {

  produtoServico!: ProdutoServico;
  form!: FormGroup;
  estadoSalvar = 'post';
  categorias: Categoria[] = [];
  categoriaFiltro: string = ''; // Valor digitado no campo
  categoriasFiltradas: Categoria[] = []; // Resultados da busca
  categoriaSelecionada: Categoria | null = null; // Categoria escolhida
  carregando: boolean = false; // Indica se está carregando os dados
  imagemSelecionada: File | null = null;
  imagemPreview: string | null = null; // Armazena a pré-visualização da imagem


  private buscaSubject = new Subject<string>();


  get f(): any{
    return this.form.controls;
  }

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private produtoServicoService: ProdutoServicoService,
              private toastr: ToastrService,
              private categoriaService: CategoriaService, 
  ){}

  ngOnInit(): void {
    this.validation();
    this.carregarProdutoServico();
    this.carregarCategorias();
  }

  
  onInputChange() {
  if (this.categoriaFiltro.trim() === '') {
    this.categoriasFiltradas = [];
  }

    this.carregando = true;
    this.buscaSubject.next(this.categoriaFiltro);
  }

  buscarCategorias(termo: string) {
    if (!termo.trim()) {
      this.carregando = false;
      return [];
    }
    return this.categoriaService.GetCategoriaTermo(termo);
  }

    public validation(): void {
      this.form = this.fb.group({
        descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]
      });
    }

    selecionarCategoria(categoria: Categoria) {
      this.categoriaSelecionada = categoria;
      this.categoriaFiltro = categoria.descricao; // Exibe o valor selecionado no input
      this.categoriasFiltradas = []; // Limpa o dropdown após seleção
    }

    public cssValidator(campoForm: FormControl): any {
      return {'is-invalid' : campoForm.errors && campoForm.touched}
    }

  public carregarProdutoServico() : void {
    const produtoServico = this.route.snapshot.paramMap.get('id');

    if(produtoServico != null){
      this.estadoSalvar = 'put';
      
      this.produtoServicoService.GetProdutoServicoId(+produtoServico).subscribe(
        (produtoServico: ProdutoServico) => {
          this.produtoServico = {...produtoServico};

          // Verifica se há imagem e a converte para base64 corretamente
          if (produtoServico.imagem) {
            this.imagemPreview = `data:image/png;base64,${produtoServico.imagem}`;
          }

          this.form.patchValue(this.produtoServico);

        // Configura a categoria selecionada
        if (produtoServico.categoria) {
          this.categoriaSelecionada = produtoServico.categoria; // Exibe os dados da categoria
          this.categoriaFiltro = produtoServico.categoria.descricao; // Mostra a descrição no input
        }

        },
        (error: any) => {
          console.error(error);
        },
        () => {}
      );
    }
  }

  public salvarProdutoServico(): void {
    if (!this.form.valid) {
        return;
    }

    // Verifique se produtoServico.id está sendo atribuído corretamente
    this.produtoServico = {
        ...this.form.value,
        id: this.produtoServico?.id, // Adicione isso para garantir que o ID seja mantido
        categoriaId: this.categoriaSelecionada?.id,
        imagem: this.produtoServico?.imagem // Mantém a imagem existente por padrão
    };

    if (this.imagemSelecionada instanceof File) {
        this.convertImageToBase64(this.imagemSelecionada)
            .then((base64Image: string) => {
                this.produtoServico.imagem = base64Image.split(',')[1]; // Apenas o conteúdo Base64, sem o prefixo MIME

                if (this.estadoSalvar === 'post') {
                    this.salvarProdutoServicoPost();
                } else if (this.estadoSalvar === 'put') {
                    this.salvarProdutoServicoPut();
                }
            })
            .catch((error) => {
                console.error('Erro ao converter a imagem:', error);
                this.toastr.error('Erro ao processar a imagem', 'Erro');
            });
    } else {
        if (this.estadoSalvar === 'post') {
            this.salvarProdutoServicoPost();
        } else if (this.estadoSalvar === 'put') {
            this.salvarProdutoServicoPut();
        }
    }
}

private salvarProdutoServicoPost(): void {
    this.produtoServicoService.post(this.produtoServico).subscribe(
        () => this.toastr.success('Produto Serviço gravado com sucesso', 'Sucesso'),
        (error: any) => {
            console.error('Erro ao salvar o produto/serviço:', error);
            this.toastr.error('Erro ao salvar o produto/serviço', 'Erro');
        }
    );
}

private salvarProdutoServicoPut(): void {
    console.log('Atualizando produto/serviço:', this.produtoServico); // Log de depuração

    this.produtoServicoService.put(this.produtoServico.id!, this.produtoServico).subscribe(
        () => this.toastr.success('Produto Serviço atualizado com sucesso', 'Sucesso'),
        (error: any) => {
            console.error('Erro ao atualizar o produto/serviço:', error);
            this.toastr.error('Erro ao atualizar o produto/serviço', 'Erro');
        }
    );
}

  
  convertImageToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
  
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = (error) => reject(error);
  
      reader.readAsDataURL(file);
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files[0]) {
      const file = input.files[0];
      this.imagemSelecionada = file;

      // Criar a pré-visualização da imagem
      const reader = new FileReader();
      reader.onload = () => {
        this.imagemPreview = reader.result as string; // Atualiza a imagem exibida
       
      };
      reader.readAsDataURL(file); // Lê o arquivo selecionado como DataURL
    }
  }
  
  carregarCategorias(): void {
    this.buscaSubject
    .pipe(
      debounceTime(300), // Atraso de 300ms após a digitação
      distinctUntilChanged(), // Evita buscas repetidas para o mesmo valor
      switchMap((termo) => this.buscarCategorias(termo))
    )
    .subscribe((categorias) => {
      this.categoriasFiltradas = categorias;
      this.carregando = false;
    });
  }

  onSelect(option: string): void {
    console.log('Selecionado:', option);
  }

}
