import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { LancamentoService } from '../../../services/lancamento.service';
import { Lancamento } from '../../../models/Lancamento';
import { EmpresaService } from '../../../services/empresa.service';
import { Empresa } from '../../../models/Empresa';
import { EmissaoLancamento } from '../../../models/emissao/lancamento/EmissaoLancamento';

@Component({
  selector: 'app-emissaolancamento',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, RouterModule, FormsModule],
  templateUrl: './emissaolancamento.component.html',
  styleUrl: './emissaolancamento.component.css'
})
export class EmissaolancamentoComponent implements OnInit{

  form!: FormGroup;
  estadoSalvar = 'post';
  empresas: Empresa[] = [];
  empresaSelecionada?: Empresa;
  lancamentoSelecionado?: Lancamento;
  emissaoLancamento?: EmissaoLancamento;
  LancamentoInicio!: Date;
  LancamentoFim!: Date;


  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private lancamentoService: LancamentoService,
    private empresaService: EmpresaService
  ){

  }

  ngOnInit(): void {
    this.inicializarEmissaoLancamento();
    this.carregarEmpresa();
    this.inicializarFormulario();
  }

    private inicializarFormulario(): void {
    this.form = this.fb.group({
      empresaId: [null, Validators.required],
      lancamentoInicio: [null, Validators.required],
      lancamentoFim: [null, Validators.required]
    });
  }

  private inicializarEmissaoLancamento(): void {
    this.emissaoLancamento = {
      empresaId: 0,
      lancamentoInicio: this.LancamentoInicio,
      lancamentoFim: this.LancamentoFim,
      // Adicione outras propriedades padrão aqui
    } as EmissaoLancamento;
  }


  carregarEmpresa(): void {
    this.empresaService.GetEmpresa().subscribe(
      (empresa: Empresa[]) => {
        this.empresas = empresa.map(emp => ({ ...emp }));
      },
      (error: any) => {
        console.error('Erro ao carregar empresas', error);
      }
    );
  }

  onEmpresaSelecionada(empresa: Empresa) {
    this.empresaSelecionada = empresa;

    if (this.emissaoLancamento && empresa) {
      this.emissaoLancamento.empresaId = empresa.id;
      this.emissaoLancamento.lancamentoInicio = this.LancamentoInicio;
      this.emissaoLancamento.lancamentoFim = this.LancamentoFim;
    }
  }

  public imprimir(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const valoresForm = this.form.value;

    const emissaoLancamento: EmissaoLancamento = {
      empresaId: valoresForm.empresaId,
      contaContabilId: 0,
      lancamentoInicio: new Date(valoresForm.lancamentoInicio),
      lancamentoFim: new Date(valoresForm.lancamentoFim)
    };

    this.lancamentoService.imprimirLancamento(emissaoLancamento).subscribe(
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
