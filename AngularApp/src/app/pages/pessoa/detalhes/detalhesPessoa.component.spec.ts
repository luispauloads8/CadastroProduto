import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesPessoaComponent } from './detalhesPessoa.component';

describe('DetalhesPessoaComponent', () => {
  let component: DetalhesPessoaComponent;
  let fixture: ComponentFixture<DetalhesPessoaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesPessoaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetalhesPessoaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
