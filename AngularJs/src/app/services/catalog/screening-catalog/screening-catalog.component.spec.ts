import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreeningCatalogComponent } from './screening-catalog.component';

describe('ScreeningCatalogComponent', () => {
  let component: ScreeningCatalogComponent;
  let fixture: ComponentFixture<ScreeningCatalogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ScreeningCatalogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScreeningCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
