import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesCidadeComponent } from './detalhesCidade.component';

describe('DetalhesCidadeComponent', () => {
  let component: DetalhesCidadeComponent;
  let fixture: ComponentFixture<DetalhesCidadeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesCidadeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetalhesCidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
