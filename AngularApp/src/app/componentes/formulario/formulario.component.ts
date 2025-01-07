import { Component, EventEmitter, Input, OnInit, Output, output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink, RouterModule } from '@angular/router';
import { Categoria } from '../../models/Categoria';

@Component({
  selector: 'app-formulario',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './formulario.component.html',
  styleUrl: './formulario.component.css'
})
export class FormularioComponent implements OnInit {
  @Input() btnAcao!:string;
  @Input() descTitulo!:string;
  @Input() dadosCategoria : Categoria | null = null;
  @Output() onSubmit = new EventEmitter<Categoria>();

  categoriaForm!: FormGroup;

  ngOnInit(): void {
    this.categoriaForm = new FormGroup({
      id: new FormControl(this.dadosCategoria ? this.dadosCategoria.id : 0),
      descricao: new FormControl(this.dadosCategoria ? this.dadosCategoria.descricao : '')
    });
  }

  submit(){
    this.onSubmit.emit(this.categoriaForm.value);
  }

}
