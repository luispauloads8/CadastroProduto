import { Routes } from '@angular/router';
import { CategoriaComponent } from './pages/categoria/categoria.component';
import { CadastroComponent } from './pages/cadastro/cadastro.component';
import { EditarComponent } from './pages/editar/editar.component';
import { DetalhesComponent } from './pages/detalhes/detalhes.component';
import { ProdutoServicoComponent } from './produto-servico/produto-servico.component';
import { title } from 'process';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PerfilComponent } from './perfil/perfil.component';


export const routes: Routes = [
    {path: 'cadastro', component: CadastroComponent, title: 'Cadastro'},
    {path: 'categoria', component: CategoriaComponent, title: 'Categoria'},
    {path: 'editar/:id', component: EditarComponent, title: 'Editar'},
    {path: 'detalhes/:id', component: DetalhesComponent, title: 'Detalhes'},
    {path: 'produtoServico', component: ProdutoServicoComponent, title: 'Produtos Serviços'},
    {path: 'dashboard', component: DashboardComponent, title: 'Dashboard'},
    {path: 'perfil', component: PerfilComponent, title: 'Perfil'},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
