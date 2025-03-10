import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, AbstractControlOptions, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ValidatorField } from '../../../helpers/ValidatorField';
import { User } from '../../../models/User';
import { AuthService } from '../../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterModule } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent implements OnInit {

  User = {} as User;
  form!: FormGroup;

  get f(): any {
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
  }

  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmaPassword')
    };

    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      email: ['', [ Validators.required, Validators.email]],
      //usuario: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmaPassword: ['', [Validators.required]]
    }, formOptions);
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid' : campoForm.errors && campoForm.touched}
  }

  public register(): void {

    if(!this.form.valid){
      return;
    }


    this.User = { ...this.form.value };
    this.authService.register(this.User).subscribe(
      () => this.router.navigateByUrl('/dashboard'),
      (error: HttpErrorResponse) => {
        // Remove o console.error e trata o erro silenciosamente
        try {
          if (error.status === 409 && error.error?.message === 'User already exists!') {
              this.toaster.error('O usuário já existe! Tente usar um nome de usuário diferente.', 'Erro');
              return; // Opcional: Se quiser interromper a execução do código aqui
          }
      } catch (e) {
          console.log('Um erro ocorreu, mas está sendo tratado de forma amigável.');
      }
      
      
      }
    );
  }
}
