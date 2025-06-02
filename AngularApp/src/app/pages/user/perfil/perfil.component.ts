import { Component, OnInit } from '@angular/core';
import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidatorField } from '../../../helpers/ValidatorField';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from '../../../models/Usuario';


@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [TituloComponent, ReactiveFormsModule, CommonModule],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent implements OnInit {

  usuarioUpdate = {} as Usuario;
  form!: FormGroup;

//captura um FormFild apenas com a letra F
  get f(): any{
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toaster: ToastrService
  ){}


  ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
  }

  private carregarUsuario(): void {
    this.authService.getUsuario().subscribe(
      (userRetorno: Usuario) => {
        console.log(userRetorno);
        this.usuarioUpdate = userRetorno;
        this.form.patchValue(this.usuarioUpdate);
        this.toaster.success('Usúario Carregado', 'Sucesso');
      },
      (error) => {
        console.error(error);
        this.toaster.error('Usuário não carregado', 'Erro');
        this.router.navigate(['/dashbord']);
      },
      () => {}
    )
  }


  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmaPassword')
    };

    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      email: ['', [ Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.nullValidator]],
      confirmaPassword: ['', [Validators.required], Validators.nullValidator]
    }, formOptions);
  }

  onSubmit(): void {

    //vai parar aqui se o from estiver invalido
    if(this.form.invalid){
      return;
    }
  }

  public atualizarUsuario() {
    this.usuarioUpdate = {...this.form.value};

    this.authService
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

    public cssValidator(campoForm: FormControl): any {
      return {'is-invalid' : campoForm.errors && campoForm.touched}
    }

}
