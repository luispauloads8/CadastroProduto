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
import path from 'path';
import { Component } from '@angular/core';
import { EmissaolancamentoComponent } from './pages/lancamento/emissaolancamento/emissaolancamento.component';
import { EmissaocategoriaComponent } from './pages/categoria/emissaocategoria/emissaocategoria.component';
import { PessoaComponent } from './pages/pessoa/pessoa.component';
import { DetalhesPessoaComponent } from './pages/pessoa/detalhes/detalhesPessoa.component';
import { CadastroPessoaComponent } from './pages/pessoa/cadastro/cadastroPessoa.component';
import { PedidoComponent } from './pages/pedido/pedido.component';
import { DetalhesPedidosComponent } from './pages/pedido/detalhes/detalhesPedidos.component';
import { CadastroPedidosComponent } from './pages/pedido/cadastro/cadastroPedidos.component';

const protectedRoutes = [
    { path: 'categoria', component: CategoriaComponent, title: 'Categoria', detalhes: DetalhesComponent, cadastro: CadastroComponent, emissao: EmissaocategoriaComponent },
    { path: 'lancamento', component: LancamentoComponent, title: 'Lançamentos', detalhes: DetalhesLancamentosComponent, cadastro: CadastroLancamentosComponent, emissao: EmissaolancamentoComponent },
    { path: 'pedido', component: PedidoComponent, title: 'Pedidos', detalhes: DetalhesPedidosComponent, cadastro: CadastroPedidosComponent},
    { path: 'produtoServico', component: ProdutoServicoComponent, title: 'Produtos Serviços', detalhes: DetalhesProdutoServicoComponent, cadastro: CadastroProdutoServicoComponent },
    { path: 'cidade', component: CidadeComponent, title: 'Cidades', detalhes: DetalhesCidadeComponent, cadastro: CadastroCidadeComponent },
    { path: 'empresa', component: EmpresaComponent, title: 'Empresa', detalhes: DetalhesEmpresaComponent, cadastro: CadastroEmpresaComponent },
    { path: 'cliente', component: ClienteComponent, title: 'Cliente', detalhes: DetalhesClienteComponent, cadastro: CadastroClienteComponent },
    { path: 'pessoa', component: PessoaComponent, title: 'Pessoa', detalhes: DetalhesPessoaComponent, cadastro: CadastroPessoaComponent },
    { path: 'contaContabil', component: ContaComponent, title: 'Conta', detalhes: DetalhesContaComponent, cadastro: CadastroContaComponent },
    { path: 'grupoConta', component: GrupoComponent, title: 'Grupo', detalhes: DetalhesGrupoComponent, cadastro: CadastroGrupoComponent },
    { path: 'user/perfil', component: PerfilComponent, title: 'Usuario', detalhes: PerfilComponent, cadastro: RegistrationComponent},
];

export const routes: Routes = [
    {
        path: 'user', component: UserComponent,
        children: [
            { path: 'login', component: LoginComponent, title: 'Login' },
            { path: 'registration', component: RegistrationComponent, title: 'Registra' }
        ]
    },
    

    { path: 'categoria', redirectTo: 'categoria/cadastro' },
    { path: 'lancamento', redirectTo: 'lancamento/cadastro' },
    { path: 'pedido', redirectTo: 'pedido/cadastro' },
    { path: 'produtoServico', redirectTo: 'produtoServico/cadastro' },
    { path: 'cidade', redirectTo: 'cidade/cadastro' },
    { path: 'empresa', redirectTo: 'empresa/cadastro' },
    { path: 'cliente', redirectTo: 'cliente/cadastro' },
    { path: 'pesssoa', redirectTo: 'pessoa/cadastro' },
    { path: 'contaContabil', redirectTo: 'contaContabil/cadastro' },
    { path: 'grupoConta', redirectTo: 'grupoConta/cadastro' },
    
    {
        path: '',
            canActivate: [authGuard],
            runGuardsAndResolvers: 'always',
            children: protectedRoutes.map(route => ({
            path: route.path,
            component: route.component,
            title: route.title,
            children: [
                ...(route.detalhes ? [
                    { path: 'detalhes/:id', component: route.detalhes, title: 'Detalhes' },
                    { path: 'detalhes', component: route.detalhes, title: 'Detalhes' },
                ] : []),
                ...(route.cadastro ? [
                    { path: 'cadastro', component: route.cadastro, title: 'Cadastro' },
                ] : []),
                ...(route.emissao ? [
                    { path: 'emissao', component: route.emissao, title: 'Emissão' },
                ] : []),
            ]
        }))
},

    { path: 'dashboard', component: DashboardComponent, title: 'Dashboard', canActivate: [authGuard] },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];