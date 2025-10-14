import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pessoa } from '../../../models/Pessoa';
import { PessoaService } from '../../../services/pessoa.service';
import { Cidade } from '../../../models/Cidade';
import { CidadeService } from '../../../services/cidade.service';
import {
  debounceTime,
  distinctUntilChanged,
  EMPTY,
  Subject,
  switchMap,
  tap,
} from 'rxjs';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { EnumEstadoCivil } from '../../../models/EnumEstadoCivil';
import { EnumSexo } from '../../../models/EnumSexo';
import { EmpresaService } from '../../../services/empresa.service';
import { Empresa } from '../../../models/Empresa';


@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [
    NgxMaskDirective,
    FormsModule,
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  providers: [provideNgxMask()],
  templateUrl: './detalhesPessoa.component.html',
  styleUrl: './detalhesPessoa.component.css',
})
export class DetalhesPessoaComponent implements OnInit {
  form!: FormGroup;
  estadoSalvar = 'post';
  pessoa!: Pessoa;
  cidadeSelecionada: Cidade | null = null;
  cidadeFiltro: string = '';
  cidades: Cidade[] = [];
  cidadesFiltradas: Cidade[] = [];
  carregando: boolean = false;
  estadoCivil: {key: number; value: string}[] = [];
  sexo: {key: number; value: string}[] = [];

  empresaFiltro: string = '';
  empresasFiltradas: Empresa[] = [];
  empresaSelecionada!: Empresa;
  carregandoEmpresa: boolean = false;

    // Se estiver editando, setar como true
  modoEdicao: boolean = false;

  constructor(
    private fb: FormBuilder,
    private pessoaService: PessoaService,
    private cidadeService: CidadeService,
    private empresaServico: EmpresaService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {}

  private buscaSubject = new Subject<string>();

  public get f(): any {
    return this.form.controls;
  }

  public get fe(): any {
  return (this.form.get('endereco') as FormGroup).controls;
}

  ngOnInit(): void {
    this.validation();
    this.carregarPessoa();
    this.carregaCidade();
    this.enumEstadoCivil();
    this.enumSexo();
  }

  onInputChange() {
    if (this.cidadeFiltro.trim() === '') {
      this.cidadesFiltradas = [];
    }

    this.carregando = true;
    this.buscaSubject.next(this.cidadeFiltro);
  }

  onInputChangePessoa() {
    if (!this.empresaFiltro || (this.empresaSelecionada && this.modoEdicao)) {
      return;
    }

    this.carregandoEmpresa = true;
    this.empresasFiltradas = [];

    // Aqui você chama seu serviço de busca de pessoas
    this.empresaServico.GetEmpresaTermo(this.empresaFiltro).subscribe({
      next: (dados) => {
        this.empresasFiltradas = dados;
        this.carregandoEmpresa = false;
      },
      error: () => {
        this.carregandoEmpresa = false;
      }
    });
  }

 private applyConditionalValidators(): void {
  this.form.get('tipoPessoa')?.valueChanges.subscribe(tipoPessoa => {
    if (tipoPessoa === 1) { // Pessoa Física
      this.form.get('cpf')?.setValidators([Validators.required]);
      this.form.get('dataNascimento')?.setValidators([Validators.required]);
      this.form.get('estadoCivil')?.setValidators([Validators.required]);
      this.form.get('inscricaoEstadual')?.clearValidators();
      this.form.get('inscricaoMunicipal')?.clearValidators();
      this.form.get('cnae')?.clearValidators();
      this.form.get('sexo')?.setValidators([Validators.required]);
      this.form.get('cnpj')?.clearValidators();
    } else { // Pessoa Jurídica
      this.form.get('cnpj')?.setValidators([Validators.required]);
      this.form.get('cpf')?.clearValidators();
      this.form.get('inscricaoEstadual')?.setValidators([Validators.required]);
      this.form.get('inscricaoMunicipal')?.setValidators([Validators.required]);
      this.form.get('cnae')?.setValidators([Validators.required]);
      this.form.get('dataNascimento')?.clearValidators();
      this.form.get('estadoCivil')?.clearValidators();
      this.form.get('sexo')?.clearValidators();
    }

    this.form.get('cpf')?.updateValueAndValidity();
    this.form.get('cnpj')?.updateValueAndValidity();
    this.form.get('dataNascimento')?.updateValueAndValidity();
    this.form.get('estadoCivil')?.updateValueAndValidity();
    this.form.get('sexo')?.updateValueAndValidity();
  });

  // dispara validação inicial (pois o tipo já vem setado como 1)
  this.form.get('tipoPessoa')?.updateValueAndValidity();
}


  private validation(): void {
    this.form = this.fb.group({
      tipoPessoa: [1, Validators.required], // 1 = Fisica, 2 = Juridica
      empresaId: [null, Validators.required],
      nome: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(200),
        ],
      ],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      cpf: ['' ],
      cnpj: [''],
      nacionalidade: [''],
      dataNascimento: [''],
      estadoCivil: [''],
      inscricaoEstadual: [''],
      inscricaoMunicipal: [''],
      cnae: [''],
      sexo: [''],
      endereco: this.fb.group({
        logradouro: ['', [Validators.required]],
        bairro: ['', [Validators.required]],
        caixaPostal: ['', [Validators.required]],
        cep: ['', [Validators.required]],
        
      }),
    });

    this.applyConditionalValidators();
    this.form.updateValueAndValidity();
  }

  selecionaEmpresa(empresa: Empresa) {
    this.empresaSelecionada = empresa;
    this.empresaFiltro = empresa.nomeFantasia;
    this.form.patchValue({ empresaId: empresa.id });
    this.empresasFiltradas = [];

    this.form.patchValue({
      nome: empresa.nomeFantasia
    });
  }

  public enumEstadoCivil(): void{
    this.estadoCivil = Object.keys(EnumEstadoCivil)
    .filter(key => isNaN(Number(key)))
    .map(key => ({key: EnumEstadoCivil[key as keyof typeof EnumEstadoCivil], value: key}));
  }

  public enumSexo(): void{
    this.sexo = Object.keys(EnumSexo)
    .filter(key => isNaN(Number(key)))
    .map(key => ({key: EnumSexo[key as keyof typeof EnumSexo], value: key}))
  }

  public cssValidator(control: any) {
    return {
      'is-invalid': control.invalid && (control.dirty || control.touched),
      'is-valid': control.valid && (control.dirty || control.touched),
    };
  }

  private carregarPessoa() {
    const pessoa = this.route.snapshot.paramMap.get('id');

    if (pessoa != null) {
      this.estadoSalvar = 'put';

      this.pessoaService.GetPessoaId(+pessoa).subscribe(
        (pessoa: Pessoa) => {
          console.log('Dados recebidos do servidor:', pessoa);
          this.pessoa = { ...pessoa };

          //this.form.patchValue({...this.pessoa});

          this.form.patchValue({
            tipoPessoa: pessoa.tipoPessoa,
            nome: pessoa.nome,
            telefone: pessoa.telefone,
            nacionalidade: pessoa.nacionalidade,
            cnae: pessoa.cnae,
            cpf: pessoa.cpf,
            dataNascimento: pessoa.dataNascimento
            ? new Date(pessoa.dataNascimento).toISOString().substring(0, 10)
            : null,
            inscricaoEstadual: pessoa.inscricaoEstadual,
            inscricaoMunicipal: pessoa.inscricaoMunicipal,
            email: pessoa.email,
            cnpj: pessoa.cnpj,
            endereco: {
              enderecoId: pessoa.enderecoId,
              logradouro: pessoa.endereco.logradouro,
              bairro: pessoa.endereco.bairro,
              caixaPostal: pessoa.endereco.caixaPostal,
              cep: pessoa.endereco.cep,
            },
            estadoCivil: pessoa.estadoCivil,  // <-- número recebido do backend
            sexo: pessoa.sexo,
            empresaId: pessoa.empresa?.id,
            //cidade: pessoa.endereco.Cidade?.id // ou descricao, depende de como você preenche
          });

          this.form.get('tipoPessoa')?.updateValueAndValidity();

          // Atualiza o input do autocomplete e a referência da empresa selecionada
          if (pessoa.empresa) {
            this.empresaSelecionada = pessoa.empresa;
            this.empresaFiltro = pessoa.empresa.nomeFantasia; // exibe no input
          }

          //configura a cidade selecionada
          if (pessoa.endereco.cidade) {
            this.cidadeSelecionada = pessoa.endereco.cidade; //exibe os dados da empresa
            this.cidadeFiltro = pessoa.endereco.cidade.descricao; // mostra a descrição no input
          }
        },
        (error: any) => {
          console.error(error);
        },
        () => {}
      );
    }
  }

  public salvarPessoa(): void {
    if (!this.form.valid) {
      return;
    }

    // Formata o CEP como 00000-000
    const cepFormatado = this.form.value.endereco.cep.replace(
      /(\d{5})(\d{3})/,
      '$1-$2'
    );

    // Dados comuns entre post e put
    const dadosPessoa = {
      ...this.form.value,
      cnpj: this.form.value.cnpj || null,
        cidadeId: this.cidadeSelecionada?.id,
        estadoCivil: this.form.value.estadoCivil,
        sexo: this.form.value.sexo
    };

      // Condicional: não enviar CNPJ se for Pessoa Física
      if (this.form.value.tipoPessoa === 1) { // 1 = Física
        delete dadosPessoa.cnpj;
        delete dadosPessoa.inscricaoEstadual;
        delete dadosPessoa.inscricaoMunicipal;
        delete dadosPessoa.cnae;
      } else { // 2 = Jurídica
        dadosPessoa.cnpj = this.form.value.cnpj;
        dadosPessoa.inscricaoEstadual = this.form.value.inscricaoEstadual;
        dadosPessoa.inscricaoMunicipal = this.form.value.inscricaoMunicipal;
        dadosPessoa.cnae = this.form.value.cnae;
        delete dadosPessoa.estadoCivil;
        delete dadosPessoa.nacionalidade;
        delete dadosPessoa.sexo;
        delete dadosPessoa.dataNascimento;
        delete dadosPessoa.cpf;
      }

    if (this.estadoSalvar === 'post') {
      this.pessoa = {
        ...this.pessoa, // preserva tudo que já existe
        ...dadosPessoa, // adiciona/atualiza campos comuns
        endereco: {
          ...this.form.value.endereco,
          cep: cepFormatado,
          //cidade: this.cidadeSelecionada,
          cidadeId: this.cidadeSelecionada?.id
        },
      };
    } else {
      this.pessoa = {
        ...this.pessoa, // mantém os dados atuais
        ...dadosPessoa, // atualiza campos do form
        enderecoId: this.pessoa.endereco?.id ?? null,
        endereco: {
          ...this.pessoa.endereco, // garante que o endereço completo permaneça
        },
      };
    }


    if (this.estadoSalvar === 'post' || this.estadoSalvar === 'put') {
      this.pessoaService[this.estadoSalvar](this.pessoa).subscribe(
        () => {
          this.toastr.success('Cliente gravado com sucesso!', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao salvar o cliente', 'Erro');
          console.error('Erro da API:', error.message ?? error.message);
        },
        () => {}
      );
    }
  }

  selecionaCidade(cidade: Cidade) {
    this.cidadeSelecionada = cidade;
    this.cidadeFiltro = cidade.descricao;
    this.cidadesFiltradas = [];

    // Mantém o foco no input
    setTimeout(() => {
      const input = document.querySelector<HTMLInputElement>(
        'input[placeholder="Informe a cidade"]'
      );
      input?.focus();
    });
  }

  buscarCidades(termo: string) {
    if (!termo.trim()) {
      this.carregando = false;
      return [];
    }
    return this.cidadeService.GetCidadeTermo(termo);
  }

  carregaCidade(): void {
    this.buscaSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((termo) => this.buscarCidades(termo))
      )
      .subscribe((cidades) => {
        this.cidadesFiltradas = cidades;
        this.carregando = false;
      });
  }

  // 🔹 Fechar menu ao clicar fora
  @HostListener('document:click', ['$event'])
  fecharDropdown(event: MouseEvent) {
    const alvo = event.target as HTMLElement;
    if (!alvo.closest('.form-group')) {
      this.cidadesFiltradas = [];
    }
  }

  // 🔹 Fechar menu ao apertar ESC
  @HostListener('document:keydown.escape')
  fecharAoPressionarEsc() {
    this.cidadesFiltradas = [];
  }
}
