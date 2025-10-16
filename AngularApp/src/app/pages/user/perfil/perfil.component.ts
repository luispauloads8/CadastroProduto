import { Component, OnInit } from '@angular/core';
import { TituloComponent } from '../../../shared/titulo/titulo.component';
import { AbstractControl, AbstractControlOptions, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidatorField } from '../../../helpers/ValidatorField';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from '../../../models/Usuario';
import { UserUpdate } from '../../../models/UserUpdate';
import { error } from 'node:console';


@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [TituloComponent, ReactiveFormsModule, CommonModule],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent implements OnInit {

  usuarioUpdate = {} as UserUpdate;
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
      (userRetorno: UserUpdate) => {
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

  passwordMatchValidator: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password')?.value;
    const confirm = group.get('confirmaPassword')?.value;
    return password === confirm ? null : { mustMatch: true };
  };

  private validation(): void {

    this.form = this.fb.group({
      nome: [''],
      email: ['', [ Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmaPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  onSubmit(): void {
    this.atualizarUsuario();
  }

  public atualizarUsuario() {
    this.usuarioUpdate = {...this.form.value};

    this.authService.updateUsuario(this.usuarioUpdate).subscribe(
      () => this.toaster.success('Usuário atualizado', 'Sucesso'),
      (error) => {
        this.toaster.error(error.error);
        console.error(error);
      },
    )
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

    public cssValidator(campoForm: FormControl): any {
      return {'is-invalid' : campoForm.errors && campoForm.touched}
    }

}
