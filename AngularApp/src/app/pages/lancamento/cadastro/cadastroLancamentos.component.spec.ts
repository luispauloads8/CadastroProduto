import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroLancamentosComponent } from './cadastroLancamentos.component';


describe('CadastroLancamentosComponent', () => {
  let component: CadastroLancamentosComponent;
  let fixture: ComponentFixture<CadastroLancamentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroLancamentosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CadastroLancamentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
