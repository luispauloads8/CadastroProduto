import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmissaolancamentoComponent } from './emissaolancamento.component';

describe('EmissaolancamentoComponent', () => {
  let component: EmissaolancamentoComponent;
  let fixture: ComponentFixture<EmissaolancamentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmissaolancamentoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmissaolancamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
