import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GrupoConta } from '../../../models/GrupoConta';
import { GrupoService } from '../../../services/grupo.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './detalhesGrupo.component.html',
  styleUrl: './detalhesGrupo.component.css'
})
export class DetalhesGrupoComponent implements OnInit {

  form!: FormGroup;
  grupoConta!: GrupoConta;
  estadoSalvar = 'post';
  
  get f(): any{
    return this.form.controls;
  }

  constructor(
    private grupoContaService: GrupoService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private toastr: ToastrService
  ){}

  ngOnInit(): void {
    this.validation();
    this.carregaGrupoConta();
  }

  public carregaGrupoConta(): void{
    const grupoConta = this.route.snapshot.paramMap.get('id');

    if(grupoConta != null){
      this.estadoSalvar = 'put';

      this.grupoContaService.GetGrupoContaId(+grupoConta).subscribe(
        (grupoConta: GrupoConta) => {
          this.grupoConta = {...grupoConta};
          this.form.patchValue(this.grupoConta);
        },
        (error: any) => {
          console.error(error);
        },
        () => {}
      );
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(150)]]
    });
  }

  public cssValidator(campoForm: FormControl): any{
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public salvarGrupoConta(): void{
    if(!this.form.valid){
      return;
    }

    this.grupoConta = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.grupoConta.id, ...this.form.value};

    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put'){
      this.grupoContaService[this.estadoSalvar](this.grupoConta).subscribe(
        () => {
          this.toastr.success('Grupo de conta gravado sucesso!', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao salvar o grupo de Conta', 'Erro');
        },
        () => {}
      )
    }                      
  }

}
