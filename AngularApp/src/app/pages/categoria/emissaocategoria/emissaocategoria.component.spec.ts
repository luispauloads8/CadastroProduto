import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmissaocategoriaComponent } from './emissaocategoria.component';

describe('EmissaocategoriaComponent', () => {
  let component: EmissaocategoriaComponent;
  let fixture: ComponentFixture<EmissaocategoriaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmissaocategoriaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmissaocategoriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
