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


export const routes: Routes = [
    {
        path: 'user', component: UserComponent,
        children: [
            {path: 'login', component: LoginComponent},
            {path: 'registration', component: RegistrationComponent}
        ]
    },
    {path: 'user/perfil', component: PerfilComponent, title: 'Perfil'},
    {path: 'categoria', redirectTo: 'categoria/cadastro'},
    {path: 'categoria', component: CategoriaComponent, title: 'Categoria',
        children: [
           // {path: 'editar/:id', component: EditarComponent, title: 'Editar'},
            {path: 'detalhes/:id', component: DetalhesComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroComponent, title: 'Cadastro'}
        ]
    },
    {path: 'lancamento', component: LancamentoComponent, title: 'Lancamentos',
        children: [
            {path: 'detalhes/:id', component: DetalhesLancamentosComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesLancamentosComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroLancamentosComponent, title: 'Cadastro'}
        ]
    },
    {path: 'produtoServico', component: ProdutoServicoComponent, title: 'Produtos Serviços',
        children: [
            {path: 'detalhes/:id', component: DetalhesProdutoServicoComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesProdutoServicoComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroProdutoServicoComponent, title: 'Cadastro'}
        ]
    },
    {path: 'cidade', component: CidadeComponent, title: 'Cidades',
        children: [
            {path: 'detalhes/:id', component: DetalhesCidadeComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesCidadeComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroCidadeComponent, title: 'Cadastro'},
        ]
    },
    {path: 'empresa', component: EmpresaComponent, title: 'Empresa',
        children: [
            {path: 'detalhes/:id', component: DetalhesEmpresaComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesEmpresaComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroEmpresaComponent, title: 'Cadastro'},
        ]
    },
    {path: 'cliente', component: ClienteComponent, title: 'Cliente',
        children: [
            {path: 'detalhes/:id', component: DetalhesClienteComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesClienteComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroClienteComponent, title: 'Cadastro'},
        ]
    },
    {path: 'contaContabil', component: ContaComponent, title: 'Conta',
        children: [
            {path: 'detalhes/:id', component: DetalhesContaComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesContaComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroContaComponent, title: 'Cadastro'},
        ]
    },

    {path: 'dashboard', component: DashboardComponent, title: 'Dashboard'},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
