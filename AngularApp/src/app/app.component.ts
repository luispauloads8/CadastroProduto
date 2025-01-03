import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NavComponent, NgxSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  public title = '';
  loading: boolean = true;

  constructor(private spinner: NgxSpinnerService){}


  ngOnInit() {
    /** spinner starts on init */
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
      this.loading = false;
    }, 3000);  // O spinner some após 3 segundos
  }

}
