import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductDirectorComponent } from './product-director.component';

describe('ProductDirectorComponent', () => {
  let component: ProductDirectorComponent;
  let fixture: ComponentFixture<ProductDirectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductDirectorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductDirectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
