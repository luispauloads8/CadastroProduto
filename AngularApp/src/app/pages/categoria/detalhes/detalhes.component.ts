import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { response } from 'express';
import { CommonModule } from '@angular/common';
import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { CategoriaService } from '../../../services/categoria.service';
import { Categoria } from '../../../models/Categoria';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [RouterModule, CommonModule, TituloComponent],
  templateUrl: './detalhes.component.html',
  styleUrl: './detalhes.component.css'
})
export class DetalhesComponent implements OnInit {

  categoria!: Categoria;
  descTitulo = "Detalhes Categoria";

  constructor(private categoriaService: CategoriaService, private route: ActivatedRoute ){}

  ngOnInit(): void {

    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.categoriaService.GetCategoriaId(id).subscribe(response => {
      this.categoria = response;
    });

  }

}
