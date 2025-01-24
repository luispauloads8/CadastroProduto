import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroProdutoServicoComponent } from './cadastroProdutoServico.component';

describe('CadastroProdutoServicoComponent', () => {
  let component: CadastroProdutoServicoComponent;
  let fixture: ComponentFixture<CadastroProdutoServicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroProdutoServicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CadastroProdutoServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
