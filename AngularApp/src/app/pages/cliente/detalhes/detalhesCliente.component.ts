import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Cliente } from '../../../models/Cliente';
import { ClienteService } from '../../../services/cliente.service';
import { ToastrService } from 'ngx-toastr';
import { EnumEstadoCivil } from '../../../models/EnumEstadoCivil';
import { EmailMaskDirective } from '../../../shared/directives/email-mask.directive';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { EnumSexo } from '../../../models/EnumSexo';
import { Cidade } from '../../../models/Cidade';
import { debounceTime, distinctUntilChanged, Subject, switchMap } from 'rxjs';
import { CidadeService } from '../../../services/cidade.service';
import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pr-br', ptBrLocale);

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [NgxMaskDirective, EmailMaskDirective, FormsModule, RouterModule, CommonModule, ReactiveFormsModule, BsDatepickerModule],
  providers: [provideNgxMask(), DatePipe],
  templateUrl: './detalhesCliente.component.html',
  styleUrl: './detalhesCliente.component.css'
})
export class DetalhesClienteComponent implements OnInit {

  form!: FormGroup;
  cliente!: Cliente;
  estadoSalvar = 'post';
  estadoCivil: {key: number; value: string}[] = [];
  sexo: {key: number; value: string}[] = [];
  cidadeFiltro: string = '';
  cidades: Cidade[] = [];
  cidadesFiltradas: Cidade[] = [];
  cidadeSelecionada: Cidade | null = null;
  carregando: boolean = false;
  mask: string = '00.000.00'; // Máscara inicial para 7 dígitos

  private buscaSubject = new Subject<string>();

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      showWeekNumbers: false
    };
  }
  
  public get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private clienteService: ClienteService,
    private cidadeService: CidadeService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  )
  {
    this.localeService.use('pr-br');
  }

  ngOnInit(): void {
    this.validation();
    this.enumEstadoCivil();
    this.enumSexo();
    this.carregarCliente();
    this.carregaCidade();
  }
 //Isso ajustará automaticamente a altura do <textarea> à medida que o usuário digita.
  ajustarAltura(textarea: HTMLTextAreaElement) {
    textarea.style.height = 'auto';
    textarea.style.height = textarea.scrollHeight + 'px';
  }

  onInputChange(){
    if(this.cidadeFiltro.trim() === ''){
      this.cidadesFiltradas = [];
    }

    this.carregando = true;
    this.buscaSubject.next(this.cidadeFiltro);
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

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  definirMascara(valor?: string) {
      // Se não for passado um valor, usa o do formulário
      const rg = valor || this.form.get('rg')?.value || '';
      const numeros = rg.replace(/\D/g, ''); // Remove não numéricos
    

      if (numeros.length >= 7) {
        this.mask = '00.000.000-0'; // Troca para 9 dígitos
      } else {
        this.mask = '00.000.00'; // Mantém 7 dígitos
      }

    // Garante que o Angular detecte a mudança
    this.form.get('rg')?.updateValueAndValidity({ onlySelf: true });
  }

  public validation(): void{
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(200)]],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      cep: ['', [Validators.required]],
      endereco: ['', [Validators.required]],
      nacionalidade: ['', [Validators.required]],
      estadoCivil: ['', Validators.required],
      sexo: ['', [Validators.required]],
      dataNascimento: ['', [Validators.required]],
      cpf: ['',  [Validators.required]],
      rg: ['', [Validators.required]],
      observacao: ['']  // Adicionando o campo observação sem validação obrigatória
      
    })
  }
  

  public salvarCliente(): void{
    if(!this.form.valid){
      return;
    }

    // Obtendo o valor do campo dataNascimento do formulário
    const dataNascimento = this.form.value.dataNascimento 
      ? new Date(this.form.value.dataNascimento).toISOString() // Converte para DateTime no formato ISO 8601
      : null;

    // Formata o CEP como 00000-000
    const cepFormatado = this.form.value.cep.replace(/(\d{5})(\d{3})/, '$1-$2');

    // Dados comuns entre post e put
    const dadosCliente = {
      ...this.form.value,
      cep: cepFormatado,
      cidadeId: this.cidadeSelecionada?.id,
      estadoCivil: +this.form.value.estadoCivil,
      sexo: +this.form.value.sexo,
      dataNascimento
    };

    if (this.estadoSalvar === 'post') {
      this.cliente = {
        ...dadosCliente
      };
    } else {
      this.cliente = {
        id: this.cliente.id,
        ...dadosCliente
      };
    }


    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
      this.clienteService[this.estadoSalvar](this.cliente).subscribe(
        () => {this.toastr.success('Cliente gravado com sucesso!', 'Sucesso')},
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao salvar o cliente', 'Erro');
        },
        () => {},
      )
    }
  }

  public carregarCliente(){
    const cliente = this.route.snapshot.paramMap.get('id');

    if(cliente != null){
      this.estadoSalvar = 'put';

      this.clienteService.GetClienteId(+cliente).subscribe(
        (cliente: Cliente) => {
          console.log("Dados recebidos do servidor:", cliente);
          this.cliente = {...cliente};

        // Formatar a data de nascimento
        this.cliente.dataNascimento = new Date(this.cliente.dataNascimento);

        this.form.patchValue({...this.cliente});
          
        console.log(this.cliente.dataNascimento);

        //configura a cidade selecionada
        if(cliente.cidade){
          this.cidadeSelecionada = cliente.cidade; //exibe os dados da empresa
          this.cidadeFiltro = cliente.cidade.descricao; // mostra a descrição no input
        }

        },
        (error: any) => {
          console.error(error);
        },
        () => {},
      )
    }
  }

  selecionaCidade(cidade: Cidade){
    this.cidadeSelecionada = cidade;
    this.cidadeFiltro = cidade.descricao;
    this.cidadesFiltradas = [];
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


}
