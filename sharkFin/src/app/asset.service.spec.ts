import { TestBed } from '@angular/core/testing';

import { AssetService } from './asset.service';

describe('TradeService', () => {
  let service: AssetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
