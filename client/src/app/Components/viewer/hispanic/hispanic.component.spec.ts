import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HispanicComponent } from './hispanic.component';

describe('HispanicComponent', () => {
  let component: HispanicComponent;
  let fixture: ComponentFixture<HispanicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HispanicComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HispanicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
