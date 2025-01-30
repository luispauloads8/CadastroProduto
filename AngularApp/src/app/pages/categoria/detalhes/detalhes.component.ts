import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { response } from 'express';
import { CommonModule } from '@angular/common';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/Categoria';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  templateUrl: './detalhes.component.html',
  styleUrl: './detalhes.component.css'
})
export class DetalhesComponent implements OnInit {

  categoria!: Categoria;
  estadoSalvar = 'post';

  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  constructor(
    private categoriaService: CategoriaService, 
    private route: ActivatedRoute, 
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService
  ){}

  ngOnInit(): void {

    // const id = Number(this.route.snapshot.paramMap.get('id'));

    // this.categoriaService.GetCategoriaId(id).subscribe(response => {
    //   this.categoria = response;
    // });

    this.validation();
    this.carregaCategoria();
  }

  public carregaCategoria(): void {
    const categoria = this.route.snapshot.paramMap.get('id');
       
    if(categoria != null){
      this.estadoSalvar = 'put';

        this.categoriaService.GetCategoriaId(+categoria).subscribe(
          (categoria: Categoria) => {
            this.categoria = {...categoria};
            this.form.patchValue(this.categoria);
          },
          (error: any) => {
            console.error(error);
          },
          () => {},
        );
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]
    });
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  // public editarCategoria(id: Number, categoria: Categoria): void {
  //   this.categoriaService.(id, categoria).subscribe(response =>{
  //     this.router.navigate(['/categoria']);
  //   });
  // }

  public salvarCategoria(): void {
    if(this.form.valid){
      
      
      this.categoria = (this.estadoSalvar === 'post') 
                    ? {...this.form.value} //passagem por referencia
                    : {id: this.categoria.id, ...this.form.value};
      
      if(this.estadoSalvar === 'post' || this.estadoSalvar === 'put')  {
        this.categoriaService[this.estadoSalvar](this.categoria).subscribe(
          () => this.toastr.success('Categoria grava com Sucesso!', 'Sucesso'),
          (error: any) => {
            console.error(error);
            this.toastr.error('Error ao salvar a categoria', 'Erro');
          },
          () => {}
        );
      }

    }
  }
}
