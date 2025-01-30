import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesLancamentosComponent } from './detalhesLancamentos.component';

describe('DetalhesLancamentosComponent', () => {
  let component: DetalhesLancamentosComponent;
  let fixture: ComponentFixture<DetalhesLancamentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesLancamentosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetalhesLancamentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
