import {
  HttpErrorResponse,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest
} from '@angular/common/http';
import { inject, isDevMode } from '@angular/core';
import { catchError, EMPTY, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment.development';

export const errorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const toastr = inject(ToastrService);

  // dentro do catchError:
  const production = !isDevMode();

    // 🔹 Função auxiliar de limpeza
  function limparConsole(): void {
    if (production) {
      console.clear(); // produção: limpa imediatamente
    } else {
      setTimeout(() => console.clear(), 800); // dev: mostra rápido antes de limpar
    }
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let friendlyMessage = 'Ocorreu um erro inesperado.';
      let title = 'Erro na Requisição';
      const isDeleteRequest = req.method === 'DELETE';

      // --- Extrai mensagem amigável do corpo do erro ---
      const errorBody = error.error;
      if (errorBody && typeof errorBody === 'object' && (errorBody.mensagem || errorBody.message)) {
        friendlyMessage = errorBody.mensagem || errorBody.message;
      } else if (typeof errorBody === 'string' && errorBody.length > 0) {
        try {
          const parsed = JSON.parse(errorBody);
          friendlyMessage = parsed.mensagem || parsed.message || friendlyMessage;
        } catch {
          friendlyMessage = errorBody;
        }
      }

      // === Tratamento de status específicos ===
      if (error.status === 400 && isDeleteRequest) {
        title = 'Falha na Exclusão';
        toastr.warning(friendlyMessage, title);
      } 
      else if (error.status === 400) {
        title = 'Requisição Inválida';
        toastr.warning(friendlyMessage, title);
        //limparConsole();
      } 
      else if (error.status === 401) {
        title = 'Falha na Autenticação';
        toastr.warning(friendlyMessage, title);
        //limparConsole();
      }
      else if (error.status === 403) {
        title = 'Acesso Negado';
        toastr.warning('Você não tem permissão para acessar este recurso.', title);
        //limparConsole();
      }
      else if (error.status === 404) {
        title = 'Não Encontrado';
        toastr.warning('O recurso solicitado não foi encontrado.', title);
      } 
      else if (error.status >= 500) {
        title = 'Erro Interno do Servidor';
        toastr.error('Ocorreu um erro no servidor. Tente novamente mais tarde.', title);
      } 
      else {
        toastr.error(friendlyMessage, title);
      }

      // 🧠 Em produção, evita logs visuais para qualquer erro tratado
      if (production) {
        console.clear();
        return EMPTY;
      }

      // 🔄 Em desenvolvimento, permite debug no console
      return throwError(() => error);
    })
  );
};
