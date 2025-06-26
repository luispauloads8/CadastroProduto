import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Categoria } from '../../../models/Categoria';
import { EmissaoCategoria } from '../../../models/emissao/categoria/EmissaoCategoria';
import { CategoriaService } from '../../../services/categoria.service';
import { ParametroEmissaoCategoriaVM } from '../../../models/emissao/categoria/ParametroEmissaoCategoriaVM';

@Component({
  selector: 'app-emissaocategoria',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './emissaocategoria.component.html',
  styleUrl: './emissaocategoria.component.css'
})
export class EmissaocategoriaComponent implements OnInit{


  categorias: Categoria[] = [];
  form!: FormGroup;
  categoriasSelecionadas: Categoria[] = [];


  private fb = inject(FormBuilder);

  constructor(
    private categoriaService: CategoriaService
  ){}

  ngOnInit(): void {
     this.form = this.fb.group({
      categoriasArray: this.fb.array([], Validators.required)
    });

    this.carregarCategorias();
  }

    get categoriasArray(): FormArray {
    return this.form.get('categoriasArray') as FormArray;
  }
  
  public carregarCategorias(): void {
    this.categoriaService.GetCategorias().subscribe({
      next: (categorias: Categoria[]) => {
        this.categorias = categorias;

        // Adiciona um FormControl para cada categoria
        categorias.forEach(() => {
          this.categoriasArray.push(new FormControl(false));
        });
      },
      error: (err) => console.error('Erro ao carregar categorias', err),
    });
  }

  public getCategoriasSelecionadas(): EmissaoCategoria[] {
  return this.categoriasArray.value
    .map((checked: boolean, i: number) => checked ? { id: this.categorias[i].id } : null)
    .filter((val: EmissaoCategoria | null) => val !== null) as EmissaoCategoria[];
  }


  public imprimir(): void {
      if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const categoriasSelecionadas = this.getCategoriasSelecionadas();

    const parametro : ParametroEmissaoCategoriaVM = {
       categoriasvm: categoriasSelecionadas,
    }
  
      this.categoriaService.imprimirLancamento(parametro).subscribe(
        (blob: Blob) => {
          const url = window.URL.createObjectURL(blob);
          const novaAba = window.open(url, '_blank');
  
          if (!novaAba) {
            const link = document.createElement('a');
            link.href = url;
            link.download = 'relatorio.pdf';
            link.click();
          }
        },
        (erro) => {
          console.error('Erro ao gerar PDF:', erro);
        },
        () => {
          console.log('Impressão finalizada');
        }
      );
    }


}
