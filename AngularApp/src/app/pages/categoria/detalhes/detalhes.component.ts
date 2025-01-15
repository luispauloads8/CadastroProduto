import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { response } from 'express';
import { CommonModule } from '@angular/common';
import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/Categoria';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [RouterModule, CommonModule, TituloComponent, ReactiveFormsModule],
  templateUrl: './detalhes.component.html',
  styleUrl: './detalhes.component.css'
})
export class DetalhesComponent implements OnInit {

  categoria!: Categoria;
  descTitulo = "Detalhes Categoria";
  
  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  constructor(private categoriaService: CategoriaService, private route: ActivatedRoute, private fb: FormBuilder){}

  ngOnInit(): void {

    // const id = Number(this.route.snapshot.paramMap.get('id'));

    // this.categoriaService.GetCategoriaId(id).subscribe(response => {
    //   this.categoria = response;
    // });

    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]
    });
  }

}
