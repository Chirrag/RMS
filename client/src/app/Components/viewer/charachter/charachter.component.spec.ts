import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharachterComponent } from './charachter.component';

describe('CharachterComponent', () => {
  let component: CharachterComponent;
  let fixture: ComponentFixture<CharachterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharachterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CharachterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
