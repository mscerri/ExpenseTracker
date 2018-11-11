import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyExpensesComponent } from './my-expenses.component';

describe('MyExpensesComponent', () => {
  let component: MyExpensesComponent;
  let fixture: ComponentFixture<MyExpensesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyExpensesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyExpensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
