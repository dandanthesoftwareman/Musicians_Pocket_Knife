import { TestBed } from '@angular/core/testing';

import { SongDetailService } from './song-detail.service';

describe('SongDetailService', () => {
  let service: SongDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SongDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
