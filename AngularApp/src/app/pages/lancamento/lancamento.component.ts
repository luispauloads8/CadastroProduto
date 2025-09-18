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
  imports: [RouterModule,ReactiveFormsModule, CommonModule, TituloComponent,  BsDatepickerModule],
  templateUrl: './lancamento.component.html',
  styleUrl: './lancamento.component.css'
})
export class LancamentoComponent implements OnInit {

  ngOnInit(): void {

  }

}
