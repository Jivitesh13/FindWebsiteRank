import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchTrendComponent } from './search-trend.component';

describe('SearchTrendComponent', () => {
  let component: SearchTrendComponent;
  let fixture: ComponentFixture<SearchTrendComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchTrendComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchTrendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
