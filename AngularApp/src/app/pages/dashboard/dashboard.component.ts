import { Component } from '@angular/core';
import { TituloComponent } from '../../shared/titulo/titulo.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [TituloComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
