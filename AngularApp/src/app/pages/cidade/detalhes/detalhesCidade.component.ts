import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Cidade } from '../../../models/Cidade';
import { CidadeService } from '../../../services/cidade.service';
import { ToastrService } from 'ngx-toastr';

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

  get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private cidaddeService: CidadeService,
    private toastr: ToastrService,
  ){}

  ngOnInit(): void {
    this.validation();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public validation(): void{
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(150)]]
    })
  }

  public salvarCidade(): void{
    if(!this.form.valid){
      return;
    }

    this.cidade = {
      ...this.form.value,
      estado: this.cidade.estado
    }

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

}
