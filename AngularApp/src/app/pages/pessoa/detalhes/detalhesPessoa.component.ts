import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pessoa } from '../../../models/Pessoa';
import { PessoaService } from '../../../services/pessoa.service';
import { Cidade } from '../../../models/Cidade';
import { CidadeService } from '../../../services/cidade.service';
import { debounceTime, distinctUntilChanged, EMPTY, Subject, switchMap, tap } from 'rxjs';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';


@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [NgxMaskDirective,FormsModule, RouterModule, CommonModule, ReactiveFormsModule],
    providers: [provideNgxMask()],
  templateUrl: './detalhesPessoa.component.html',
  styleUrl: './detalhesPessoa.component.css'
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

  constructor(
    private fb: FormBuilder,
    private pessoaService: PessoaService,
    private cidadeService: CidadeService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ){}

  private buscaSubject = new Subject<string>();

  public get f(): any{
  return this.form.controls;
  }

  ngOnInit(): void {
    this.validation();
    this.carregarPessoa();
    this.carregaCidade();
  }

    onInputChange(){
    if(this.cidadeFiltro.trim() === ''){
      this.cidadesFiltradas = [];
    }

    this.carregando = true;
    this.buscaSubject.next(this.cidadeFiltro);
  }

  private validation(): void{
    this.form = this.fb.group({
          nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(200)]],
          telefone: ['', [Validators.required]],
          email: ['', [Validators.required, Validators.email]],
          cnpj: ['', [Validators.required]],
          endereco: this.fb.group({
            logradouro: ['', [Validators.required]],
            bairro: ['', [Validators.required]],
            caixaPostal: ['', [Validators.required]],
            cep: ['', [Validators.required]],
          }),
        })
  }


  public cssValidator(control: any) {
    return {
      'is-invalid': control.invalid && (control.dirty || control.touched),
      'is-valid': control.valid && (control.dirty || control.touched)
    };
  }

  private carregarPessoa(){
      const pessoa = this.route.snapshot.paramMap.get('id');
  
      if(pessoa != null){
        this.estadoSalvar = 'put';
  
        this.pessoaService.GetPessoaId(+pessoa).subscribe(
          (pessoa: Pessoa) => {
            console.log("Dados recebidos do servidor:", pessoa);
            this.pessoa = {...pessoa};
  
          //this.form.patchValue({...this.pessoa});

            this.form.patchValue({
            nome: pessoa.nome,
            telefone: pessoa.telefone,
            email: pessoa.email,
            cnpj: pessoa.cnpj,
            endereco: {
              enderecoId: pessoa.enderecoId,
              logradouro: pessoa.endereco.logradouro,
              bairro: pessoa.endereco.bairro,
              caixaPostal: pessoa.endereco.caixaPostal,
              cep: pessoa.endereco.cep
            },
            //cidade: pessoa.endereco.Cidade?.id // ou descricao, depende de como você preenche
          });

                  //configura a cidade selecionada
        if(pessoa.endereco.cidade){
          this.cidadeSelecionada = pessoa.endereco.cidade; //exibe os dados da empresa
          this.cidadeFiltro = pessoa.endereco.cidade.descricao; // mostra a descrição no input
        }

          },
          (error: any) => {
            console.error(error);
          },
          () => {},
        )
      }
  }


  public salvarPessoa(): void{
    if(!this.form.valid){
      return;
    }

    // Formata o CEP como 00000-000
    const cepFormatado = this.form.value.endereco.cep.replace(/(\d{5})(\d{3})/, '$1-$2');

    // Dados comuns entre post e put
    const dadosPessoa = {
      ...this.form.value,
     endereco: {
      ...this.form.value.endereco,
      cep: cepFormatado,
      cidade: this.cidadeSelecionada
        ? {
            ...this.cidadeSelecionada,
            id: this.cidadeSelecionada.id // garante que o ID da cidade seja enviado
          }
        : null,
      cidadeId: this.cidadeSelecionada?.id,
      id: this.pessoa.endereco?.id ?? null // garante que o ID do endereço vá no update
    },
    enderecoId: this.pessoa.enderecoId ?? null
    };

    if (this.estadoSalvar === 'post') {
      this.pessoa = {
        ...dadosPessoa
      };
    } else {
      this.pessoa = {
        id: this.pessoa.id,
        ...dadosPessoa
      };
    }

    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
      this.pessoaService[this.estadoSalvar](this.pessoa).subscribe(
        () => {this.toastr.success('Cliente gravado com sucesso!', 'Sucesso')},
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao salvar o cliente', 'Erro');
        },
        () => {},
      )
    }
  }

  selecionaCidade(cidade: Cidade) {
    this.cidadeSelecionada = cidade;
    this.cidadeFiltro = cidade.descricao;
    this.cidadesFiltradas = [];
  
    // Mantém o foco no input
      setTimeout(() => {
        const input = document.querySelector<HTMLInputElement>('input[placeholder="Informe a cidade"]');
        input?.focus();
      });
    }
  
    buscarCidades(termo: string){
      if(!termo.trim()){
        this.carregando = false;
        return [];
      }
      return this.cidadeService.GetCidadeTermo(termo);
    }
  
    carregaCidade(): void{
      this.buscaSubject
        .pipe(
          debounceTime(300),
          distinctUntilChanged(),
          switchMap((termo) => this.buscarCidades(termo))
        )
        .subscribe((cidades) => {
          this.cidadesFiltradas = cidades;
          this.carregando = false;
        })
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
