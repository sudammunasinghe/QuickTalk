import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FindPeopleDialog } from './find-people-dialog';

describe('FindPeopleDialog', () => {
  let component: FindPeopleDialog;
  let fixture: ComponentFixture<FindPeopleDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FindPeopleDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(FindPeopleDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
