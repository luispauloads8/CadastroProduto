// src/app/services/toast.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  // Em uma aplicação real, injete e use MatSnackBar do Angular Material ou uma biblioteca similar.
  // Ex: constructor(private snackBar: MatSnackBar) {}

  showError(message: string): void {
   // console.error('Mensagem de Erro para o Usuário:', message);
    // Substitua este 'alert' pela sua implementação de toast/snackbar (MatSnackBar, Toastr, etc.)
    alert(`Erro de Operação:\n${message}`);
  }

  showWarning(message: string): void {
    //console.warn('Mensagem de Aviso para o Usuário:', message);
    // Implementação para avisos...
    alert(`Aviso:\n${message}`);
  }
}