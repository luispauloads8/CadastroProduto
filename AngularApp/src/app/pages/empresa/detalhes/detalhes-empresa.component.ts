import { Component, OnInit } from '@angular/core';
import { NgxMaskDirective } from 'ngx-mask';
import { EmailMaskDirective } from '../../../shared/directives/email-mask.directive';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Cidade } from '../../../models/Cidade';
import { debounceTime, distinctUntilChanged, Subject, switchMap } from 'rxjs';
import { CidadeComponent } from '../../cidade/cidade.component';
import { CidadeService } from '../../../services/cidade.service';
import { Empresa } from '../../../models/Empresa';
import { EmpresaService } from '../../../services/empresa.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detalhes-empresa',
  standalone: true,
  imports: [NgxMaskDirective , EmailMaskDirective, FormsModule, RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './detalhes-empresa.component.html',
  styleUrl: './detalhes-empresa.component.css'
})
export class DetalhesEmpresaComponent implements OnInit {

  form!: FormGroup;
  estadoSalvar = 'post';
  cidadeFiltro: string = ''; //valor digitado no campo busca de cidade
  cidades: Cidade[] = [];
  cidadesFiltradas: Cidade[] = [];
  cidadeSelecionada: Cidade | null = null; //cidade escollhida
  carregando: boolean = false; // Indica se está carregando os dados
  empresa!: Empresa;

  private buscaSubject = new Subject<string>();

  get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder, 
    private cidadeService: CidadeService,
    private empresaService: EmpresaService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ){}


  ngOnInit(): void {
    this.validation();
    this.carregaCidade();
    this.carregaEmpresa();
  }

  onInputChange(){
    if(this.cidadeFiltro.trim() ===''){
      this.cidadesFiltradas = [];
    }

    this.carregando = true;
    this.buscaSubject.next(this.cidadeFiltro)
  }

    public cssValidator(campoForm: FormControl): any {
      return {'is-invalid' : campoForm.errors && campoForm.touched}
    }

  public validation(): void {
    this.form = this.fb.group({
      razaoSocial: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
      nomeFantasia: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required]],
      cnpj: ['', [Validators.required]]
    })
  }

  public carregaEmpresa(): void{
    const empresa = this.route.snapshot.paramMap.get('id');

    if(empresa != null){
      this.estadoSalvar = 'put';

        this.empresaService.GetEmpresaId(+empresa).subscribe(
          (empresa: Empresa) => {
            this.empresa = {...empresa};
            this.form.patchValue(this.empresa);

            //configura a cidade selecionada
            if(empresa.cidade){
              this.cidadeSelecionada = empresa.cidade; //exibe os dados da empresa
              this.cidadeFiltro = empresa.cidade.descricao; // mostra a descrição no input
            }

          },
          (error: any) => {
            console.error(error);
          },
          () => {}
        )
    }
  }

  public salvarEmpresa(): void{
    if(this.form.valid){
      
      this.empresa = (this.estadoSalvar == 'post')
                      ? {...this.form.value,
                        cidadeId: this.cidadeSelecionada?.id  
                      }
                      : {id: this.empresa.id, 
                        cidadeId: this.cidadeSelecionada?.id,
                        ...this.form.value};

        if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
          this.empresaService[this.estadoSalvar](this.empresa).subscribe(
            () => {
              this.toastr.success('Empresa gravada com sucesso!', 'Sucesso');
            },
            (error: any) => {
              console.error(error);
              this.toastr.error('Error ao salvar a empresa', 'Error');
            },
            () => {}
        );
      }                    
    }
  }

  selecionaCidade(cidade: Cidade){
    this.cidadeSelecionada = cidade;
    this.cidadeFiltro = cidade.descricao; //exibe o valor selecionando
    this.cidadesFiltradas = []; //limpa o dropdown apos seleção
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
