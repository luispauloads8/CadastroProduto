import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroCidadeComponent } from './cadastroCidade.component';

describe('CadastroCidadeComponent', () => {
  let component: CadastroCidadeComponent;
  let fixture: ComponentFixture<CadastroCidadeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroCidadeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CadastroCidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
