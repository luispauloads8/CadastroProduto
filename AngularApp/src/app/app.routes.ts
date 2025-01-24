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
    {path: 'lancamento', component: LancamentoComponent, title: 'Lancamentos'},
    {path: 'produtoServico', component: ProdutoServicoComponent, title: 'Produtos Serviços',
        children: [
            {path: 'detalhes/:id', component: DetalhesProdutoServicoComponent, title: 'Detalhes'},
            {path: 'detalhes', component: DetalhesProdutoServicoComponent, title: 'Detalhes'},
            {path: 'cadastro', component: CadastroProdutoServicoComponent, title: 'Cadastro'},
        ]
    },
    {path: 'dashboard', component: DashboardComponent, title: 'Dashboard'},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
