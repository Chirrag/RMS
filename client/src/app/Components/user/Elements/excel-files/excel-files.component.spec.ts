import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcelFilesComponent } from './excel-files.component';

describe('ExcelFilesComponent', () => {
  let component: ExcelFilesComponent;
  let fixture: ComponentFixture<ExcelFilesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExcelFilesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExcelFilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
