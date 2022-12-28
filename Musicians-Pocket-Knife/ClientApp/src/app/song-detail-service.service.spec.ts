import { TestBed } from '@angular/core/testing';

import { SongDetailServiceService } from './song-detail-service.service';

describe('SongDetailServiceService', () => {
  let service: SongDetailServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SongDetailServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
