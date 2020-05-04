import { TestBed } from '@angular/core/testing';

import { VideoPokerService } from './video-poker.service';

describe('VideoPokerService', () => {
  let service: VideoPokerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VideoPokerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
