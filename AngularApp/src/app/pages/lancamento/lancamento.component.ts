import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';

import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pr-br', ptBrLocale);

@Component({
  selector: 'app-lancamento',
  standalone: true,
  imports: [RouterModule, TituloComponent, ReactiveFormsModule, CommonModule,  BsDatepickerModule],
  templateUrl: './lancamento.component.html',
  styleUrl: './lancamento.component.css'
})
export class LancamentoComponent implements OnInit {

  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  get bsConfig(): any {
    return { 
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder, private localeService: BsLocaleService){
    this.localeService.use('pr-br');
  }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      empresa: ['', [Validators.required]],
      produtoServico: ['', [ Validators.required]],
      cliente: ['', [Validators.required]],
      quantidade: ['', [ Validators.required]],
      valor: ['', [Validators.required]],
      contaContabil: ['', [ Validators.required]],
      dataLancamento: ['', [Validators.required]],
      observacao: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(300)]],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

    public cssValidator(campoForm: FormControl): any {
      return {'is-invalid' : campoForm.errors && campoForm.touched}
    }

}
