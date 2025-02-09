import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Cidade } from '../../../models/Cidade';
import { CidadeService } from '../../../services/cidade.service';
import { ToastrService } from 'ngx-toastr';
import { EnumEstado } from '../../../models/EnumEstado';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './detalhesCidade.component.html',
  styleUrl: './detalhesCidade.component.css'
})
export class DetalhesCidadeComponent implements OnInit {

  form!: FormGroup;
  cidade!: Cidade;
  estadoSalvar = 'post';
  estados: { key: number; value: string }[] = [];

  get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private cidaddeService: CidadeService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.validation();
    this.enumEstado();
    this.carregarCidade();
  }

  public enumEstado(): void{
    // Converter Enum para Array utilizável no Select
    this.estados = Object.keys(EnumEstado)
    .filter(key => isNaN(Number(key))) // Filtra apenas as chaves textuais
    .map(key => ({ key: EnumEstado[key as keyof typeof EnumEstado], value: key }));
  }
  

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public validation(): void{
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(150)]],
      estado: [null, Validators.required]
    })
  }

  public salvarCidade(): void{
    if(!this.form.valid){
      return;
    }

    this.cidade = {
      id: this.estadoSalvar === 'put' ? this.cidade.id : undefined,
      ...this.form.value,
      estado: +this.form.value.estado // O operador "+" converte para número de forma simplificada
    };
    

    if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put')  {
      this.cidaddeService[this.estadoSalvar](this.cidade).subscribe(
        () => this.toastr.success('Cidade grava com Sucesso!', 'Sucesso'),
        (error: any) => {
          console.error(error);
          this.toastr.error('Error ao salvar a cidade', 'Erro');
        },
        () => {}
      );
    }
  }

  public carregarCidade(): void{
    const cidade = this.route.snapshot.paramMap.get('id');

    if(cidade != null){
      this.estadoSalvar = 'put';

      this.cidaddeService.GetCidadeId(+cidade).subscribe(
        (cidade: Cidade) => {
          this.cidade = {...cidade};

        this.form.patchValue({
          ...this.cidade,
          estados: this.cidade.estado
        });

        },
        (error: any) => {
          console.error(error);
        },
        () => {}
      )
    }
  }

}
