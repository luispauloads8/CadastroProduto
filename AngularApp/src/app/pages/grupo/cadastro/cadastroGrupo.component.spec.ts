import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroGrupoComponent } from './cadastroGrupo.component';

describe('CadastroGrupoComponent', () => {
  let component: CadastroGrupoComponent;
  let fixture: ComponentFixture<CadastroGrupoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroGrupoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CadastroGrupoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
