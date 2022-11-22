import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FindsongComponent } from './findsong.component';

describe('FindsongComponent', () => {
  let component: FindsongComponent;
  let fixture: ComponentFixture<FindsongComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FindsongComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FindsongComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
