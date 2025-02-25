import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CidadeComponent } from './cidade.component';

describe('CidadeComponent', () => {
  let component: CidadeComponent;
  let fixture: ComponentFixture<CidadeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CidadeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
