import { Component, OnInit } from '@angular/core';
import { ContaContabil } from '../../../models/ContaContabil';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GrupoConta } from '../../../models/GrupoConta';
import { GrupoService } from '../../../services/grupo.service';
import { debounceTime, distinctUntilChanged, Subject, switchMap } from 'rxjs';
import { ContaService } from '../../../services/conta.service';

@Component({
  selector: 'app-detalhes',
  standalone: true,
imports: [FormsModule, RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './detalhesConta.component.html',
  styleUrl: './detalhesConta.component.css'
})
export class DetalhesContaComponent implements OnInit {

  form!: FormGroup;
  ContaContabil!: ContaContabil;
  grupoContabeis: GrupoConta[] = [];
  grupoContabilFiltradas: GrupoConta[] = [];
  grupoFiltro: string = '';
  grupoContabilSelecionada: GrupoConta | null = null;
  carregando: boolean = false;
  estadoSalvar = 'post';

  private buscaSubject = new Subject<string>();

  public get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private grupoContaService: GrupoService,
    private contaService: ContaService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.validation();
    this.carregaGrupoConta();
    this.carregarContaContabil();
  }

  onInputChange(){
    if(this.grupoFiltro.trim() === ''){
      this.grupoContabilFiltradas = [];
    }

    this.carregando = true;
    this.buscaSubject.next(this.grupoFiltro);
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public validation(): void{
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(200)]]
    })
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

    this.ContaContabil = {
      id: this.estadoSalvar == 'put' ? this.ContaContabil.id : undefined,
      ...this.form.value,
      grupoContaId: this.grupoContabilSelecionada?.id
    };

    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
      this.contaService[this.estadoSalvar](this.ContaContabil).subscribe(
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
          this.ContaContabil = {...contaContabil};

          this.form.patchValue({...this.ContaContabil});

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

}
