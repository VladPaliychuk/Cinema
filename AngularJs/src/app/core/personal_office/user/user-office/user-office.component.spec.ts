import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserOfficeComponent } from './user-office.component';

describe('UserOfficeComponent', () => {
  let component: UserOfficeComponent;
  let fixture: ComponentFixture<UserOfficeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserOfficeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserOfficeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
