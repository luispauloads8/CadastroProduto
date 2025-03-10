import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const toaster = inject(ToastrService);


  if(authService.isAuthenticated()){
    return true;
  }

  //toaster.info("Usuário não autenticado");
  return  router.navigate(['/user/login']);
  
};
