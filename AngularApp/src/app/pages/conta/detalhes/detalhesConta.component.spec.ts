import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesContaComponent } from './detalhesConta.component';

describe('DetalhesContaComponent', () => {
  let component: DetalhesContaComponent;
  let fixture: ComponentFixture<DetalhesContaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesContaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DetalhesContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
