import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Usuario } from '../../../models/Usuario';
import { UserLogin } from '../../../models/UserLogin';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  userLogin = {} as UserLogin;

  constructor(
    private authService: AuthService,
    private router: Router,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {}

  public login(): void {

    if (!this.userLogin.userName || !this.userLogin.password) {
    this.toaster.warning('Preencha usuário e senha antes de continuar.');
    return; // Interrompe a execução se estiver faltando algo
  }
    
    this.authService.login(this.userLogin).subscribe(
      () => {
        this.router.navigateByUrl('/dashboard');
      }
    );
  }

}
