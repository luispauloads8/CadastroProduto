import { Routes } from '@angular/router';
import { CategoriaComponent } from './pages/categoria/categoria.component';
import { CadastroComponent } from './pages/categoria/cadastro/cadastro.component';
import { title } from 'process';
//import { EditarComponent } from './pages/categoria/editar/editar.component';
import { DetalhesComponent } from './pages/categoria/detalhes/detalhes.component';
import { ProdutoServicoComponent } from './pages/produto-servico/produto-servico.component';
import { PerfilComponent } from './pages/user/perfil/perfil.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserComponent } from './pages/user/user.component';
import { LoginComponent } from './pages/user/login/login.component';
import { RegistrationComponent } from './pages/user/registration/registration.component';
import { LancamentoComponent } from './pages/lancamento/lancamento.component';
import { DetalhesProdutoServicoComponent } from './pages/produto-servico/detalhes/detalhesProdutoServico.component';
import { CadastroProdutoServicoComponent } from './pages/produto-servico/cadastro/cadastroProdutoServico.component';
import { DetalhesLancamentosComponent } from './pages/lancamento/detalhes/detalhesLancamentos.component';
import { CadastroLancamentosComponent } from './pages/lancamento/cadastro/cadastroLancamentos.component';
import { CidadeComponent } from './pages/cidade/cidade.component';
import { DetalhesCidadeComponent } from './pages/cidade/detalhes/detalhesCidade.component';
import { CadastroCidadeComponent } from './pages/cidade/cadastro/cadastroCidade.component';
import { EmpresaComponent } from './pages/empresa/empresa.component';
import { DetalhesEmpresaComponent } from './pages/empresa/detalhes/detalhes-empresa.component';
import { CadastroEmpresaComponent } from './pages/empresa/cadastro/cadastro-empresa.component';
import { ClienteComponent } from './pages/cliente/cliente.component';
import { DetalhesClienteComponent } from './pages/cliente/detalhes/detalhesCliente.component';
import { CadastroClienteComponent } from './pages/cliente/cadastro/cadastroCliente.component';
import { combineLatest } from 'rxjs';
import { ContaComponent } from './pages/conta/conta.component';
import { DetalhesContaComponent } from './pages/conta/detalhes/detalhesConta.component';
import { CadastroContaComponent } from './pages/conta/cadastro/cadastroConta.component';
import { GrupoComponent } from './pages/grupo/grupo.component';
import { DetalhesGrupoComponent } from './pages/grupo/detalhes/detalhesGrupo.component';
import { CadastroGrupoComponent } from './pages/grupo/cadastro/cadastroGrupo.component';
import { authGuard } from './guard/auth.guard';


export const routes: Routes = [
    {
        path: 'user', component: UserComponent,
        children: [
            {path: 'login', component: LoginComponent, title: 'Login'},
            {path: 'registration', component: RegistrationComponent, title: "Registra"}
        ]
    },
    {path: 'user/perfil', component: PerfilComponent, title: 'Perfil'},
    {path: 'categoria', redirectTo: 'categoria/cadastro'},

    {path: 'categoria', component: CategoriaComponent, title: 'Categoria', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroComponent, title: 'Cadastro', canActivate: [authGuard]}
        ]
    },
    {path: 'lancamento', component: LancamentoComponent, title: 'Lancamentos', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesLancamentosComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesLancamentosComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroLancamentosComponent, title: 'Cadastro', canActivate: [authGuard]}
        ]
    },
    {path: 'produtoServico', component: ProdutoServicoComponent, title: 'Produtos Serviços', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesProdutoServicoComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesProdutoServicoComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroProdutoServicoComponent, title: 'Cadastro', canActivate: [authGuard]}
        ]
    },
    {path: 'cidade', component: CidadeComponent, title: 'Cidades', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesCidadeComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesCidadeComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroCidadeComponent, title: 'Cadastro', canActivate: [authGuard]},
        ]
    },
    {path: 'empresa', component: EmpresaComponent, title: 'Empresa', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesEmpresaComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesEmpresaComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroEmpresaComponent, title: 'Cadastro', canActivate: [authGuard]},
        ]
    },
    {path: 'cliente', component: ClienteComponent, title: 'Cliente', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesClienteComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesClienteComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroClienteComponent, title: 'Cadastro', canActivate: [authGuard]},
        ]
    },
    {path: 'contaContabil', component: ContaComponent, title: 'Conta', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesContaComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesContaComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroContaComponent, title: 'Cadastro', canActivate: [authGuard]},
        ]
    },
    {path: 'grupoConta', component: GrupoComponent, title: 'Grupo', canActivate: [authGuard],
        children: [
            {path: 'detalhes/:id', component: DetalhesGrupoComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'detalhes', component: DetalhesGrupoComponent, title: 'Detalhes', canActivate: [authGuard]},
            {path: 'cadastro', component: CadastroGrupoComponent, title: 'Cadastro', canActivate: [authGuard]},
        ]
    },

    {path: 'dashboard', component: DashboardComponent, title: 'Dashboard' , canActivate: [authGuard]},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
