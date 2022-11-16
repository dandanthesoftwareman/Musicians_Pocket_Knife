import { TestBed } from '@angular/core/testing';

import { ApireturnsongarrayService } from './apireturnsongarray.service';

describe('ApireturnsongarrayService', () => {
  let service: ApireturnsongarrayService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApireturnsongarrayService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
