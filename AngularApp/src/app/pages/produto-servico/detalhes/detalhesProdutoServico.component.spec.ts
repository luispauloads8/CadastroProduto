import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesProdutoServicoComponent } from './detalhesProdutoServico.component';

describe('DetalhesProdutoServicoComponent', () => {
  let component: DetalhesProdutoServicoComponent;
  let fixture: ComponentFixture<DetalhesProdutoServicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesProdutoServicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetalhesProdutoServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
