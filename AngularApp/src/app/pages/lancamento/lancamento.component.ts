import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TituloComponent } from '../../shared/titulo/titulo.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lancamento',
  standalone: true,
  imports: [RouterModule, TituloComponent, ReactiveFormsModule, CommonModule],
  templateUrl: './lancamento.component.html',
  styleUrl: './lancamento.component.css'
})
export class LancamentoComponent implements OnInit {

  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  constructor(private fb: FormBuilder){}

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

}
