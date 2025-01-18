// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { CommonModule } from '@angular/common';
// import { TituloComponent } from '../../../shared/titulo/titulo.component';
// import { FormularioComponent } from '../../../componentes/formulario/formulario.component';
// import { CategoriaService } from '../../../services/categoria.service';
// import { Categoria } from '../../../models/Categoria';
// import { FormGroup } from '@angular/forms';

// @Component({
//   selector: 'app-editar',
//   standalone: true,
//   imports: [FormularioComponent, CommonModule, TituloComponent],
//   templateUrl: './editar.component.html',
//   styleUrl: './editar.component.css'
// })
// export class EditarComponent implements OnInit {
//   btnAcao = "Editar";
//   descTitulo = "Editar Categoria";
//   categoria! :  Categoria;

//   form!: FormGroup;

//   constructor(
//     private categoriaService: CategoriaService, 
//     private router: Router, 
//     private route: ActivatedRoute)
//     {}

//   ngOnInit(): void {
//    // this.carregaCategoria();

//     const id = Number(this.route.snapshot.paramMap.get(`id`))

//     this.categoriaService.GetCategoriaId(id).subscribe(response =>{
//       this.categoria = response;
//     });
//   }

//   // public carregaCategoria(): void {
//   //   const categoria = this.route.snapshot.paramMap.get('id');

//   //  if(categoria != null)
//   //   {
//   //     this.categoriaService.GetCategoriaId(+categoria).subscribe(
//   //        (categoria: Categoria) => {
//   //         this.categoria = {...categoria};
//   //         this.form.patchValue(this.categoria);
//   //        },
//   //        (error: any) => {
//   //         console.error(error);
//   //        },
//   //        () => {},
//   //     );
//   //   }

//   // }

//   editarCategoria(id: Number, categoria: Categoria){
//     this.categoriaService.EditarCategoria(id, categoria).subscribe(response =>{
//       this.router.navigate(['/categoria']);
//     });
//   }
// }
