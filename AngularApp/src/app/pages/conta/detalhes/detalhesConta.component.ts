import { Component, OnInit } from '@angular/core';
import { ContaContabil } from '../../../models/ContaContabil';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GrupoConta } from '../../../models/GrupoConta';
import { GrupoService } from '../../../services/grupo.service';
import { debounceTime, distinctUntilChanged, of, Subject, switchMap } from 'rxjs';
import { ContaService } from '../../../services/conta.service';
import { EmpresaService } from '../../../services/empresa.service';
import { Empresa } from '../../../models/Empresa';

@Component({
  selector: 'app-detalhes',
  standalone: true,
imports: [FormsModule, RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './detalhesConta.component.html',
  styleUrl: './detalhesConta.component.css'
})
export class DetalhesContaComponent implements OnInit {

  form!: FormGroup;
  contaContabil!: ContaContabil;
  grupoContabeis: GrupoConta[] = [];
  grupoContabilFiltradas: GrupoConta[] = [];
  grupoFiltro: string = '';
  grupoContabilSelecionada: GrupoConta | null = null;
  carregando: boolean = false;
  estadoSalvar = 'post';

  empresas: Empresa[] = [];
  empresasFiltradas: Empresa[] = [];
  empresaSelecionado: Empresa | null = null;// produto escolhido
  empresaFiltro: string = ''; //valor digitado no campo

  private buscaSubject = new Subject<string>();
  private buscaEmpresaSubject = new Subject<string>();

  public get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private grupoContaService: GrupoService,
    private contaService: ContaService,
    private empresaService: EmpresaService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.validation();
    this.carregaGrupoConta();
    this.carregarContaContabil();
    this.carregarEmpresa();
  }

  onInputChange(){
    if(this.grupoFiltro.trim() === ''){
      this.grupoContabilFiltradas = [];
    }

    this.carregando = true;
    this.buscaSubject.next(this.grupoFiltro);
  }

  OnIputChangeEmpresa(){

     if(this.empresaFiltro.trim() === ''){
       this.empresasFiltradas = []
      }

    this.carregando = true;
    this.buscaEmpresaSubject.next(this.empresaFiltro);
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public validation(): void{
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(200)]]
    })
  }

  selecionarEmpresa(empresa: Empresa){
    this.empresaSelecionado = empresa;
    this.empresaFiltro = empresa.razaoSocial;
    this.empresasFiltradas = [];
  }

  selecionaContaContabil(grupoConta: GrupoConta){
    this.grupoContabilSelecionada = grupoConta;
    this.grupoFiltro = grupoConta.descricao;
    this.grupoContabilFiltradas = [];
  }

  public salvarContaContabil(): void{
    if(!this.form.valid){
      return;
    }

    this.contaContabil = {
      id: this.estadoSalvar == 'put' ? this.contaContabil.id : undefined,
      ...this.form.value,
      grupoContaId: this.grupoContabilSelecionada?.id,
      empresaId: this.empresaSelecionado?.id
    };

    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
      this.contaService[this.estadoSalvar](this.contaContabil).subscribe(
        () => this.toastr.success('Conta Contabil gravada com Sucesso!', 'Sucesso'),
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao salvar a conta contabil', 'Erro');
        },
        () => {}
      )
    }
  }

  public carregarContaContabil(): void{
    const contaContabil = this.route.snapshot.paramMap.get('id');

    if(contaContabil != null){
      this.estadoSalvar = 'put';


      this.contaService.GetContaContabilId(+contaContabil).subscribe(
        (contaContabil: ContaContabil) => {
          this.contaContabil = {...contaContabil};

          this.form.patchValue({...this.contaContabil});

          this.atualizarFiltros(contaContabil);

          if(contaContabil.grupoConta){
            this.grupoContabilSelecionada = contaContabil.grupoConta; // exibe os dados do grupo
            this.grupoFiltro = contaContabil.grupoConta.descricao; // mostra a descrição no input
          }
        },
        (error: any) => {
          console.error(error);
        },
        () => {},
      )

    }
  }

  private atualizarFiltros(contContabil: ContaContabil){
    if (contContabil.empresa) {
      this.empresaSelecionado = contContabil.empresa;
      this.empresaFiltro = contContabil.empresa.razaoSocial;
    }
  }

  buscarGrupoConta(termo: string){
    if(!termo.trim()){
      this.carregando = false;
      return [];
    }
    return this.grupoContaService.GetGrupoContaTermo(termo);
  }

  carregaGrupoConta(): void{
    this.buscaSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((termo) => this.buscarGrupoConta(termo))
      )
      .subscribe((grupoContabeis) => {
        this.grupoContabilFiltradas = grupoContabeis;
        this.carregando = false;
      })
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

}
