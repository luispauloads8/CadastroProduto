import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesPedidosComponent } from './detalhesPedidos.component';

describe('DetalhesComponent', () => {
  let component: DetalhesPedidosComponent;
  let fixture: ComponentFixture<DetalhesPedidosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesPedidosComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DetalhesPedidosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
